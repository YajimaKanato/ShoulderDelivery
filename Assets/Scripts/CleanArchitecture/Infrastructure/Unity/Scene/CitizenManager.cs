using System;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CitizenManager : MonoBehaviour
    {
        [SerializeField] Citizen[] _citizens;

        public event Action OnHitPlayer;

        private void Awake()
        {
            foreach (var citizen in _citizens)
            {
                citizen.OnPlayerHit += HitPlayer;
            }
        }

        private void OnDestroy()
        {
            foreach (var citizen in _citizens)
            {
                citizen.OnPlayerHit -= HitPlayer;
            }
        }

        void HitPlayer()
        {
            OnHitPlayer?.Invoke();
        }
    }
}
