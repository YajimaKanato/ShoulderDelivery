namespace ShoulderDelivery.Entity
{
    /// <summary>合計スコアのクラス</summary>
    public sealed class Score
    {
        int _score;

        /// <summary>合計スコア</summary>
        public int Total => _score;

        /// <summary>
        /// スコアを加算するメソッド
        /// </summary>
        /// <param name="score">計算タイミングごとのスコアのまとまり</param>
        public void AddScore(ScoreBreakdown score)
        {
            _score += score.Total;
        }

        /// <summary>スコアにペナルティを適用するメソッド</summary>
        /// <param name="amount">ペナルティの点数</param>
        /// <returns>実際に減点した点数</returns>
        public int ApplyPenalty(int amount)
        {
            if (amount < 0)
                throw new System.ArgumentOutOfRangeException(nameof(amount));

            var deductedScore = System.Math.Min(_score, amount);
            _score -= deductedScore;
            return deductedScore;
        }
    }
}
