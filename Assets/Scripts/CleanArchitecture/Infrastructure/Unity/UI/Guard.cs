using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class Guard : MonoBehaviour
    {
        [SerializeField] PlayerController _player;
        [SerializeField] Renderer _renderer;
        [SerializeField] float _visibleMaxDistance = 100;
        [SerializeField] float _visibleMinDistance = 10;
        [SerializeField] string _propertyName;
        [SerializeField] string _colorPropertyName;

        MaterialPropertyBlock _guardPropertyBlock;

        private void Awake()
        {
            _guardPropertyBlock = new();
        }

        // Update is called once per frame
        void Update()
        {
            if (_player == null) return;

            _renderer.GetPropertyBlock(_guardPropertyBlock);

            // プレイヤーと壁のxz平面に射影した座標
            var playerProjectionPosition = _player.transform.position;
            playerProjectionPosition.y = 0;
            var projectionPosition = transform.position;
            projectionPosition.y = 0;

            // 距離
            var range = _visibleMaxDistance - _visibleMinDistance;
            var distance = Mathf.Clamp(Vector3.Magnitude(playerProjectionPosition - projectionPosition) - _visibleMinDistance
                , 0
                , _visibleMaxDistance);

            // 距離に対しての透過割合
            var result = 1 - distance / range;
            _guardPropertyBlock.SetFloat(_propertyName, result);

            _renderer.SetPropertyBlock(_guardPropertyBlock);
        }

        public void SetColor(Color color)
        {
            _renderer.GetPropertyBlock(_guardPropertyBlock);
            _guardPropertyBlock.SetColor(_colorPropertyName, color);
            _renderer.SetPropertyBlock(_guardPropertyBlock);
        }
    }
}
