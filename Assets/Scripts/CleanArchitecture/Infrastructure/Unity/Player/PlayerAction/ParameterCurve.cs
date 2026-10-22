using System;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>パラメータの管理をするクラス</summary>
    [Serializable]
    internal class ParameterCurve
    {
        [SerializeField, Tooltip("パラメータの変化")] AnimationCurve _speedCurve;
        [SerializeField, Tooltip("最大入力時に最大パラメータに到達するまでの時間")] float _maxParamDuration = 1;
        [SerializeField, Tooltip("入力がないときの減衰率")] float _damping = 0.5f;
        float _delta = 0;

        internal float CalculateParam(float delta, CurveParam input)
        {
            var param = input.Value;
            // 何秒後に最大値を取るかに合わせて
            var deltaTime = delta / _maxParamDuration;
            // _maxParamDuration秒かけて最大パラメータになるように数値を加算
            _delta += deltaTime * param;
            _delta = Mathf.Clamp(_delta, 0, param);

            // 入力がなければパラメータ減衰
            _delta -= deltaTime * _damping * (input.IsPressed() ? 1 : 0);
            _delta = Mathf.Max(_delta, 0);

            return _speedCurve.Evaluate(_delta);
        }
    }
}
