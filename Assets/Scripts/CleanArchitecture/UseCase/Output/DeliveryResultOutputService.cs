using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達結果のDTOを生成する処理を持つクラス</summary>
    public static class DeliveryResultOutputService
    {
        public static DeliveryResultOutput Delivered(ScoreBreakdown scoreBreakdown, int score)
        {
            return new DeliveryResultOutput(scoreBreakdown, score);
        }

        public static DeliveryResultOutput Missed()
        {
            return new DeliveryResultOutput(null, 0);
        }
    }
}
