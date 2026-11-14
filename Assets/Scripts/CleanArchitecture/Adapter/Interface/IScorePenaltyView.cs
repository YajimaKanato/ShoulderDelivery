namespace ShoulderDelivery.Adapter
{
    /// <summary>減点を表示する機能を持つインターフェース</summary>
    public interface IScorePenaltyView
    {
        /// <summary>市民に衝突したときの減点を表示するメソッド</summary>
        /// <param name="deductedScore">実際に減点した点数</param>
        void ShowCitizenHitPenalty(int deductedScore);
    }
}
