namespace ShoulderDelivery.Adapter
{
    /// <summary>配達先の情報を更新する機能を持つインターフェース</summary>
    public interface ITargetView
    {
        /// <summary>次の配達先を表示するメソッド</summary>
        /// <param name="viewModel">配達先の情報</param>
        void UpdateTarget(NextTargetViewModel viewModel);
    }
}
