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

            var distance = GetWallDistance(_player.transform.position);

            // 距離に対しての透過割合
            var result = 1 - Mathf.InverseLerp(_visibleMinDistance, _visibleMaxDistance, distance);
            _guardPropertyBlock.SetFloat(_propertyName, result);

            _renderer.SetPropertyBlock(_guardPropertyBlock);
        }

        /// <summary>XZ平面上でプレイヤーから壁までの最短距離を取得するメソッド</summary>
        /// <param name="playerPosition">プレイヤーのワールド座標</param>
        /// <returns>壁までの最短距離</returns>
        float GetWallDistance(Vector3 playerPosition)
        {
            // 壁のメッシュはローカルX方向に伸びているので、その両端を取得する
            var bounds = _renderer.localBounds;
            var center = bounds.center;
            var halfWidth = Vector3.right * bounds.extents.x;
            var wallStart = _renderer.transform.TransformPoint(center - halfWidth);
            var wallEnd = _renderer.transform.TransformPoint(center + halfWidth);

            playerPosition.y = 0f;
            wallStart.y = 0f;
            wallEnd.y = 0f;

            // 壁の線分上でプレイヤーに最も近い点を求める
            var wall = wallEnd - wallStart;
            var lengthSquared = wall.sqrMagnitude;
            if (lengthSquared < 0.000001f)
                return Vector3.Distance(playerPosition, wallStart);

            var t = Mathf.Clamp01(Vector3.Dot(playerPosition - wallStart, wall) / lengthSquared);
            return Vector3.Distance(playerPosition, wallStart + wall * t);
        }

        public void SetColor(Color color)
        {
            _renderer.GetPropertyBlock(_guardPropertyBlock);
            _guardPropertyBlock.SetColor(_colorPropertyName, color);
            _renderer.SetPropertyBlock(_guardPropertyBlock);
        }
    }
}
