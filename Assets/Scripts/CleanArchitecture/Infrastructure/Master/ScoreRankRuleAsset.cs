using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "ScoreRankRule", menuName = "Master/ScoreRankRuleAsset")]
    public sealed class ScoreRankRuleAsset : MasterAssetBase<ScoreRankRule>
    {
        [SerializeField, Min(0)] int _minimumS = 800;
        [SerializeField, Min(0)] int _minimumA = 600;
        [SerializeField, Min(0)] int _minimumB = 400;
        [SerializeField, Min(0)] int _minimumC = 200;

        public override ScoreRankRule ToEntity() => new(_minimumS, _minimumA, _minimumB, _minimumC);

        void OnValidate()
        {
            _minimumC = Mathf.Max(0, _minimumC);
            _minimumB = Mathf.Max(_minimumC, _minimumB);
            _minimumA = Mathf.Max(_minimumB, _minimumA);
            _minimumS = Mathf.Max(_minimumA, _minimumS);
        }
    }
}
