using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの結果を保持するDTO</summary>
    public readonly struct GameResultOutput
    {
        public readonly int Total;
        public readonly int DeliveryCount;

        public GameResultOutput(int total, int deliveryCount)
        {
            if (total < 0)
                throw new ArgumentOutOfRangeException(nameof(total));

            if (deliveryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(deliveryCount));

            Total = total;
            DeliveryCount = deliveryCount;
        }
    }
}
