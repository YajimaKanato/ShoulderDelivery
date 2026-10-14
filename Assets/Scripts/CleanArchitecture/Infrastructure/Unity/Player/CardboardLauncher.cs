using ShoulderDelivery.UseCase;
using ShoulderDerivery.Common;
using System.Collections.Generic;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardLauncher : MonoBehaviour, ICardboardLauncher
    {
        [SerializeField] CardboardView[] _cardboardViews;
        [SerializeField] int _poolSize = 10;
        Dictionary<CardboardWeight, OriginalObjectPool<CardboardView>> _cardboardViewPools = new();
        ActionDisposer _disposer;

        public void Init()
        {
            _disposer = new();

            // プールの設定
            foreach (var cardboardView in _cardboardViews)
            {
                if (cardboardView == null) continue;
                _cardboardViewPools[cardboardView.CardboardWeight] ??= new(cardboardView, _poolSize);
                _disposer.AddActionDisposing(cardboardView.OnReleaseToPool(ReleaseToPool));
            }
        }

        private void OnDestroy()
        {
            _disposer?.Dispose();
        }

        public void LaunchCardboard(ThrowCardboardOutput output)
        {
            var cardboardWeight = output.CardboardWeight;
            if (!_cardboardViewPools.TryGetValue(cardboardWeight, out var pool) || pool == null)
            {
                Debug.LogWarning("オブジェクトプールがありませんでした");
                return;
            }

            if (pool.TryRecycleObject(out var cardboard))
            {
                // 再利用
                cardboard.gameObject.SetActive(true);
            }
            else
            {
                // シーンに生成
                Instantiate(cardboard);
            }

            // 射出に必要なデータの準備
            var cardboardId = output.CardboardId;
            var cardboardLaunch = output.Launch;
            var position = LaunchCalculator.CalculatePosition(cardboardLaunch.Position);
            var rotation = LaunchCalculator.CalculateRotation(cardboardLaunch.Rotation);
            var launch = LaunchCalculator.CalculateSpeed(cardboardLaunch.Direction, cardboardLaunch.InitialSpeed);

            // 射出
            cardboard.Launch(cardboardId
                , position
                , rotation
                , launch);
        }

        /// <summary>
        /// プールに戻すメソッド
        /// </summary>
        /// <param name="view">戻すオブジェクト</param>
        void ReleaseToPool(CardboardView view)
        {
            if (view == null) return;
            if (!_cardboardViewPools.TryGetValue(view.CardboardWeight, out var pool) || pool == null)
            {
                Debug.LogWarning("オブジェクトプールがありませんでした");
                return;
            }

            // プールに戻す
            if (!pool.TryReleaseToPool(view))
            {
                Destroy(view.gameObject);
            }
            else
            {
                view.gameObject.SetActive(false);
            }
        }
    }
}
