using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達結果のDTOを生成する処理を持つクラス</summary>
    public static class DeliveryResultOutputService
    {
        public static DeliverySuccessOutput Delivered(ScoreBreakdown scoreBreakdown, int score, DeliveryBreakdown deliveryBreakdown)
        {
            return new DeliverySuccessOutput(scoreBreakdown, score, deliveryBreakdown);
        }

        public static DeliveryFailedOutput Missed()
        {
            return new DeliveryFailedOutput();
        }
    }
}
