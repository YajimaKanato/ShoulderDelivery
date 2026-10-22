using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>プレイヤーの行動を行うベースクラス</summary>
    public abstract class PlayerActionBase : MonoBehaviour
    {
        /// <summary>アクションをキャッシュするメソッド</summary>
        /// <param name="actions">アクションクラス</param>
        public abstract void CacheAction(Actions actions = null);

        /// <summary>毎フレーム処理するメソッド</summary>
        public virtual void OnUpdate() { }

        /// <summary>毎固定フレーム処理するメソッド</summary>
        public virtual void OnFixedUpdate() { }
    }
}
