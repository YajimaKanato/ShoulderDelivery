using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

namespace ShoulderDelivery.Infrastructure
{
    public class Citizen : MonoBehaviour
    {
        [SerializeField] SplineContainer _spline;
        [SerializeField] bool _isGreeting;
        Animator _animator;
        Transform _target;
        Vector3 _forward;
        bool _hitPlayer;
        bool _dancing;
        bool _walking;
        int _hitPlayerValue;

        public event Action OnPlayerHit;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _target = FindAnyObjectByType<PlayerController>()?.transform;
            _forward = transform.forward;
        }

        private void Update()
        {
            if (_dancing)
            {
                Vector3 direction = _target.position - transform.position;
                direction.y = 0;

                if (direction.sqrMagnitude > 0.0001f)
                {
                    transform.forward = direction;
                }
            }

            if (_walking)
            {

            }
        }

        /// <summary>
        /// 非同期で移動するメソッド
        /// </summary>
        /// <param name="start">開始割合</param>
        /// <param name="end">終了割合</param>
        /// <param name="duration">時間</param>
        /// <param name="ct">キャンセルトークン</param>
        /// <returns></returns>
        async UniTask MoveAsync(float start, float end, float duration, CancellationToken ct)
        {
            float elapsed = 0f;
            float3 position;

            // 時間までループ
            while (elapsed < duration)
            {
                // 踊っている間は移動しない
                await UniTask.WaitUntil(() => !_dancing);

                // キャンセルがリクエストされていたらここで中断
                ct.ThrowIfCancellationRequested();

                elapsed += Time.deltaTime;

                // 進行度計算
                float progress = Mathf.Clamp01(elapsed / duration);

                // 開始点と終了点を加味した進行度に変換する
                float t = Mathf.Lerp(start, end, progress);

                position = _spline.EvaluatePosition(t);
                transform.position = new Vector3(position.x, position.y, position.z);

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            position = _spline.EvaluatePosition(end);
            transform.position = new Vector3(position.x, position.y, position.z);
        }

        void EnterPlayer()
        {
            _dancing = true;
            _hitPlayer = true;
            _hitPlayerValue = UnityEngine.Random.Range(0, 2);
            OnPlayerHit?.Invoke();
        }

        void ExitPlayer()
        {
            _hitPlayer = false;
        }

        void EndDancing()
        {
            _dancing = _hitPlayer;
            transform.forward = _forward;
        }

        private void LateUpdate()
        {
            _animator?.SetBool("HitPlayer", _hitPlayer);
            _animator?.SetInteger("HitPlayerRandomValue", _hitPlayerValue);
            _animator?.SetBool("Greeting", _isGreeting && UnityEngine.Random.Range(0, 100) == 0);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;

            EnterPlayer();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;

            ExitPlayer();
        }
    }
}
