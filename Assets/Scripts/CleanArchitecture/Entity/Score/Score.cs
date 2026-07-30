using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>合計スコアの構造体</summary>
    public class Score
    {
        int _score;

        /// <summary>合計スコア</summary>
        public int Total => _score;

        /// <summary>
        /// スコアを加算するメソッド
        /// </summary>
        /// <param name="score">計算タイミングごとのスコアのまとまり</param>
        public void AddScore(ScoreBreakdown? score)
        {
            if (score == null)
                throw new ArgumentNullException(nameof(score));

            _score += score.Value.Total;
        }
    }
}
