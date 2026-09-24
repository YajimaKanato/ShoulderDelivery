using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>配達時の情報を持つデータを生成する機能を持つクラス</summary>
    public static class DeliveryBreakdownService
    {
        /// <summary>
        /// 配達時の情報を持つデータの塊を生成するメソッド
        /// </summary>
        /// <param name="deliveryState">現在の配達情報を管理するクラスの参照</param>
        /// <returns>配達時の情報を持つデータ</returns>
        /// <exception cref="ArgumentNullException">必要な参照がない</exception>
        public static DeliveryBreakdown Deliveried(DeliveryState deliveryState)
        {
            if (deliveryState == null)
                throw new ArgumentNullException(nameof(deliveryState));

            var deliveryCount = deliveryState.DeliveredCount;
            var requiredDeliveryCount = deliveryState.RequiredDeliveryCount;
            var deliveryCombo = deliveryState.DeliveryCombo;

            return new DeliveryBreakdown(deliveryCount, requiredDeliveryCount, deliveryCombo);
        }
    }
}
