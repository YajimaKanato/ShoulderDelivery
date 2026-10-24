using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>プレイヤーの行動を行うベースクラス</summary>
    public abstract class PlayerActionBase : MonoBehaviour
    {
        protected Actions _actions;

        /// <summary>アクションをキャッシュするメソッド</summary>
        /// <param name="actions">アクションクラス</param>
        public virtual void CacheAction(Actions actions = null)
        {
            _actions = actions ?? new();
        }

        /// <summary>毎フレーム処理するメソッド</summary>
        public virtual void OnUpdate() { }

        /// <summary>毎固定フレーム処理するメソッド</summary>
        public virtual void OnFixedUpdate() { }
    }
}
