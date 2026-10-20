using ShoulderDelivery.UseCase;
using UnityEngine;
using System;

namespace ShoulderDelivery.Infrastructure
{
    public class DeliveryManager : MonoBehaviour
    {
        ResolveDeliveryUseCase _resolveDeliveryUseCase;
        DiscardCardboardUseCase _discardCardboardUseCase;

        bool _isInitialized = false;

        static DeliveryManager _instance;
        public static DeliveryManager Instance => _instance;

        public void Init(ResolveDeliveryUseCase resolveDeliveryUseCase
            , DiscardCardboardUseCase discardCardboardUseCase)
        {
            _resolveDeliveryUseCase = resolveDeliveryUseCase
                ?? throw new ArgumentNullException(nameof(resolveDeliveryUseCase));
            _discardCardboardUseCase = discardCardboardUseCase
                ?? throw new ArgumentNullException(nameof(discardCardboardUseCase));

            _isInitialized = true;
        }

        //public void 
    }
}
