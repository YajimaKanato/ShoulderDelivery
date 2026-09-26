using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    internal static class UILogger
    {
        /// <summary>
        /// アサインされていないことを出力するメソッド
        /// </summary>
        /// <param name="context">アサインされていないフィールド</param>
        internal static void LogNotAssigned(Object context)
        {
            Debug.LogWarning("アサインされていません", context);
        }
    }
}
