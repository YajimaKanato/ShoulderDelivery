using System;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class Citizen : MonoBehaviour
    {
        [SerializeField] string _hitPlayerState;
        [SerializeField] string _hitPlayerValueState;
        Animator _animator;
        bool _hitPlayer;
        int _hitPlayerValue;

        public event Action OnPlayerHit;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void EnterPlayer()
        {
            _hitPlayer = true;
            _hitPlayerValue = UnityEngine.Random.Range(0, 2);
            OnPlayerHit?.Invoke();
        }

        void ExitPlayer()
        {
            _hitPlayer = false;
        }

        private void LateUpdate()
        {
            _animator?.SetBool(_hitPlayerState, _hitPlayer);
            _animator?.SetInteger(_hitPlayerValueState, _hitPlayerValue);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;

            EnterPlayer();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;

            ExitPlayer();
        }
    }
}
