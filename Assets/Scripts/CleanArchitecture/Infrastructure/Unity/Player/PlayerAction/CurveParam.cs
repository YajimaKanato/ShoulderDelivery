using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>カーブのパラメータオブジェクト</summary>
    internal readonly struct CurveParam
    {
        internal readonly float Value;

        internal CurveParam(float value)
        {
            Value = Mathf.Clamp01(value);
        }

        /// <summary>入力があるかどうかを返すメソッド</summary>
        /// <returns>入力があるかどうか</returns>
        internal bool IsPressed() => Value != 0;
    }
}
