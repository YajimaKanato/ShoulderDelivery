namespace ShoulderDelivery.Adapter
{
    /// <summary>配達結果を表示する機能を持つインターフェース</summary>
    public interface IDeliveriedView
    {
        /// <summary>配達成功を表示するメソッド</summary>
        /// <param name="context">配達結果</param>
        void ShowDeliverySucceededResult(DeliveriedViewModel context);

        /// <summary>配達失敗を表示するメソッド</summary>
        /// <param name="context">配達結果</param>
        void ShowDeliveryFailedResult(DeliveriedViewModel context);
    }
}
