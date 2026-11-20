using Cysharp.Threading.Tasks;
using ShoulderDelivery.Adapter;
using ShoulderDelivery.Entity;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class GameResultView : MonoBehaviour, IGameResultView
    {
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _timeText;
        [SerializeField] TextMeshProUGUI _deliveryCountText;
        [SerializeField] TextMeshProUGUI _resultText;
        [SerializeField] TextMeshProUGUI _rankStamp;
        [SerializeField, Min(0)] float _rankStampDuration = 0.3f;
        [SerializeField] Button _stageSelectButton;
        [SerializeField] PlayableDirector _playableDirector;
        [SerializeField] TextMeshProUGUI _finishText;
        [SerializeField, Min(0)] float _finishDuration = 2f;
        [SerializeField, Min(0)] float _finishEntryDuration = 0.4f;
        [SerializeField, Min(1)] float _finishEntryScale = 1.8f;
        [SerializeField, Min(0)] float _deliveryCountDuration = 1f;
        [SerializeField, Min(0)] float _timeDuration = 1f;
        [SerializeField, Min(0)] float _scoreDuration = 1.5f;

        CancellationTokenSource _performanceCancellation;
        bool IsAssigned => _resultText != null
            && _scoreText != null
            && _timeText != null
            && _deliveryCountText != null;

        public void ShowClear(GameClearViewModel viewModel)
        {
            ShowResultAsync("配達完了！", "ステージクリア！", viewModel.DeliveryCount, viewModel.RequiredDeliveryCount
                , viewModel.RemainingTime, viewModel.TotalScore, viewModel.Rank).SuppressCancellationThrow().Forget();
        }

        public void ShowFailed(GameFailedViewModel viewModel)
        {
            ShowResultAsync("タイムアップ！", "タイムアップ", viewModel.DeliveryCount, viewModel.RequiredDeliveryCount
                , viewModel.RemainingTime, viewModel.TotalScore, viewModel.Rank).SuppressCancellationThrow().Forget();
        }

        /// <summary>演出と数値表示が終わってからボタン操作を受け付けるメソッド</summary>
        async UniTask ShowResultAsync(string finish, string result, int deliveryCount, int requiredDeliveryCount
            , float remainingTime, int score, ScoreRank rank)
        {
            if (!IsAssigned || _performanceCancellation != null) return;

            // gameObjectに紐づいたCancellationTokenを取得する
            var lifetime = _finishText != null
                ? _finishText.GetCancellationTokenOnDestroy()
                : this.GetCancellationTokenOnDestroy();

            // gameObjectに紐づいたCancellationTokenから新しくCancellationTokenSourceを作成
            var cancellation = CancellationTokenSource.CreateLinkedTokenSource(lifetime);
            _performanceCancellation = cancellation;
            var ct = cancellation.Token;

            // ボタンを触れないようにする
            var buttons = GetComponentsInChildren<Button>(true);
            var interactable = new bool[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                interactable[i] = buttons[i].interactable;
                buttons[i].interactable = false;
            }

            try
            {
                await ShowFinishAsync(finish, ct);
                _resultText.text = result;
                _deliveryCountText.text = $"0 / {requiredDeliveryCount}";
                SetTime(0);
                _scoreText.text = "0";
                if (_rankStamp != null) _rankStamp.gameObject.SetActive(false);
                gameObject.SetActive(true);

                // リザルトの演出
                await ResultPerformanceAsync(ct);
                // 配達数表示
                await CountNumberAsync(deliveryCount, _deliveryCountDuration
                    , value => _deliveryCountText.text = $"{value} / {requiredDeliveryCount}", ct);
                // 残り時間表示
                var remainingSeconds = StageTimeDisplay.GetRemainingSeconds(remainingTime);
                await CountNumberAsync(remainingSeconds, _timeDuration, SetTime, ct);
                // スコア表示
                await CountNumberAsync(score, _scoreDuration
                    , value => _scoreText.text = value.ToString("0"), ct);
                await ShowRankStampAsync(rank, ct);
            }
            finally
            {
                // いずれかの演出が失敗してもボタンには触れるようにする
                if (_finishText != null) _finishText.gameObject.SetActive(false);
                for (int i = 0; i < buttons.Length; i++)
                    if (buttons[i] != null) buttons[i].interactable = interactable[i];

                _performanceCancellation = null;
                cancellation.Dispose();
            }

            if (this != null && isActiveAndEnabled) _stageSelectButton?.Select();
        }

        /// <summary>終了メッセージを表示してからリザルトに進むメソッド</summary>
        async UniTask ShowFinishAsync(string message, CancellationToken ct)
        {
            if (_finishText == null) return;

            var originalScale = _finishText.transform.localScale;
            var originalAlpha = _finishText.alpha;
            var entryDuration = Mathf.Min(_finishEntryDuration, _finishDuration);
            try
            {
                ct.ThrowIfCancellationRequested();
                _finishText.text = message;
                _finishText.transform.localScale = originalScale * _finishEntryScale;
                _finishText.alpha = 0;
                _finishText.gameObject.SetActive(true);

                var elapsed = 0f;
                while (elapsed < entryDuration)
                {
                    var progress = elapsed / entryDuration;
                    // 終わりに向けてゆっくり元のサイズに戻す
                    var eased = 1 - Mathf.Pow(1 - progress, 3);
                    _finishText.transform.localScale = originalScale * Mathf.Lerp(_finishEntryScale, 1, eased);
                    _finishText.alpha = originalAlpha * eased;
                    await UniTask.Yield(PlayerLoopTiming.Update, ct);
                    elapsed += Time.unscaledDeltaTime;
                }

                _finishText.transform.localScale = originalScale;
                _finishText.alpha = originalAlpha;
                await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Max(0, _finishDuration - entryDuration))
                    , ignoreTimeScale: true, cancellationToken: ct);
            }
            finally
            {
                if (_finishText != null)
                {
                    _finishText.gameObject.SetActive(false);
                    _finishText.transform.localScale = originalScale;
                    _finishText.alpha = originalAlpha;
                }
            }
        }

        /// <summary>指定した秒数で数値を0から目標値まで変化させるメソッド</summary>
        async UniTask CountNumberAsync(int target, float duration, Action<int> showNumber, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var elapsed = 0f;
            while (elapsed < duration && target != 0)
            {
                showNumber(Mathf.RoundToInt(Mathf.Lerp(0, target, elapsed / duration)));
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                elapsed += Time.unscaledDeltaTime;
            }
            showNumber(target);
        }

        async UniTask ShowRankStampAsync(ScoreRank rank, CancellationToken ct)
        {
            if (_rankStamp == null) return;
            ct.ThrowIfCancellationRequested();

            var stamp = _rankStamp.rectTransform;
            var originalScale = stamp.localScale;
            _rankStamp.text = rank.ToString();
            stamp.localScale = originalScale * 1.8f;
            _rankStamp.gameObject.SetActive(true);

            try
            {
                var elapsed = 0f;
                while (elapsed < _rankStampDuration)
                {
                    var progress = elapsed / _rankStampDuration;
                    var scale = progress < 0.55f
                        ? Mathf.Lerp(1.8f, 0.9f, Mathf.Pow(progress / 0.55f, 2))
                        : Mathf.Lerp(0.9f, 1f, (progress - 0.55f) / 0.45f);
                    stamp.localScale = originalScale * scale;
                    await UniTask.Yield(PlayerLoopTiming.Update, ct);
                    elapsed += Time.unscaledDeltaTime;
                }
            }
            finally
            {
                if (stamp != null) stamp.localScale = originalScale;
            }
        }

        void SetTime(int seconds)
        {
            _timeText.text = StageTimeDisplay.Format(seconds);
        }

        async UniTask ResultPerformanceAsync(CancellationToken ct)
        {
            if (_playableDirector == null || _playableDirector.playableAsset == null) return;

            _playableDirector.extrapolationMode = DirectorWrapMode.Hold;
            _playableDirector.time = 0;
            _playableDirector.Play();
            await UniTask.WaitUntil(() => _playableDirector.time >= _playableDirector.duration, cancellationToken: ct);
        }

        private void OnDisable() => _performanceCancellation?.Cancel();
        private void OnDestroy() => _performanceCancellation?.Cancel();
    }
}
