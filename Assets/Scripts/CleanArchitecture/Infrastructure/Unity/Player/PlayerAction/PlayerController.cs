using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class PlayerController : MonoBehaviour, IControllerView
    {
        PlayerActionBase[] _playerActionBases;
        Actions _actions;
        bool _inputEnabled;

        private void Awake()
        {
            _actions = new();

            _playerActionBases ??= GetComponents<PlayerActionBase>();
            foreach (var playerActionBase in _playerActionBases)
                playerActionBase?.CacheAction(_actions);
        }

        // Update is called once per frame
        void Update()
        {
            if (!_inputEnabled) return;
            foreach (var playerActionBase in _playerActionBases)
                playerActionBase?.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (!_inputEnabled) return;
            foreach (var playerActionBase in _playerActionBases)
                playerActionBase?.OnFixedUpdate();
        }

        public void SetControllerEnabled(bool enabled)
        {
            _inputEnabled = enabled;
            if (enabled)
            {
                _actions.Ingame.Enable();
                return;
            }

            _actions.Ingame.Disable();
            GetComponent<PlayerCardboardLaunch>()?.HideGuide();
            if (TryGetComponent<Rigidbody>(out var body))
            {
                body.linearVelocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
            }
        }

        private void OnDestroy() => _actions?.Dispose();
    }
}
