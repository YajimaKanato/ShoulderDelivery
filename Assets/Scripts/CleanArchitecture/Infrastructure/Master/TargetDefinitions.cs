using System.Collections.Generic;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "TargetDefinitions", menuName = "Master/TargetDefinitions")]
    public class TargetDefinitions : ScriptableObject
    {
        [SerializeField] TargetDefinitionAsset[] _definitions;

        public IReadOnlyList<TargetDefinitionAsset> Definitions => _definitions;
    }
}
