using UnityEngine;
using UnityEngine.InputSystem;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>プレイヤーの移動に関するクラス</summary>
    public class PlayerMove : PlayerActionBase
    {
        [Header("移動に関するデータ")]
        [SerializeField, Tooltip("アクセルのパラメータカーブ")] ParameterCurve _accelParam;
        [SerializeField, Tooltip("後輪にかかるモーターの強さ")] float _accelPower = 10;
        [SerializeField, Tooltip("ブレーキのパラメータカーブ")] ParameterCurve _brakeParam;
        [SerializeField, Tooltip("ブレーキにかかる力")] float _brakePower = 5;

        [Header("旋回に関するデータ")]
        [SerializeField, Tooltip("最大の旋回角度")] float _maxSteerAngle = 45f;
        [SerializeField, Tooltip("右に旋回するときのパラメータカーブ")] ParameterCurve _rightSteerCurve;
        [SerializeField, Tooltip("左に旋回するときのパラメータカーブ")] ParameterCurve _leftSteerCurve;

        [Header("車体に関するデータ")]
        [SerializeField, Tooltip("車体の傾き角度")] float _maxVehicleAngle = 30;
        [SerializeField, Tooltip("車体のオブジェクト")] Transform _vehicle;
        [SerializeField, Tooltip("ハンドルの傾き角度")] float _maxHandleAngle = 30;
        [SerializeField, Tooltip("ハンドルオブジェクト")] Transform _handleTransform;
        [SerializeField, Tooltip("カメラの傾き角度")] float _maxCameraAngle = 30;
        [SerializeField, Tooltip("カメラ角度")] Transform _cameraAngle;

        PlayerCamera _playerCamera;
        PlayerController _playerController;
        Rigidbody _rb;
        InputAction _accel;
        InputAction _brake;
        InputAction _steer;
        float _moveSpeed = 0;
        float _steerParam = 0;

        public override void CacheAction(Actions actions = null)
        {
            base.CacheAction(actions);
            _accel = _actions.Ingame.Accel;
            _brake = _actions.Ingame.Brake;
            _steer = _actions.Ingame.Steer;
        }

        public override void OnUpdate()
        {
            UpdateMove();
        }

        public override void OnFixedUpdate()
        {
            FixedUpdateMove();
        }

        private void LateUpdate()
        {
            UpdateVehicleRotation();
        }

        /// <summary>
        /// 移動に関するパラメータの更新
        /// </summary>
        void UpdateMove()
        {
            _moveSpeed = Accel() * _accelPower - Brake() * _brakePower;
            _steerParam = _maxSteerAngle * Steer();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!isActiveAndEnabled) return;

            // 接触面の法線は相手からプレイヤーへ向くので、符号を反転して前後を判定する
            var direction = 0f;
            for (int i = 0; i < collision.contactCount; i++)
            {
                var contactDirection = -Vector3.Dot(transform.forward, collision.GetContact(i).normal);
                if (Mathf.Abs(contactDirection) > Mathf.Abs(direction))
                    direction = contactDirection;
            }

            if (direction > 0.1f)
            {
                _accelParam.ResetDelta();
                _moveSpeed = Mathf.Min(0f, _moveSpeed);
            }
            else if (direction < -0.1f)
            {
                _brakeParam.ResetDelta();
                _moveSpeed = Mathf.Max(0f, _moveSpeed);
            }
            else return;

            // 市民との衝突では Citizen 側でシェイクする
            if (collision.collider.GetComponentInParent<Citizen>() != null) return;

            _playerCamera ??= GetComponent<PlayerCamera>();
            _playerController ??= GetComponent<PlayerController>();
            if (_playerCamera != null && _playerController != null)
                _playerCamera.ShakeOnHit(_playerController.SpeedBeforePhysics);
        }

        void FixedUpdateMove()
        {
            _rb ??= GetComponent<Rigidbody>();

            // ステアリング角度に応じて旋回
            _rb.angularVelocity = Vector3.up * _steerParam * Mathf.Deg2Rad * Mathf.Clamp(_moveSpeed, -1, 1);

            // 正面方向に前進
            var veloY = _rb.linearVelocity.y;
            var newVelo = transform.forward * _moveSpeed + Vector3.up * veloY;
            _rb.linearVelocity = newVelo;
        }

        /// <summary>
        /// 車体の傾きを調整するメソッド
        /// </summary>
        void UpdateVehicleRotation()
        {
            // 車体の傾き
            var vehicleRotation = _vehicle.localEulerAngles;
            vehicleRotation.z = -_maxVehicleAngle * Steer();
            _vehicle.localEulerAngles = vehicleRotation;

            // ハンドルの傾き
            var handleRotation = _handleTransform.localEulerAngles;
            handleRotation.y = _maxHandleAngle * Steer();
            _handleTransform.localEulerAngles = handleRotation;

            // カメラの傾き
            var cameraAngle = _cameraAngle.localEulerAngles;
            cameraAngle.z = -_maxCameraAngle * Steer();
            _cameraAngle.localEulerAngles = cameraAngle;
        }

        #region Calculate
        /// <summary>
        /// アクセルの入力を処理するメソッド
        /// </summary>
        /// <returns>現在のアクセル値</returns>
        float Accel()
        {
            var accel = _accel.IsPressed() ? 1 : 0;
            return _accelParam.CalculateParam(Time.deltaTime, new CurveParam(accel));
        }

        /// <summary>
        /// ブレーキの入力を処理するメソッド
        /// </summary>
        /// <returns>現在のブレーキ値</returns>
        float Brake()
        {
            var brake = _brake.IsPressed() ? 1 : 0;
            return _brakeParam.CalculateParam(Time.deltaTime, new CurveParam(brake));
        }

        /// <summary>
        /// ステアリング入力を処理するメソッド
        /// </summary>
        /// <returns>現在のステアリング値</returns>
        float Steer()
        {
            var input = _steer.ReadValue<float>();
            var right = _rightSteerCurve.CalculateParam(Time.deltaTime, new CurveParam(input));
            var left = _leftSteerCurve.CalculateParam(Time.deltaTime, new CurveParam(-input));
            return right - left;
        }
        #endregion

    }
}
