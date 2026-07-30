using System.Collections.Generic;

namespace ShoulderDelivery.Entity
{
    /// <summary>投擲された段ボールの状態について管理するクラス</summary>
    public sealed class InFlightCardboardState
    {
        readonly Dictionary<CardboardId, ThrowContext> _contexts = new();

        /// <summary>
        /// 投げた情報を登録するメソッド
        /// </summary>
        /// <param name="id">情報を登録する段ボールのID</param>
        /// <param name="context">投げた情報</param>
        public void RegisterThrowContext(CardboardId id, ThrowContext context)
        {
            _contexts[id] = context;
        }

        /// <summary>
        /// 段ボールが消えた時に紐づく情報を処理するメソッド
        /// </summary>
        /// <param name="id">消えた段ボール</param>
        /// <param name="context">投げた情報</param>
        /// <returns>正常に処理できたか</returns>
        public bool TryResolve(CardboardId id, out ThrowContext context)
        {
            if (!_contexts.TryGetValue(id, out context)) return false;

            _contexts.Remove(id);
            return true;
        }
    }
}
