using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace ShoulderDelivery.Infrastructure
{
    public class WalkCitizen : Citizen
    {
        [SerializeField] SplineContainer _spline;
        [SerializeField] int _splineIndex;
        [SerializeField] float _walkPerSeconds = 2f;
        CancellationTokenSource _cts;
        UniTask _walkUniTask;
        bool _walking;
        byte _startRate = 0;
        float _walkInterval = 1.0f;
        float _elapsed = 0;

        protected override void Awake()
        {
            base.Awake();

            _elapsed = _walkInterval;
            if (_spline.Splines.Count <= _splineIndex)
            {
                Debug.LogWarning($"Index{_splineIndex}は指定できません");
                _spline = null;
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (_spline != null)
            {
                _elapsed = Mathf.Clamp(_elapsed + Time.deltaTime, 0, _walkInterval);
                // 歩きモーションが終了していたら一定確率で再度歩き出す
                if (_walkUniTask.Status.IsCompleted()
                    && _elapsed >= _walkInterval)
                {
                    _cts = new CancellationTokenSource();
                    _walkUniTask = MoveAsync(_startRate, 1 - _startRate, _cts.Token);
                    _startRate = (byte)(1 - _startRate);
                }
            }
        }

        /// <summary>
        /// 非同期で移動するメソッド
        /// </summary>
        /// <param name="start">開始割合</param>
        /// <param name="end">終了割合</param>
        /// <param name="ct">キャンセルトークン</param>
        /// <returns></returns>
        async UniTask MoveAsync(float start, float end, CancellationToken ct)
        {
            float elapsed = 0f;
            float3 position;
            _walking = true;
            _forward = _spline.EvaluatePosition(_splineIndex, end).TransportF3ToV3() - _spline.EvaluatePosition(_splineIndex, start).TransportF3ToV3();
            transform.forward = _forward;
            var duration = _forward.magnitude / _walkPerSeconds;

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

                position = _spline.EvaluatePosition(_splineIndex, t);
                transform.position = new Vector3(position.x, position.y, position.z);

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            position = _spline.EvaluatePosition(_splineIndex, end);
            transform.position = new Vector3(position.x, position.y, position.z);
            _walking = false;
            _elapsed = 0;
            _walkInterval = UnityEngine.Random.Range(0, 4);
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            _animator?.SetBool("Walking", _walking);
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
