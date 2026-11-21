using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class TitleDancingCitizen : MonoBehaviour
    {
        [SerializeField] GameObject[] _citizens;
        bool _spawned;

        public void Spawn()
        {
            if (_spawned || _citizens == null || _citizens.Length == 0) return;

            Instantiate(_citizens[Random.Range(0, _citizens.Length)], transform);
            _spawned = true;
        }
    }
}
