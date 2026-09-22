using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達結果のDTOを生成する処理を持つクラス</summary>
    public static class DeliveryResultOutputService
    {
        public static DeliverySuccessOutput Delivered(ScoreBreakdown scoreBreakdown, int score)
        {
            return new DeliverySuccessOutput(scoreBreakdown, score);
        }

        public static DeliveryFailedOutput Missed()
        {
            return new DeliveryFailedOutput();
        }
    }
}
