using ShoulderDerivery.Common;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールを飛ばす機能を持つインターフェース</summary>
    public interface ICardboardLauncher
    {
        /// <summary>段ボールを飛ばすメソッド</summary>
        /// <param name="output">投擲時の情報</param>
        void LaunchCardboard(ThrowCardboardOutput output);

        /// <summary>次に投擲する段ボールの重さを設定するメソッド</summary>
        /// <param name="weight">次に投擲する段ボールの重さ</param>
        void SetNextThrowingCardboard(CardboardWeight weight);
    }
}
