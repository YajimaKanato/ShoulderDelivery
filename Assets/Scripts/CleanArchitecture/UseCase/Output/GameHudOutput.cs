using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの現在の状況を保持するDTO</summary>
    public readonly struct GameHudOutput
    {
        public readonly float RemainingTime;
        public readonly int RemainingDeliveryCount;
        public readonly int Score;

        public GameHudOutput(float remainingTime
            , int remainingDeliveryCount
            , int score)
        {
            if (remainingTime < 0)
                throw new ArgumentOutOfRangeException(nameof(remainingTime));

            if (remainingDeliveryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(remainingDeliveryCount));

            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score));

            RemainingTime = remainingTime;
            RemainingDeliveryCount = remainingDeliveryCount;
            Score = score;
        }
    }
}
