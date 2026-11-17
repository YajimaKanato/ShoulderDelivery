using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class TitleDancingCitizen : MonoBehaviour
    {
        [SerializeField] GameObject[] _citizens;

        public void Spawn()
        {
            Instantiate(_citizens[Random.Range(0, _citizens.Length)], transform);
        }
    }
}
