using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShoulderDelivery.Infrastructure
{
    public class PlayerCamera : PlayerActionBase
    {
        [Header("カメラの操作関連のパラメータ")]
        [SerializeField, Tooltip("カメラオブジェクト")] Transform _cameraTransform;
        [SerializeField, Tooltip("横方向の首振り制限")] float _shakeMaxRange = 45;
        [SerializeField, Tooltip("縦方向の首振り制限")] float _nodMaxRange = 30;
        [SerializeField, Tooltip("マウス感度")] float _mouseSensitivity = 0.01f;
        [SerializeField, Tooltip("コントローラー感度")] float _controllerSensitivity = 1f;
        InputAction _camera;
        float _yawDelta;
        float _pitchDelta;
        [SerializeField]
        CinemachineImpulseDefinition _hitImpulse = new()
        {
            ImpulseChannel = 1,
            ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Bump,
            ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Uniform,
            ImpulseDuration = 0.25f
        };
        [SerializeField, Min(0), Tooltip("衝突時のカメラシェイクの最大の強さ")] float _hitImpulseStrength = 0.2f;
        [SerializeField, Min(0.01f), Tooltip("衝突時の揺れが最大になる移動速度")] float _hitImpulseMaxSpeed = 14.95f;

        public void ShakeOnHit(float speed)
        {
            if (_hitImpulseStrength <= 0f || _hitImpulse.ImpulseDuration <= 0f) return;
            var strength = _hitImpulseStrength * Mathf.Clamp01(speed / _hitImpulseMaxSpeed);
            if (strength > 0f)
                _hitImpulse.CreateEvent(transform.position, Vector3.down * strength);
        }

        public override void CacheAction(Actions actions = null)
        {
            base.CacheAction(actions);
            _camera = _actions.Ingame.Camera;
        }

        public override void OnUpdate()
        {
            ShakeAndNod();
        }

        /// <summary>
        /// カメラの首振り（横方向）およびうなずき（縦方向）の角度を入力デバイスに応じて更新します。
        /// </summary>
        void ShakeAndNod()
        {
            var look = _camera.ReadValue<Vector2>();
            if (look.sqrMagnitude == 0f) return;

            var useGamepad = _camera.activeControl?.device is Gamepad;

            // 横方向の首振り角度
            if (useGamepad)
            {
                _yawDelta += look.x * Time.deltaTime * _controllerSensitivity * _shakeMaxRange;
            }
            else
            {
                _yawDelta += look.x * _shakeMaxRange * _mouseSensitivity;
            }
            _yawDelta = Mathf.Clamp(_yawDelta, -_shakeMaxRange, _shakeMaxRange);

            // 縦方向の首振り角度
            if (useGamepad)
            {
                _pitchDelta += look.y * Time.deltaTime * _controllerSensitivity * _nodMaxRange;
            }
            else
            {
                _pitchDelta += look.y * _nodMaxRange * _mouseSensitivity;
            }
            _pitchDelta = Mathf.Clamp(_pitchDelta, -_nodMaxRange, _nodMaxRange);

            _cameraTransform.localRotation = Quaternion.Euler(-_pitchDelta, _yawDelta, 0f);
        }
    }
}
