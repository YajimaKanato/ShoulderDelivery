using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの結果を保持するDTO</summary>
    public readonly struct GameResultOutput
    {
        public readonly int Total;
        public readonly int DeliveryCount;
        public readonly float RemainingTime;
        public readonly bool IsQuataMet;

        public GameResultOutput(int total, int deliveryCount, float remainingTime, bool isQuataMet)
        {
            if (total < 0)
                throw new ArgumentOutOfRangeException(nameof(total));

            if (deliveryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(deliveryCount));

            if (remainingTime < 0)
                throw new ArgumentOutOfRangeException(nameof(remainingTime));

            Total = total;
            DeliveryCount = deliveryCount;
            RemainingTime = remainingTime;
            IsQuataMet = isQuataMet;
        }
    }
}
