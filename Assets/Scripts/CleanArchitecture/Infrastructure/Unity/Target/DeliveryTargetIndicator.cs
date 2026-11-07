using System.Linq;
using ShoulderDelivery.Adapter;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public sealed class DeliveryTargetIndicator : MonoBehaviour, ITargetView
    {
        [SerializeField] GameObject _pinPrefab;
        [SerializeField] RectTransform _arrow;
        [SerializeField] TextMeshProUGUI _distance;
        [SerializeField] float _textSpaceX = 0.8f;
        [SerializeField] float _textSpaceY = 0.8f;
        [SerializeField] float _imageSpaceX = 0.9f;
        [SerializeField] float _imageSpaceY = 0.9f;

        TargetView[] _targets;
        TargetView _currentTarget;
        GameObject _pin;
        Camera _camera;

        public void Initialize(TargetView[] targets)
        {
            _targets = targets;
            _pin = Instantiate(_pinPrefab, transform);
            _camera = Camera.main;
            ClearTarget();
        }

        public void UpdateTarget(NextTargetViewModel viewModel)
        {
            var target = _targets.FirstOrDefault(view => view.TargetId == viewModel.TargetId);
            if (!target)
            {
                ClearTarget();
                Debug.LogError($"配達先 {viewModel.TargetId} がシーンにありません。");
                return;
            }

            _currentTarget = target;
            _pin.transform.position = target.transform.position + Vector3.up * 7f;
            _pin.SetActive(true);
        }

        public void ClearTarget()
        {
            _currentTarget = null;
            _pin.SetActive(false);
            _arrow.gameObject.SetActive(false);
            _distance.gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if (!_currentTarget || !_camera) return;

            var targetPosition = _currentTarget.transform.position;
            var viewport = _camera.WorldToViewportPoint(targetPosition);
            var toTarget = targetPosition - _camera.transform.position;
            float canvasX;
            float canvasY;

            // 画面内に収まっているかどうか
            // y座標の画面のおさまりは無視する
            if (viewport.z > 0 && viewport.x >= 0f && viewport.x <= 1f)
            {
                canvasX = Mathf.Clamp01(viewport.x);
                canvasY = 1f;
            }
            else
            {
                var direction = toTarget.normalized;
                canvasX = Vector3.Dot(direction, _camera.transform.right) < 0f ? 0f : 1f;
                canvasY = Vector3.Dot(_camera.transform.forward, direction) * 0.5f + 0.5f;
            }

            // UIピンの座標計算
            var canvas = (RectTransform)_arrow.parent;
            var x = (canvasX - 0.5f) * canvas.rect.width;
            var y = (canvasY - 0.5f) * canvas.rect.height;
            _distance.rectTransform.localPosition = new Vector3(x * _textSpaceX, y * _textSpaceY, 0f);
            _distance.text = $"{toTarget.magnitude:0000} m";
            _arrow.localPosition = new Vector3(x * _imageSpaceX, y * _imageSpaceY, 0f);

            // UIピンの回転計算
            var horizontal = canvasX * 2f - 1f;
            var angle = Mathf.Sign(horizontal) * (180f - (1f - canvasY) * 135f) - 45f * horizontal;
            _arrow.localEulerAngles = new Vector3(0f, 0f, angle);
            _arrow.gameObject.SetActive(true);
            _distance.gameObject.SetActive(true);
        }
    }
}
