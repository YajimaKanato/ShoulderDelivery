using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの失敗結果を持つDTO</summary>
    public readonly struct GameFailedOutput
    {
        public readonly int TotalScore;
        public readonly int RequiredDeliveryCount;
        public readonly int DeliveryCount;
        public readonly int TimeLimitSeconds;
        public readonly float RemainingTime;

        public GameFailedOutput(int totalScore
            , int requiredDeliveryCount
            , int deliveryCount
            , int timeLimitSeconds
            , float remainingTime)
        {
            if (totalScore < 0)
                throw new ArgumentOutOfRangeException(nameof(totalScore));

            if (requiredDeliveryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(requiredDeliveryCount));

            if (deliveryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(deliveryCount));

            if (timeLimitSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(timeLimitSeconds));

            if (remainingTime < 0)
                throw new ArgumentOutOfRangeException(nameof(remainingTime));

            TotalScore = totalScore;
            RequiredDeliveryCount = requiredDeliveryCount;
            DeliveryCount = deliveryCount;
            TimeLimitSeconds = timeLimitSeconds;
            RemainingTime = remainingTime;
        }
    }
}
