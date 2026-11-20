using Cysharp.Threading.Tasks;
using ShoulderDelivery.Adapter;
using ShoulderDelivery.Common;
using ShoulderDelivery.Entity;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class StageSelectView : MonoBehaviour, IStageSelectView
    {
        [SerializeField] StageButton _buttonPrefab;

        [SerializeField] ScrollRect _scrollView;
        [SerializeField] RectTransform _selectPanel;
        [SerializeField] TextMeshProUGUI _stageName;
        [SerializeField] TextMeshProUGUI _stageInfo;
        [SerializeField] TextMeshProUGUI _stageCount;
        [SerializeField] Button _startButton;
        [SerializeField] float _waitTime = 0.1f;
        [SerializeField] FadeDirector _director;

        Button[] _rightButtons;
        Button _firstButton;
        Button _selectedButton;
        GameObject _focusedButton;
        bool _loading;

        public event Action<StageId> OnStageSelected;
        public event Action OnStart;
        public event Action OnOpenStageSelect;

        /// <summary>タイトル画面とステージ選択画面を準備するメソッド</summary>
        public void Initialize()
        {
            _startButton.interactable = false;
            _startButton.onClick.AddListener(PressStartButton);
            _selectPanel.gameObject.SetActive(false);
        }

        /// <summary>ステージ選択ボタンを作成するメソッド</summary>
        public async void CreateStageSelectButton(CreateStageSelectViewModel viewModel, CancellationToken ct)
        {
            // ステージ数を表示
            _stageCount.text = "全 " + viewModel.StageCount + " ステージ";

            // ボタンを生成
            var buttons = new Button[viewModel.StageCount];
            for (int i = 0; i < buttons.Length; i++)
            {
                // キャンセルされたら終了
                if (ct.IsCancellationRequested) return;

                var (stageId, isCleared) = viewModel.Stages[i];
                var stageButton = CreateButton(stageId.Id, isCleared);
                var button = stageButton.GetComponent<Button>();
                buttons[i] = button;

                // 初めのボタンを設定
                if (_firstButton == null)
                {
                    _firstButton = button;
                    if (_firstButton) _firstButton.Select();
                }

                // イベントを登録
                button.onClick.AddListener(() =>
                {
                    if (_loading) return;
                    if (_selectedButton) _selectedButton.image.color = Color.white;
                    _selectedButton = button;
                    button.image.color = Color.white;
                    OnStageSelected?.Invoke(stageId);
                });

                // 最初のボタンを選んでおく
                if (i == 0) button.onClick.Invoke();

                // 任意の秒数待ち演出をずらす
                stageButton.OpenStageSelect();
                if (await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: ct)
                    .SuppressCancellationThrow()) return;
            }

            if (ct.IsCancellationRequested) return;
            SetStageNavigation(buttons);
        }

        /// <summary>ステージ一覧と開始ボタンの間の移動を設定するメソッド</summary>
        /// <param name="buttons">生成したステージ選択ボタン</param>
        void SetStageNavigation(Button[] buttons)
        {
            var columns = GetComponent<GridLayoutGroup>().constraintCount;
            var rowCount = (buttons.Length + columns - 1) / columns;
            _rightButtons = new Button[rowCount];

            for (int i = 0; i < buttons.Length; i++)
            {
                var column = i % columns;
                var isRightEdge = column == columns - 1 || i == buttons.Length - 1;

                // 生成中にステージが選ばれていた場合も、STARTへの移動を設定する
                var right = isRightEdge
                    ? (_startButton.interactable ? _startButton : null)
                    : buttons[i + 1];

                buttons[i].navigation = new Navigation
                {
                    mode = Navigation.Mode.Explicit,
                    selectOnLeft = column > 0 ? buttons[i - 1] : null,
                    selectOnRight = right,
                    selectOnUp = i >= columns ? buttons[i - columns] : null,
                    selectOnDown = i + columns < buttons.Length ? buttons[i + columns] : null
                };

                if (isRightEdge)
                    _rightButtons[i / columns] = buttons[i];
            }
        }

        /// <summary>選択されたステージの詳細を表示するメソッド</summary>
        public void ShowStageInfo(ShowStageInfoViewModel viewModel)
        {
            _stageName.text = viewModel.StageName;
            _stageInfo.text = $"時間帯  {GetStageHour(viewModel.StageHour)}\n"
                + $"制限時間  {StageTimeDisplay.Format(viewModel.TimeLimitSeconds)}\n"
                + $"ノルマ配達数  {viewModel.RequiredDeliveryCount}\n"
                + $"ベスト  {viewModel.BestScore?.ToString("N0") ?? "--"}  /  {viewModel.BestRank?.ToString() ?? "--"}";
            _startButton.interactable = true;

            // まだ生成中なら、生成完了時に右端の移動先を設定する
            if (_rightButtons == null) return;

            foreach (var button in _rightButtons)
            {
                var navigation = button.navigation;
                navigation.selectOnRight = _startButton;
                button.navigation = navigation;
            }
        }

        /// <summary>ロード中にボタンの連打を防ぐメソッド</summary>
        public void ShowLoading()
        {
            _loading = true;
            _startButton.interactable = false;
        }

        /// <summary>ステージ選択画面に切り替えるメソッド</summary>
        public void OpenStageSelect()
        {
            _selectPanel.gameObject.SetActive(true);
            Canvas.ForceUpdateCanvases();
            _scrollView.verticalNormalizedPosition = 1f;
            OnOpenStageSelect?.Invoke();
        }

        private void LateUpdate()
        {
            // キーやパッドでスクロール範囲外のボタンを選んだ場合は表示範囲に収める
            var selected = EventSystem.current?.currentSelectedGameObject;
            if (!selected || selected == _focusedButton || !selected.transform.IsChildOf(transform)) return;

            _focusedButton = selected;
            // 開始ボタンから左に戻ったときは直前のステージボタンに戻す
            _startButton.navigation = new Navigation
            {
                mode = Navigation.Mode.Explicit,
                selectOnLeft = selected.GetComponent<Button>()
            };

            var viewport = _scrollView.viewport;
            var bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, selected.transform);
            var offset = Mathf.Max(0f, viewport.rect.yMin - bounds.min.y)
                - Mathf.Max(0f, bounds.max.y - viewport.rect.yMax);
            if (Mathf.Approximately(offset, 0f)) return;

            _scrollView.StopMovement();
            _scrollView.content.anchoredPosition += Vector2.up * offset;
        }

        StageButton CreateButton(string label, bool isCleared)
        {
            var stageButton = Instantiate(_buttonPrefab, transform);
            var button = stageButton.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.image.color = Color.white;
            stageButton.SetCleared(isCleared);
            var text = button.GetComponentInChildren<TextMeshProUGUI>(true);
            text.text = isCleared ? $"{label}\nクリア" : label;
            text.enableAutoSizing = true;
            text.fontSizeMin = 14f;
            text.fontSizeMax = 25f;
            return stageButton;
        }

        /// <summary>シーン遷移前にフェードアウトするメソッド</summary>
        public async UniTask FadeOut()
        {
            if (_director != null) await _director.FadeOut();
        }

        void PressStartButton()
        {
            if (!_loading) OnStart?.Invoke();
        }

        string GetStageHour(StageHour stageHour)
        {
            return stageHour switch
            {
                StageHour.Morning => "朝",
                StageHour.Afternoon => "昼",
                StageHour.Evening => "夕方",
                StageHour.Night => "夜",
                _ => "朝"
            };
        }
    }
}
