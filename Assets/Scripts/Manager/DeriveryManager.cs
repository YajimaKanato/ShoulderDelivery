using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeriveryManager : ManagerBase
{
    [SerializeField] RectTransform _canvas;
    [SerializeField] TextMeshProUGUI _distanceText;
    [SerializeField] TextMeshProUGUI _deriveryCountText;
    [SerializeField] float _textSpaceX = 0.8f;
    [SerializeField] float _textSpaceY = 0.8f;
    [SerializeField] Image _targetPinImage;
    [SerializeField] float _imageSpaceX = 0.9f;
    [SerializeField] float _imageSpaceY = 0.9f;
    [SerializeField] int _deriveryCount = 5;
    Target[] _targets;
    Camera _camera;
    int _currentDeriveryIndex = -1;
    int _currentDeriveryCount = -1;

    private void Update()
    {
        if (_gameEnd) return;
        UpdateTargetUI();
    }

    public override void Init(IngameManager ingameManager)
    {
        base.Init(ingameManager);
        _targets = FindObjectsByType<Target>(FindObjectsSortMode.None);
        _camera = Camera.main;
        Shuffle();
        NextDerivery();
    }

    void Shuffle()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            var rand = Random.Range(0, _targets.Length - 1);
            (_targets[i], _targets[rand]) = (_targets[rand], _targets[i]);
        }

        foreach (var target in _targets)
        {
            target.Init(this);
        }
    }


    public void NextDerivery()
    {
        _currentDeriveryCount++;
        _deriveryCountText.text = $"{_currentDeriveryCount} / {_deriveryCount}";
        if (_currentDeriveryCount >= _deriveryCount)
        {
            _ingameManager.CompleteDerivery();
            return;
        }

        _currentDeriveryIndex++;
        _currentDeriveryIndex %= _deriveryCount;
        _targets[_currentDeriveryIndex].RequestDerivery();
    }

    void UpdateTargetUI()
    {
        var target = _targets[_currentDeriveryIndex].transform.position;
        var viewpoint = _camera.WorldToViewportPoint(target);
        var canvasX = 0f;
        var canvasY = 1f;
        if (0 <= viewpoint.x && viewpoint.x <= 1 && viewpoint.z > 0)
        {
            // 画面内
            canvasX = Mathf.Clamp01(viewpoint.x);
        }
        else
        {
            // 画面外
            var dir = target - _camera.transform.position;
            dir.Normalize();
            canvasX = Vector3.Dot(dir, _camera.transform.right) < 0 ? 0 : 1;
            canvasY = Vector3.Dot(_camera.transform.forward, dir) * 0.5f + 0.5f;
        }
        _distanceText.transform.localPosition = new Vector3((canvasX - 0.5f) * _canvas.rect.width * _textSpaceX, (canvasY - 0.5f) * _canvas.rect.height * _textSpaceY, 0);
        _distanceText.text = Vector3.Distance(_camera.transform.position, target).ToString("0000") + " m";

        _targetPinImage.transform.localPosition = new Vector3((canvasX - 0.5f) * _canvas.rect.width * _imageSpaceX, (canvasY - 0.5f) * _canvas.rect.height * _imageSpaceY, 0);
        var x = canvasX * 2 - 1;
        var sign = Mathf.Sign(x);
        _targetPinImage.transform.localEulerAngles = new Vector3(0, 0, (sign * (180 - (1 - canvasY) * 135)) - (45 * x));
    }

    public override void EndGame()
    {
        _gameEnd = true;
        _distanceText.text = "";
        _targets[_currentDeriveryIndex].CompleteDerivery();
    }
}
