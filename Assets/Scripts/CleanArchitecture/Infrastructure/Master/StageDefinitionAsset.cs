using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "StageDefinitionAsset", menuName = "Master/StageDefinitionAsset")]
    public class StageDefinitionAsset : ScriptableObject
    {
        [SerializeField] string _stageId;
        [SerializeField] int _countDownSeconds = 3;
        [SerializeField] int _timeLimitSeconds = 60;
        [SerializeField] int _requiredDeliveryCount = 3;
    }
}
