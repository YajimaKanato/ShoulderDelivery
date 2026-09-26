using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの成功結果を保持するDTO</summary>
    public readonly struct GameClearOutput
    {
        public readonly int TotalScore;
        public readonly int RequiredDeliveryCount;
        public readonly int DeliveryCount;
        public readonly int TimeLimitSeconds;
        public readonly float RemainingTime;

        public GameClearOutput(int totalScore
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
