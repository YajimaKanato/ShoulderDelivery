using System.Linq;
using ShoulderDelivery.Adapter;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public sealed class DeliveryTargetIndicator : MonoBehaviour, ITargetView
    {
        [SerializeField] GameObject _pinPrefab;
        [SerializeField] Transform _distancePivot;
        [SerializeField] TextMeshPro _distanceText;

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
            if (_distancePivot != null) _distancePivot.gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if (!_currentTarget || !_camera) return;

            if (_distancePivot == null || _distanceText == null) return;

            var targetPosition = _currentTarget.transform.position;
            var direction = targetPosition - _distancePivot.position;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.0001f)
                _distancePivot.rotation = Quaternion.LookRotation(direction, Vector3.up);

            _distanceText.transform.rotation = _camera.transform.rotation;
            var displayDistance = Vector3.Distance(targetPosition, _camera.transform.position) * 0.1f;
            _distanceText.text = $"{displayDistance:0000} m";
            _distancePivot.gameObject.SetActive(true);
        }
    }
}
