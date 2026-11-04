using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class TargetView : MonoBehaviour
    {
        TargetId _targetId;
        public TargetId TargetId => _targetId;

        /// <summary>
        /// IDを設定するメソッド
        /// </summary>
        /// <param name="targetId">ID</param>
        public void SetTargetId(TargetId targetId)
        {
            _targetId = targetId;
        }
    }
}
