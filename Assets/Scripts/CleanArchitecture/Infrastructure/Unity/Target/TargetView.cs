using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class TargetView : MonoBehaviour
    {
        [SerializeField] TargetDefinitionAsset _definitionAsset;

        private void OnValidate()
        {
            if (_definitionAsset != null)
                SetPosition();
        }

#if UNITY_EDITOR
        [ContextMenu("SetPosition")]
        void SetPosition()
        {
            if (_definitionAsset == null)
            {
                Debug.Log("アセットがアサインされていません");
                return;
            }

            _definitionAsset.SetPosition(transform.position);
            Debug.Log("座標を更新しました", gameObject);
        }
#endif
    }
}
