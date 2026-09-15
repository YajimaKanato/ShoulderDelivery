using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>マスターデータアセットのベースクラス</summary>
    /// <typeparam name="T">生成するEntityのデータ型</typeparam>
    public abstract class MasterAssetBase<T> : ScriptableObject where T : IEntity
    {
        /// <summary>
        /// Entityを生成するメソッド
        /// </summary>
        /// <returns>生成するEntity</returns>
        public abstract T ToEntity();
    }
}
