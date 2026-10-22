using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>保管庫の機能のベースインターフェース</summary>
    public interface IRepository<TEntity, TKey> where TEntity : IEntity where TKey : IEquatable<TKey>
    {
        /// <summary>要素を取得するメソッド</summary>
        /// <param name="key">取得する要素のキー</param>
        /// <returns>要素</returns>
        TEntity Get(TKey key);
    }
}
