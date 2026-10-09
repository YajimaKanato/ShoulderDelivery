using ShoulderDelivery.Entity;
using ShoulderDerivery.Common;
using System;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardView : MonoBehaviour
    {
        [SerializeField, Tooltip("段ボールの重さ")] CardboardWeight _cardboardWeight;

        TrailRenderer _trailRender;
        Rigidbody _rb;
        CardboardId _cardboardId;

        event Action<CardboardView> _onReleaseToPool;
        public Action OnReleaseToPool(Action<CardboardView> act)
        {
            _onReleaseToPool += act;
            return () => _onReleaseToPool -= act;
        }

        public CardboardWeight CardboardWeight => _cardboardWeight;

        /// <summary>
        /// 段ボールを射出するメソッド
        /// </summary>
        /// <param name="cardboardId">段ボールのID</param>
        /// <param name="pos">段ボールを投げる位置</param>
        /// <param name="rot">投げる時の回転</param>
        /// <param name="velo">投げる時の速度</param>
        public void Launch(CardboardId cardboardId
            , Vector3 pos
            , Quaternion rot
            , Vector3 velo)
        {
            _cardboardId = cardboardId;

            // 位置を調整
            transform.SetPositionAndRotation(pos, rot);

            SetUpRigidbody(velo);

            SetUpTrail();
        }

        /// <summary>
        /// 軌跡を調整するメソッド
        /// </summary>
        void SetUpTrail()
        {
            _trailRender ??= GetComponent<TrailRenderer>();
            _trailRender.Clear();
            _trailRender.emitting = true;
        }

        /// <summary>
        /// 物理を調整するメソッド
        /// </summary>
        /// <param name="velo">打ち出す速度</param>
        void SetUpRigidbody(Vector3 velo)
        {
            _rb ??= GetComponent<Rigidbody>();
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.linearVelocity = velo;
        }

        /// <summary>
        /// プールに戻すメソッド
        /// </summary>
        void ReleaseToPool()
        {
            _onReleaseToPool?.Invoke(this);
        }

        private void OnCollisionEnter(Collision collision)
        {

        }
    }
}
