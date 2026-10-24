using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>次の配達先の情報を持つDTO</summary>
    public readonly struct NextDeliveryTargetOutput
    {
        public readonly TargetId TargetId;

        public NextDeliveryTargetOutput(TargetId targetId)
        {
            TargetId = targetId;
        }

        /// <summary>
        /// DTOを生成するメソッド
        /// </summary>
        /// <param name="targetId">ターゲットのID</param>
        /// <returns>DTO</returns>
        public static NextDeliveryTargetOutput GenerateOutput(TargetId targetId)
        {
            return new NextDeliveryTargetOutput(targetId);
        }
    }
}
