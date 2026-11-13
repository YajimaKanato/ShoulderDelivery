using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _loadingText;
        [SerializeField] Animator _loadingAnimator;
        [SerializeField] string _loadingAnimName;
        [SerializeField] CanvasGroup _loadingCanvasGroup;

        private void Awake()
        {
            _loadingText.text = string.Empty;
            _loadingCanvasGroup.alpha = 0;
        }

        public void PlayLoading()
        {
            _loadingText.text = "Loading";
            _loadingCanvasGroup.alpha = 1;
            _loadingAnimator.Play(_loadingAnimName);
        }
    }
}
