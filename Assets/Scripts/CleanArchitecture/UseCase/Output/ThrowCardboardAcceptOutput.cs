using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲成功結果を持つDTO</summary>
    public readonly struct ThrowCardboardAcceptOutput
    {
        public readonly CardboardId CardboardId;

        public ThrowCardboardAcceptOutput(CardboardId cardboardId)
        {
            CardboardId = cardboardId;
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
    }
}
