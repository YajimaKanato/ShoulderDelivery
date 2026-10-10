using ShoulderDelivery.Entity;
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

        public void Init()
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

        }
    }
}
