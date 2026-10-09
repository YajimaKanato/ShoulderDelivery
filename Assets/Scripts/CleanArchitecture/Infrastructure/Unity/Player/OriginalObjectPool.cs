using System.Collections.Generic;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>自作オブジェクトプールクラス</summary>
    /// <typeparam name="T">プーリングしたいオブジェクト</typeparam>
    internal sealed class OriginalObjectPool<T> where T : class
    {
        Queue<T> _queue;
        T _originalObject;
        int _capacity;

        public OriginalObjectPool(T originalObject, int capacity)
        {
            _originalObject = originalObject;
            _capacity = capacity;
            _queue = new Queue<T>(capacity);
        }

        /// <summary>
        /// オブジェクト生成するときにを再利用できるか判定するメソッド
        /// </summary>
        /// <param name="originalObject">生成するオブジェクト</param>
        /// <returns>再利用できるか</returns>
        public bool TryRecycleObject(out T originalObject)
        {
            // 再利用できるかどうか判定
            var recycleObject = _queue.Count > 0;
            originalObject = recycleObject ? _queue.Dequeue() : _originalObject;

            return recycleObject;
        }

        /// <summary>
        /// プールに戻すメソッド
        /// </summary>
        /// <param name="originalObject">プールに戻すオブジェクト</param>
        /// <returns>プールに戻せたかどうか</returns>
        public bool TryReleaseToPool(T originalObject)
        {
            var releaseToPool = _queue.Count < _capacity;
            if (releaseToPool) _queue.Enqueue(originalObject);

            return releaseToPool;
        }
    }
}
