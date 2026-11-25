using ShoulderDelivery.Adapter;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShoulderDelivery.Infrastructure
{
    public class PlayerController : MonoBehaviour, IControllerView
    {
        PlayerActionBase[] _playerActionBases;
        Actions _actions;
        InputAction _restart;
        bool _inputEnabled;
        Rigidbody _body;

        public bool RestartPressed => _restart?.WasPressedThisFrame() == true;
        public bool QuitPressed => _inputEnabled && _actions.Ingame.Quit.WasPressedThisFrame();
        public float SpeedBeforePhysics { get; private set; }

        private void Awake()
        {
            _actions = new();
            _body = GetComponent<Rigidbody>();
            _restart = _actions.Ingame.Restart;
            _restart.Enable();

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
            if (_inputEnabled)
                foreach (var playerActionBase in _playerActionBases)
                    playerActionBase?.OnFixedUpdate();

            // 衝突で減速する前の移動速度を保持する
            SpeedBeforePhysics = _body != null ? _body.linearVelocity.magnitude : 0f;
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
            // カウントダウン中やリザルト表示中もリスタートは受け付ける
            _restart.Enable();
            GetComponent<PlayerCardboardLaunch>()?.HideGuide();
            if (TryGetComponent<Rigidbody>(out var body))
            {
                body.linearVelocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
            }
        }

        private void OnDestroy()
        {
            _actions?.Disable();
            _actions?.Dispose();
        }
    }
}
