using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class ComboUI : MonoBehaviour
    {
        [SerializeField] Image _preImage;
        [SerializeField] Image _image;
        [SerializeField] float _duration = 0.3f;
        [SerializeField] Vector3 _entryPosition;
        [SerializeField] Vector3 _exitPosition;
        Vector3 _defaultPosition;

        private void Awake()
        {
            _defaultPosition = _image.rectTransform.localPosition;
        }

        public void Entry()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_image.rectTransform.DOAnchorPos(_entryPosition, _duration));
            sequence.Join(_image.DOFade(1, _duration));
            sequence.OnComplete(() =>
            {
                _preImage.rectTransform.localPosition = _entryPosition;
                _preImage.DOFade(1, 0);
            });
        }

        public void Exit()
        {
            _image.rectTransform.localPosition = _defaultPosition;
            _image.DOFade(0, 0);
            var sequence = DOTween.Sequence();
            sequence.Append(_preImage.rectTransform.DOAnchorPos(_exitPosition, _duration));
            sequence.Join(_preImage.DOFade(0, _duration));
        }
    }
}
