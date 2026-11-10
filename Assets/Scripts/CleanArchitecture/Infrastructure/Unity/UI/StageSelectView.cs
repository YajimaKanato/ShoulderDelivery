using ShoulderDelivery.Adapter;
using ShoulderDelivery.Entity;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class StageSelectView : MonoBehaviour, IStageSelectView
    {
        [SerializeField] Button _buttonPrefab;
        [SerializeField] Color AccentColor = new(0.28f, 0.85f, 0.78f);

        [SerializeField] ScrollRect _scrollView;
        [SerializeField] RectTransform _titlePanel;
        [SerializeField] RectTransform _selectPanel;
        [SerializeField] TextMeshProUGUI _stageName;
        [SerializeField] TextMeshProUGUI _stageInfo;
        [SerializeField] TextMeshProUGUI _stageCount;
        [SerializeField] Button _startButton;
        [SerializeField] TextMeshProUGUI _startLabel;

        Button _firstButton;
        Button _selectedButton;
        GameObject _focusedButton;
        bool _loading;

        public event Action<StageId> OnStageSelected;
        public event Action OnStart;

        /// <summary>タイトル画面とステージ選択画面を準備するメソッド</summary>
        public void Initialize()
        {
            _startButton.interactable = false;
            _startButton.onClick.AddListener(() => OnStart?.Invoke());
            _selectPanel.gameObject.SetActive(false);
        }

        /// <summary>ステージ選択ボタンを作成するメソッド</summary>
        public void CreateStageSelectButton(CreateStageSelectViewModel viewModel)
        {
            _stageCount.text = viewModel.StageCount + " STAGES";
            var buttons = new Button[viewModel.StageCount];
            for (int i = 0; i < buttons.Length; i++)
            {
                var stageId = viewModel.StageIds[i];
                var button = CreateButton(transform, stageId.Id, Color.white);
                buttons[i] = button;
                _firstButton ??= button;
                button.onClick.AddListener(() =>
                {
                    if (_loading) return;
                    if (_selectedButton) _selectedButton.image.color = Color.white;
                    _selectedButton = button;
                    button.image.color = AccentColor;
                    OnStageSelected?.Invoke(stageId);
                });
            }

            SetStageNavigation(buttons);
        }

        /// <summary>ステージ一覧と開始ボタンの間の移動を設定するメソッド</summary>
        /// <param name="buttons">生成したステージ選択ボタン</param>
        void SetStageNavigation(Button[] buttons)
        {
            var columns = GetComponent<GridLayoutGroup>().constraintCount;
            for (int i = 0; i < buttons.Length; i++)
            {
                var column = i % columns;
                buttons[i].navigation = new Navigation
                {
                    mode = Navigation.Mode.Explicit,
                    selectOnLeft = column > 0 ? buttons[i - 1] : null,
                    selectOnRight = column + 1 < columns && i + 1 < buttons.Length ? buttons[i + 1] : _startButton,
                    selectOnUp = i >= columns ? buttons[i - columns] : null,
                    selectOnDown = i + columns < buttons.Length ? buttons[i + columns] : null
                };
            }
        }

        /// <summary>選択されたステージの詳細を表示するメソッド</summary>
        public void ShowStageInfo(ShowStageInfoViewModel viewModel)
        {
            _stageName.text = viewModel.StageName;
            _stageInfo.text = $"Time limit   {viewModel.TimeLimitSeconds / 60}:{viewModel.TimeLimitSeconds % 60:00}\n"
                + $"Deliveries   {viewModel.RequiredDeliveryCount}\n"
                + $"Addresses   {viewModel.TargetCount}";
            _startButton.interactable = true;
        }

        /// <summary>ロード中にボタンの連打を防ぐメソッド</summary>
        public void ShowLoading()
        {
            _loading = true;
            _startButton.interactable = false;
            _startLabel.text = "LOADING...";
        }

        /// <summary>タイトル画面での入力を受け付けるメソッド</summary>
        public bool HasTitleInput()
        {
            return _titlePanel.gameObject.activeSelf
                && (Keyboard.current?.anyKey.wasPressedThisFrame == true
                    || Mouse.current?.leftButton.wasPressedThisFrame == true
                    || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true
                    || Gamepad.current?.allControls.OfType<ButtonControl>().Any(button => button.wasPressedThisFrame) == true);
        }

        /// <summary>ステージ選択画面に切り替えるメソッド</summary>
        public void OpenStageSelect()
        {
            _titlePanel.gameObject.SetActive(false);
            _selectPanel.gameObject.SetActive(true);
            Canvas.ForceUpdateCanvases();
            _scrollView.verticalNormalizedPosition = 1f;
            if (_firstButton) _firstButton.Select();
        }

        private void LateUpdate()
        {
            // キーやパッドでスクロール範囲外のボタンを選んだ場合は表示範囲に収める
            var selected = EventSystem.current?.currentSelectedGameObject;
            if (!selected || selected == _focusedButton || !selected.transform.IsChildOf(transform)) return;

            _focusedButton = selected;
            // 開始ボタンから左に戻ったときは直前のステージボタンに戻す
            _startButton.navigation = new Navigation
            {
                mode = Navigation.Mode.Explicit,
                selectOnLeft = selected.GetComponent<Button>()
            };

            var viewport = _scrollView.viewport;
            var bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, selected.transform);
            var offset = Mathf.Max(0f, viewport.rect.yMin - bounds.min.y)
                - Mathf.Max(0f, bounds.max.y - viewport.rect.yMax);
            if (Mathf.Approximately(offset, 0f)) return;

            _scrollView.StopMovement();
            _scrollView.content.anchoredPosition += Vector2.up * offset;
        }

        Button CreateButton(Transform parent, string label, Color color)
        {
            var button = Instantiate(_buttonPrefab, parent);
            button.onClick = new Button.ButtonClickedEvent();
            button.image.color = color;
            var text = button.GetComponentInChildren<TextMeshProUGUI>(true);
            text.text = label;
            text.enableAutoSizing = true;
            text.fontSizeMin = 14f;
            text.fontSizeMax = 32f;
            return button;
        }
    }
}
