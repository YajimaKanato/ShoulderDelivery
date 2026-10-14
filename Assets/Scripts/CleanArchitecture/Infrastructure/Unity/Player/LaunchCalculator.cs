using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>射出時の計算をするクラス</summary>
    internal static class LaunchCalculator
    {
        /// <summary>
        /// 入力から位置ベクトルを計算するメソッド
        /// </summary>
        /// <param name="position">入力</param>
        /// <returns>位置ベクトル</returns>
        internal static Vector3 CalculatePosition(Coordinates position)
        {
            return new Vector3(position.X, position.Y, position.Z);
        }

        /// <summary>
        /// 入力（オイラー角）からQuaternionを計算するメソッド
        /// </summary>
        /// <param name="rotation">入力（オイラー角）</param>
        /// <returns>Quaternion</returns>
        internal static Quaternion CalculateRotation(Coordinates rotation)
        {
            return Quaternion.Euler(rotation.X, rotation.Y, rotation.Z);
        }

        /// <summary>
        /// 射出ベクトルを計算するメソッド
        /// </summary>
        /// <param name="direction">射出方向</param>
        /// <param name="speed">初速</param>
        /// <returns>射出ベクトル</returns>
        internal static Vector3 CalculateSpeed(Coordinates direction, float speed)
        {
            return new Vector3(direction.X, direction.Y, direction.Z) * speed;
        }
    }
}
