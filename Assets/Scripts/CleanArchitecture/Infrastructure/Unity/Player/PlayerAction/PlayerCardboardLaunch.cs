using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace ShoulderDelivery.Infrastructure
{
    public class PlayerCardboardLaunch : PlayerActionBase
    {
        [Header("投擲関連のデータ")]
        [SerializeField, Tooltip("投擲力")] float _throwingPower = 10;
        [SerializeField, Tooltip("投擲開始位置")] Transform _throwPos;
        [SerializeField, Tooltip("ガイドラインを表示するクラス")] GuideLineRenderer _guideLineRenderer;

        Rigidbody _rb;
        CardboardUseCase _useCase;
        InputAction _lockOn;
        InputAction _throw;

        /// <summary>
        /// 必要なUseCase参照をセットするメソッド
        /// </summary>
        /// <param name="useCase">UseCase参照</param>
        /// <exception cref="ArgumentNullException">参照が不足している</exception>
        public void SetCardboardUseCase(CardboardUseCase useCase)
        {
            _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        }

        public override void CacheAction(Actions actions = null)
        {
            base.CacheAction(actions);

            _lockOn = _actions.Ingame.LockOn;
            _throw = _actions.Ingame.Throw;
        }

        public override void OnUpdate()
        {
            LockOn();
            Launch();
        }

        /// <summary>
        /// 段ボールを投擲するメソッド
        /// </summary>
        void Launch()
        {
            if (_throw.WasPressedThisFrame())
            {
                _rb ??= GetComponent<Rigidbody>();

                // 投擲に必要な情報を生成
                var throwContext = new ThrowContext(_rb.linearVelocity.magnitude
                    , _rb.position.ToCoordinates()
                    , _rb.rotation.eulerAngles.ToCoordinates());
                var cardboardLaunch = new CardboardLaunch(_throwPos.position.ToCoordinates()
                    , _throwPos.localEulerAngles.ToCoordinates()
                    , _throwPos.forward.ToCoordinates()
                    , _throwingPower);

                // 投擲
                _useCase?.Throw(new ThrowCardboardInput(throwContext, cardboardLaunch));
            }
        }

        void LockOn()
        {
            if (_lockOn.IsPressed())
            {
                _guideLineRenderer?.EnableMesh(_throwPos.position, _throwPos.forward * _throwingPower);
            }
            else if (_lockOn.WasReleasedThisFrame())
            {
                _guideLineRenderer?.DisableMesh();
            }
        }
    }
}
