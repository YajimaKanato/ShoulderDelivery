using ShoulderDerivery.Common;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールを投擲する機能を持つインターフェース</summary>
    public interface IThrowingGuidable
    {
        /// <summary>投擲する段ボールの重さを更新するメソッド</summary>
        /// <param name="weight">投擲する段ボールの重さ</param>
        void UpdateThrowCardboardWeight(CardboardWeight weight);
    }
}
