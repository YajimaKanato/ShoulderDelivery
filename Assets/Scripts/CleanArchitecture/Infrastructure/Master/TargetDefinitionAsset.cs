using ShoulderDelivery.Entity;
using ShoulderDerivery.Common;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "Target", menuName = "Master/TargetDefinitionAsset")]
    public class TargetDefinitionAsset : MasterAssetBase<TargetDefinition>
    {
        [SerializeField, Tooltip("配達目的地のID")] string _targetId = "Target";
        [SerializeField, Tooltip("求める段ボールの重さ")] CardboardWeight _requestedWeight;
        Vector3 _position;

        /// <summary>
        /// <para>シーン上の座標を設定するメソッド</para>
        /// <para>アサインしたタイミングで呼び出される想定</para>
        /// </summary>
        /// <param name="position">シーン上の座標</param>
        public void SetPosition(Vector3 position)
        {
            _position = position;
        }

        public override TargetDefinition ToEntity()
        {
            var targetId = new TargetId(_targetId);
            var coordinates = new Coordinates(_position.x, _position.y, _position.z);

            return new TargetDefinition(targetId, coordinates, _requestedWeight);
        }
    }
}
