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
        [SerializeField, Min(0), Tooltip("右スティックを離した時に正面へ戻る速さ（度/秒）。0なら戻さない")]
        float _controllerReturnSpeed = 60f;
        InputAction _camera;
        bool _useGamepad;
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

            // スティックを離してもコントローラー操作の状態を保持する
            if (_camera.activeControl != null)
                _useGamepad = _camera.activeControl.device is Gamepad;

            // 横方向の首振り角度
            if (_useGamepad)
            {
                _yawDelta += look.x * Time.deltaTime * _controllerSensitivity * _shakeMaxRange;
            }
            else
            {
                _yawDelta += look.x * _shakeMaxRange * _mouseSensitivity;
            }
            _yawDelta = Mathf.Clamp(_yawDelta, -_shakeMaxRange, _shakeMaxRange);

            // 縦方向の首振り角度
            if (_useGamepad)
            {
                _pitchDelta += look.y * Time.deltaTime * _controllerSensitivity * _nodMaxRange;
            }
            else
            {
                _pitchDelta += look.y * _nodMaxRange * _mouseSensitivity;
            }
            _pitchDelta = Mathf.Clamp(_pitchDelta, -_nodMaxRange, _nodMaxRange);

            // 入力がなくなったら、指定した速度で正面に戻す
            if (_useGamepad && look.sqrMagnitude < 0.0001f)
            {
                var rotation = Vector2.MoveTowards(new Vector2(_yawDelta, _pitchDelta)
                    , Vector2.zero, _controllerReturnSpeed * Time.deltaTime);
                _yawDelta = rotation.x;
                _pitchDelta = rotation.y;
            }

            _cameraTransform.localRotation =
                Quaternion.Euler(
                    -_pitchDelta
                    , _yawDelta
                    , 0);
        }
    }
}
