using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲結果のDTOを生成する役割を持つクラス</summary>
    internal static class ThrowCardboardOutputService
    {
        /// <summary>
        /// 投擲失敗を生成する
        /// </summary>
        /// <returns>投擲失敗DTO</returns>
        internal static ThrowCardboardRejectedOutput Rejected()
        {
            return new ThrowCardboardRejectedOutput();
        }

        /// <summary>
        /// 投擲成功を生成する
        /// </summary>
        /// <param name="id">投擲した段ボールのID</param>
        /// <returns>投擲成功DTO</returns>
        internal static ThrowCardboardAcceptOutput Accepted(CardboardId id)
        {
            return new ThrowCardboardAcceptOutput(id);
        }

        /// <summary>
        /// 投擲を実行するときに必要な情報を生成するメソッド
        /// </summary>
        /// <param name="input">投擲実行に必要な情報</param>
        /// <returns>投擲実行に必要な情報</returns>
        internal static ThrowCardboardOutput Output(ThrowCardboardInput input)
        {
            return new ThrowCardboardOutput(input.Context, input.Launch);
        }
    }
}
