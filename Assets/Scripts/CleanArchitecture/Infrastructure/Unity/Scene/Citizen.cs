using System;
using UnityEngine;
using Unity.Mathematics;

namespace ShoulderDelivery.Infrastructure
{
    public class Citizen : MonoBehaviour
    {
        [SerializeField] bool _isGreeting;
        protected Animator _animator;
        Transform _target;
        protected Vector3 _forward;
        bool _hitPlayer;
        protected bool _dancing;
        int _hitPlayerValue;

        public event Action OnPlayerHit;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _target = FindAnyObjectByType<PlayerController>()?.transform;
            _forward = transform.forward;
        }

        protected virtual void Update()
        {
            if (_dancing)
            {
                Vector3 direction = _target.position - transform.position;
                direction.y = 0;

                if (direction.sqrMagnitude > 0.0001f)
                {
                    transform.forward = direction;
                }
            }
        }

        void EnterPlayer()
        {
            _dancing = true;
            _hitPlayer = true;
            _hitPlayerValue = UnityEngine.Random.Range(0, 2);
            OnPlayerHit?.Invoke();
        }

        void ExitPlayer()
        {
            _hitPlayer = false;
        }

        void EndDancing()
        {
            _dancing = _hitPlayer;
            transform.forward = _forward;
        }

        protected virtual void LateUpdate()
        {
            _animator?.SetBool("HitPlayer", _hitPlayer);
            _animator?.SetInteger("HitPlayerRandomValue", _hitPlayerValue);
            _animator?.SetBool("Greeting", _isGreeting && UnityEngine.Random.Range(0, 100) == 0);
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

    internal static class Float3Extention
    {
        internal static Vector3 TransportF3ToV3(this float3 position)
        {
            return new Vector3(position.x, position.y, position.z);
        }
    }
}
