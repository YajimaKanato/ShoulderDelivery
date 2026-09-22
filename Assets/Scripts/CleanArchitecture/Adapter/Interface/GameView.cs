using UnityEngine;

namespace ShoulderDelivery.Adapter
{
    /// <summary>画面に描画するオブジェクトに実装する抽象クラス</summary>
    /// <para>インターフェースによる依存性逆転をMonoBehaviourを継承したクラスで代替</para>
    public abstract class GameView : MonoBehaviour
    {
        /// <summary>コントローラーの有効無効を切り替えるメソッド</summary>
        /// <param name="enabled">コントローラーの有効無効</param>
        public abstract void SetControllerEnabled(bool enabled);
    }
}
