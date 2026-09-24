using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達結果のDTOを生成する処理を持つクラス</summary>
    public static class DeliveryResultOutputService
    {
        /// <summary>
        /// 配達成功のDTOを生成するメソッド
        /// </summary>
        /// <param name="scoreBreakdown">スコア内訳</param>
        /// <param name="score">スコア</param>
        /// <param name="deliveryBreakdown">配達内訳</param>
        /// <returns>配達成功のDTO</returns>
        public static DeliverySuccessOutput Delivered(ScoreBreakdown scoreBreakdown, int score, DeliveryBreakdown deliveryBreakdown)
        {
            return new DeliverySuccessOutput(scoreBreakdown, score, deliveryBreakdown);
        }

        /// <summary>
        /// 配達失敗のDTOを生成するメソッド
        /// </summary>
        /// <param name="score">スコア</param>
        /// <returns>配達失敗のDTO</returns>
        public static DeliveryFailedOutput Missed(int score)
        {
            return new DeliveryFailedOutput(score);
        }
    }
}
