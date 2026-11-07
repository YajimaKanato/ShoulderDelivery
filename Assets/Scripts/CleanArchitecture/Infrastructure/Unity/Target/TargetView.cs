using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class TargetView : MonoBehaviour
    {
        [SerializeField] TargetDefinitionAsset _definition;
        TargetId _targetId;
        public TargetId TargetId => _targetId;

        public TargetDefinitionAsset BindDefinition()
        {
            SetTargetId(new TargetId(_definition.TargetId));
            _definition.SetPosition(transform.position);
            return _definition;
        }

        /// <summary>
        /// IDを設定するメソッド
        /// </summary>
        /// <param name="targetId">ID</param>
        void SetTargetId(TargetId targetId)
        {
            _targetId = targetId;
        }
    }
}
