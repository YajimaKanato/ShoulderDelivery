using System;

namespace ShoulderDelivery.Entity
{
    public sealed class ScoreRankRule : IEntity
    {
        public static ScoreRankRule Default { get; } = new(800, 600, 400, 200);

        readonly int _minimumS;
        readonly int _minimumA;
        readonly int _minimumB;
        readonly int _minimumC;

        public int MinimumS => _minimumS;
        public int MinimumA => _minimumA;
        public int MinimumB => _minimumB;
        public int MinimumC => _minimumC;

        public ScoreRankRule(int minimumS, int minimumA, int minimumB, int minimumC)
        {
            _minimumC = Math.Max(0, minimumC);
            _minimumB = Math.Max(_minimumC, minimumB);
            _minimumA = Math.Max(_minimumB, minimumA);
            _minimumS = Math.Max(_minimumA, minimumS);
        }

        public ScoreRank Evaluate(int score)
        {
            if (score >= _minimumS) return ScoreRank.S;
            if (score >= _minimumA) return ScoreRank.A;
            if (score >= _minimumB) return ScoreRank.B;
            if (score >= _minimumC) return ScoreRank.C;
            return ScoreRank.D;
        }
    }
}
