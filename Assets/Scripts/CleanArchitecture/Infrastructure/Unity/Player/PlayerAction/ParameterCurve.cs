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

        /// <summary>
        /// パラメータを計算するメソッド
        /// </summary>
        /// <param name="delta">フレーム時間</param>
        /// <param name="input">入力量</param>
        /// <returns>現在のパラメータ</returns>
        internal float CalculateParam(float delta, CurveParam input)
        {
            var param = input.Value;
            // 何秒後に最大値を取るかに合わせて
            var deltaTime = delta / _maxParamDuration;

            // _maxParamDuration秒かけて最大パラメータになるように数値を加算
            _delta += deltaTime * param;

            if (input.IsPressed())
                _delta = Mathf.Min(_delta, param);
            else
                _delta -= deltaTime * _damping;

            _delta = Mathf.Max(_delta, 0);

            return _speedCurve.Evaluate(_delta);
        }

        /// <summary>
        /// パラメータを初期化するメソッド
        /// </summary>
        internal void ResetDelta()
        {
            _delta = 0;
        }
    }
}
