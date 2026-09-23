using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>カウントダウンの情報を持つDTO</summary>
    public readonly struct CountDownOutput
    {
        /// <summary>カウントダウンの現在時間</summary>
        public readonly float Seconds;

        public CountDownOutput(float seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException(nameof(seconds));

            Seconds = seconds;
        }
    }
}
