using System;
using System.Collections.Generic;

namespace ShoulderDelivery.Entity
{
    /// <summary>ステージ全体の配送状況を持つクラス</summary>
    public sealed class DeliveryState
    {
        /// <summary>配達完了したターゲットのIDを持つコレクション</summary>
        readonly HashSet<TargetId> _deliveredTargetIds = new();
        /// <summary>配達待ちのターゲットのIDを持つコレクション</summary>
        readonly Queue<TargetId> _waitingDeliveryTargetIds = new();
        TargetId _currentTargetId;
        readonly int _requiredDeliveryCount;
        int _deliverCombo;

        /// <summary>現在のターゲット</summary>
        public TargetId CurrentTargetId => _currentTargetId;
        /// <summary>必要な配達数</summary>
        public int RequiredDeliveryCount => _requiredDeliveryCount;
        /// <summary>現在の配達数</summary>
        public int DeliveredCount => _deliveredTargetIds.Count;
        /// <summary>残りの配達数</summary>
        public int RemainigDeliveryCount => Math.Max(0, RequiredDeliveryCount - DeliveredCount);
        /// <summary>連続配達成功回数</summary>
        public int DeliveryCombo => _deliverCombo;
        /// <summary>ノルマ達成フラグ</summary>
        public bool IsQuataMet => DeliveredCount >= RequiredDeliveryCount;

        public DeliveryState(StageDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException(nameof(definition));

            _requiredDeliveryCount = definition.RequiredDeliveryCount;
            foreach (var targetId in definition.TargetIds)
            {
                _waitingDeliveryTargetIds.Enqueue(targetId);
            }

            if (_waitingDeliveryTargetIds.Count > 0) _currentTargetId = _waitingDeliveryTargetIds.Dequeue();
        }

        /// <summary>
        /// 配達をするメソッド
        /// </summary>
        /// <param name="id">配達先のID</param>
        /// <returns>配達できたか</returns>
        public bool TryDelivery(TargetId? id = null)
        {
            if (id != null && _currentTargetId == id.Value && _deliveredTargetIds.Add(id.Value))
            {
                _deliverCombo++;
                NextTarget();
                return true;
            }

            _deliverCombo = 0;
            return false;
        }

        /// <summary>
        /// 次の配達先を指定するメソッド
        /// </summary>
        void NextTarget()
        {
            if (_waitingDeliveryTargetIds.Count > 0)
                _currentTargetId = _waitingDeliveryTargetIds.Dequeue();
        }
    }
}
