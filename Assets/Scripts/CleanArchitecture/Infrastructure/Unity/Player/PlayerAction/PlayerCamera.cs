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

            var useGamepad = Gamepad.current != null
                && Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.01;

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

            _cameraTransform.localRotation =
                Quaternion.Euler(
                    -_pitchDelta
                    , _yawDelta
                    , 0);
        }
    }
}
