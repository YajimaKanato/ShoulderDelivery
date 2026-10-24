using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using ShoulderDerivery.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardLauncher : MonoBehaviour, ICardboardLauncher, IDiscardCardboard
    {
        [SerializeField] CardboardView[] _cardboardViews;
        [SerializeField] int _poolSize = 10;
        Dictionary<CardboardWeight, OriginalObjectPool<CardboardView>> _cardboardViewPools = new();
        HashSet<CardboardView> _inFlightCardboardHashSet;

        public void Awake()
        {
            // プールの設定
            foreach (var cardboardView in _cardboardViews)
            {
                if (cardboardView == null) continue;
                _cardboardViewPools[cardboardView.CardboardWeight] ??= new(cardboardView, _poolSize);
            }
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
                if (_inFlightCardboardHashSet.Contains(cardboard)) return;
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
            var launch = LaunchCalculator.CalculateSpeed(cardboardLaunch.Direction, cardboardLaunch.ThrowPower);

            // 射出
            cardboard.Launch(cardboardId
                , position
                , rotation
                , launch);

            _inFlightCardboardHashSet ??= new();
            _inFlightCardboardHashSet.Add(cardboard);
        }

        public void DiscardCardboard(CardboardId cardboardId)
        {
            // 削除する段ボールを取得
            var cardboard = _inFlightCardboardHashSet.FirstOrDefault(c => c != null && c.CardboardId == cardboardId);
            if (!_inFlightCardboardHashSet.Remove(cardboard)) return;

            // プールに戻してみる
            if (_cardboardViewPools[cardboard.CardboardWeight].TryReleaseToPool(cardboard))
            {
                cardboard.gameObject.SetActive(false);
            }
            else
            {
                Destroy(cardboard.gameObject);
            }
        }

        /// <summary>
        /// 投擲する段ボールの重さを取得するメソッド
        /// </summary>
        /// <param name="weight">段ボールの重さを表すenum</param>
        /// <returns>段ボールの重さ</returns>
        internal float GetCardboardWeight(CardboardWeight weight)
        {
            return _cardboardViewPools.TryGetValue(weight, out var cardboard)
                ? cardboard.OriginalObject.RealCardboardWeight
                : (int)weight;
        }
    }
}
