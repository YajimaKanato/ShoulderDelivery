using Cysharp.Threading.Tasks;
using ShoulderDelivery.Adapter;
using ShoulderDelivery.Entity;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class StageSelectView : MonoBehaviour, IStageSelectView
    {
        [SerializeField] StageButton _buttonPrefab;

        [SerializeField] ScrollRect _scrollView;
        [SerializeField] RectTransform _selectPanel;
        [SerializeField] TextMeshProUGUI _stageName;
        [SerializeField] TextMeshProUGUI _stageInfo;
        [SerializeField] TextMeshProUGUI _stageCount;
        [SerializeField] Button _startButton;
        [SerializeField] float _waitTime = 0.1f;
        [SerializeField] FadeDirector _director;

        Button[] _rightButtons;
        Button _firstButton;
        Button _selectedButton;
        GameObject _focusedButton;
        bool _loading;

        public event Action<StageId> OnStageSelected;
        public event Action OnStart;
        public event Action OnOpenStageSelect;

        /// <summary>タイトル画面とステージ選択画面を準備するメソッド</summary>
        public void Initialize()
        {
            _startButton.interactable = false;
            _startButton.onClick.AddListener(PressStartButton);
            _selectPanel.gameObject.SetActive(false);
        }

        /// <summary>ステージ選択ボタンを作成するメソッド</summary>
        public async void CreateStageSelectButton(CreateStageSelectViewModel viewModel)
        {
            // ステージ数を表示
            _stageCount.text = viewModel.StageCount + " STAGES";

            // ボタンを生成
            var buttons = new Button[viewModel.StageCount];
            for (int i = 0; i < buttons.Length; i++)
            {
                var stageId = viewModel.StageIds[i];
                var stageButton = CreateButton(transform, stageId.Id, Color.white);
                var button = stageButton.GetComponent<Button>();
                buttons[i] = button;

                // 初めのボタンを設定
                if (_firstButton == null)
                {
                    _firstButton = button;
                    if (_firstButton) _firstButton.Select();
                }

                // イベントを登録
                button.onClick.AddListener(() =>
                {
                    if (_loading) return;
                    if (_selectedButton) _selectedButton.image.color = Color.white;
                    _selectedButton = button;
                    button.image.color = Color.white;
                    OnStageSelected?.Invoke(stageId);
                });

                // 任意の秒数待ち演出をずらす
                stageButton.OpenStageSelect();
                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));
            }

            SetStageNavigation(buttons);
        }

        /// <summary>ステージ一覧と開始ボタンの間の移動を設定するメソッド</summary>
        /// <param name="buttons">生成したステージ選択ボタン</param>
        void SetStageNavigation(Button[] buttons)
        {
            var columns = GetComponent<GridLayoutGroup>().constraintCount;
            var rowCount = (buttons.Length + columns - 1) / columns;
            _rightButtons = new Button[rowCount];

            for (int i = 0; i < buttons.Length; i++)
            {
                var column = i % columns;
                var isRightEdge = column == columns - 1 || i == buttons.Length - 1;

                // 生成中にステージが選ばれていた場合も、STARTへの移動を設定する
                var right = isRightEdge
                    ? (_startButton.interactable ? _startButton : null)
                    : buttons[i + 1];

                buttons[i].navigation = new Navigation
                {
                    mode = Navigation.Mode.Explicit,
                    selectOnLeft = column > 0 ? buttons[i - 1] : null,
                    selectOnRight = right,
                    selectOnUp = i >= columns ? buttons[i - columns] : null,
                    selectOnDown = i + columns < buttons.Length ? buttons[i + columns] : null
                };

                if (isRightEdge)
                    _rightButtons[i / columns] = buttons[i];
            }
        }

        /// <summary>選択されたステージの詳細を表示するメソッド</summary>
        public void ShowStageInfo(ShowStageInfoViewModel viewModel)
        {
            _stageName.text = viewModel.StageName;
            _stageInfo.text = $"StageHour  {viewModel.StageHour}\n"
                + $"Time limit   {viewModel.TimeLimitSeconds / 60}:{viewModel.TimeLimitSeconds % 60:00}\n"
                + $"Deliveries   {viewModel.RequiredDeliveryCount}\n"
                + $"Addresses   {viewModel.TargetCount}";
            _startButton.interactable = true;

            // まだ生成中なら、生成完了時に右端の移動先を設定する
            if (_rightButtons == null) return;

            foreach (var button in _rightButtons)
            {
                var navigation = button.navigation;
                navigation.selectOnRight = _startButton;
                button.navigation = navigation;
            }
        }

        /// <summary>ロード中にボタンの連打を防ぐメソッド</summary>
        public void ShowLoading()
        {
            _loading = true;
            _startButton.interactable = false;
        }

        /// <summary>ステージ選択画面に切り替えるメソッド</summary>
        public void OpenStageSelect()
        {
            _selectPanel.gameObject.SetActive(true);
            Canvas.ForceUpdateCanvases();
            _scrollView.verticalNormalizedPosition = 1f;
            OnOpenStageSelect?.Invoke();
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

        StageButton CreateButton(Transform parent, string label, Color color)
        {
            var stageButton = Instantiate(_buttonPrefab, parent);
            var button = stageButton.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.image.color = color;
            var text = button.GetComponentInChildren<TextMeshProUGUI>(true);
            text.text = label;
            text.enableAutoSizing = true;
            text.fontSizeMin = 14f;
            text.fontSizeMax = 25f;
            return stageButton;
        }

        async void PressStartButton()
        {
            if (_director != null) await _director.FadeOut();
            OnStart?.Invoke();
        }
    }
}
