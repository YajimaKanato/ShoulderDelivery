using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using ShoulderDerivery.Common;
using System.Collections.Generic;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardLauncher : MonoBehaviour, ICardboardLauncher, IDiscardCardboard
    {
        [SerializeField] CardboardView[] _cardboardViews;
        [SerializeField] int _poolSize = 10;
        [SerializeField, Tooltip("投げる段ボールの重さ")] CardboardWeight _cardboardWeight;
        Dictionary<CardboardWeight, OriginalObjectPool<CardboardView>> _cardboardViewPools = new();
        readonly Dictionary<CardboardId, CardboardView> _inFlight = new();

        public event System.Action<ResolveDeliveryInput> HitTarget;
        public event System.Action<DiscardCardboardInput> CardboardExpired;

        public void Awake()
        {
            // プールの設定
            foreach (var cardboardView in _cardboardViews)
            {
                if (cardboardView == null) continue;
                _cardboardViewPools[cardboardView.CardboardWeight] = new(cardboardView, _poolSize);
            }
        }

        public void LaunchCardboard(ThrowCardboardOutput output)
        {
            var cardboardWeight = ResolveWeight(output.CardboardWeight);
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
                cardboard = Instantiate(cardboard);
                cardboard.OnHit += ReportHit;
                cardboard.OnExpired += ReportExpiry;
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

            _inFlight.Add(cardboardId, cardboard);
        }

        void ReportHit(CardboardId id, TargetId? targetId)
            => HitTarget?.Invoke(new ResolveDeliveryInput(id, targetId));

        void ReportExpiry(CardboardId id)
            => CardboardExpired?.Invoke(new DiscardCardboardInput(id));

        public void DiscardCardboard(CardboardId cardboardId)
        {
            // 削除する段ボールを取得
            if (!_inFlight.Remove(cardboardId, out var cardboard)) return;

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
        /// <returns>段ボールの重さ</returns>
        public float GetCardboardWeight()
        {
            return _cardboardViewPools.TryGetValue(_cardboardWeight, out var cardboard)
                ? cardboard.OriginalObject.RealCardboardWeight
                : (int)_cardboardWeight;
        }

        public void SetNextThrowingCardboard(CardboardWeight weight)
        {
            _cardboardWeight = ResolveWeight(weight);
        }

        /// <summary>
        /// 段ボールの重さを返すメソッド
        /// </summary>
        /// <param name="weight">段ボールの重さ</param>
        /// <returns>段ボールの重さ</returns>
        CardboardWeight ResolveWeight(CardboardWeight weight)
        {
            return weight == CardboardWeight.NoRequested || weight == 0
                ? CardboardWeight.Weight0
                : weight;
        }
    }
}
