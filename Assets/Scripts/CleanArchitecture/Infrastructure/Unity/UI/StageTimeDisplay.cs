using System;

namespace ShoulderDelivery.Infrastructure
{
    internal static class StageTimeDisplay
    {
        public static int GetRemainingSeconds(float remainingTime)
            => (int)Math.Ceiling(Math.Max(0f, remainingTime));

        public static string Format(int seconds) => $"{seconds / 60}:{seconds % 60:00}";
    }
}
