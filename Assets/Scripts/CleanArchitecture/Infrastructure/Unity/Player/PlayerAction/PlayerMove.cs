using UnityEngine;
using UnityEngine.InputSystem;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>プレイヤーの移動に関するクラス</summary>
    public class PlayerMove : PlayerActionBase
    {
        [Header("移動に関するデータ")]
        [SerializeField, Tooltip("固定の旋回角度")] float _baseSteerAngle = 5f;
        [SerializeField, Tooltip("最大の旋回角度")] float _maxSteerAngle = 45f;
        [SerializeField, Tooltip("移動速度に合わせた旋回角度")] AnimationCurve _steerAnglePerSpeed;
        [SerializeField, Tooltip("アクセルのパラメータカーブ")] ParameterCurve _accelParam;
        [SerializeField, Tooltip("ブレーキのパラメータカーブ")] ParameterCurve _brakeParam;
        [SerializeField, Tooltip("後輪にかかるモーターの強さ")] float _motorPower = 1000;

        [Header("車体に関するデータ")]
        [SerializeField, Tooltip("車体の傾き角度")] float _maxVehicleAngle = 30;
        [SerializeField, Tooltip("車体のオブジェクト")] Transform _vehicle;
        [SerializeField, Tooltip("ハンドルオブジェクト")] Transform _handleTransform;
        [SerializeField, Tooltip("車体の傾きの中心")] Transform _pivot;

        Rigidbody _rb;
        Actions _actions;
        InputAction _accel;
        InputAction _brake;
        InputAction _steer;
        float _moveParam = 0;
        float _steerParam = 0;

        public override void CacheAction(Actions actions = null)
        {
            _actions = actions ?? new();
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

        /// <summary>
        /// 移動に関するパラメータの更新
        /// </summary>
        void UpdateMove()
        {
            var accel = Accel();
            var brake = Brake();
            _moveParam = accel - brake;
            _steerParam = (Mathf.Max(_steerAnglePerSpeed.Evaluate(Mathf.Abs(_moveParam)), _baseSteerAngle)) * Steer();
        }

        void FixedUpdateMove()
        {
            _rb ??= GetComponent<Rigidbody>();

            // ステアリング角度に応じて旋回
            _rb.angularVelocity = Vector3.up * _steerParam;

            // 正面方向に前進
            _rb.linearVelocity = transform.forward * (_moveParam + _steerParam) * _motorPower;
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
            return input;
        }
        #endregion
    }
}
