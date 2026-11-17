using ShoulderDelivery.Common;
using System;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CitizenManager : MonoBehaviour
    {
        [SerializeField] Citizen[] _citizens;
        [SerializeField] ActiveCitizenCountPair[] _pair;
        int[] _activeCitizenIndices;

        public event Action OnHitPlayer;

        public void Init(StageHour stageHour)
        {
            var count = Mathf.Min(_pair.First(p => p.StageHour == stageHour).ActiveCitizenCount, _citizens.Length - 1);
            _activeCitizenIndices = Enumerable.Range(0, _citizens.Length).ToArray();
            for (int i = 0; i < _citizens.Length; i++)
            {
                var index = UnityEngine.Random.Range(i, _citizens.Length);
                (_activeCitizenIndices[i], _activeCitizenIndices[index]) = (_activeCitizenIndices[index], _activeCitizenIndices[i]);
            }

            for (int i = 0; i < _citizens.Length; i++)
            {
                var citizen = _citizens[_activeCitizenIndices[i]];
                citizen.gameObject.SetActive(i < count);
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

        [Serializable]
        class ActiveCitizenCountPair
        {
            [SerializeField] StageHour _stageHour;
            [SerializeField] int _activeCitizenCount;

            public StageHour StageHour => _stageHour;
            public int ActiveCitizenCount => _activeCitizenCount;
        }
    }
}
