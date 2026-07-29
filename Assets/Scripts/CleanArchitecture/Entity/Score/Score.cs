namespace ShoulderDelivery.Entity
{
    /// <summary>合計スコアの構造体</summary>
    public class Score
    {
        /// <summary>合計スコア</summary>
        public int Total { get; private set; }

        /// <summary>
        /// スコアを加算するメソッド
        /// </summary>
        /// <param name="score">計算タイミングごとのスコアのまとまり</param>
        public void AddScore(ScoreBreakdown score)
        {
            Total += score.Total;
        }
    }
}
