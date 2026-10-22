using System;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>ログ出力を依頼するクラスのベース</summary>
    public abstract class ShowContextBase : MonoBehaviour
    {
        event Action<string, Color> _onShowContext;

        /// <summary>
        /// ログ出力のイベントに登録するメソッド
        /// </summary>
        /// <param name="act">登録するAction</param>
        /// <returns>登録解除メソッド</returns>
        public Action OnShowContext(Action<string, Color> act)
        {
            _onShowContext += act;
            return () => _onShowContext -= act;
        }

        /// <summary>
        /// イベントを発火するメソッド
        /// </summary>
        /// <param name="context">出力メッセージ</param>
        /// <param name="color">テキストカラー</param>
        protected void OnInvoke(string context, Color color) => _onShowContext?.Invoke(context, color);
    }
}
