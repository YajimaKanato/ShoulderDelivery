using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>Vector3に拡張メソッドを実装するクラス</summary>
    internal static class Vector3Converter
    {
        /// <summary>
        /// Vector3をCoordinatesに変換するメソッド
        /// </summary>
        /// <param name="v">変換する座標</param>
        /// <returns>変換後のデータ</returns>
        internal static Coordinates ToCoordinates(this Vector3 v)
        {
            return new Coordinates(v.x, v.y, v.z);
        }
    }
}
