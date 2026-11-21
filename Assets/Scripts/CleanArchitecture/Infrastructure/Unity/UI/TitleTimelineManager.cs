using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>タイトル演出とステージ選択への切り替えを管理するクラス</summary>
    [RequireComponent(typeof(PlayableDirector))]
    public class TitleTimelineManager : MonoBehaviour
    {
        [SerializeField] StageSelectView _stageSelectView;
        [SerializeField] GameObject _titleMenu;
        [SerializeField] GameObject _infoWindow;
        [SerializeField] GameObject _configWindow;
        [SerializeField] Button _stageSelectButton;
        [SerializeField] Button _infoButton;
        [SerializeField] Button _configButton;
        [SerializeField] Button _backButton;
        [SerializeField] GameObject[] _titleObjects;
        [SerializeField] PlayableDirector _director;
        [SerializeField] TitleDancingCitizen _dancingCitizen;
        [SerializeField] int _enableHasInputFrame = 60;
        const double TIMELINEFPS = 60;

        Button _returnButton;
        bool _menuReady;

        void Awake()
        {
            _stageSelectButton.onClick.AddListener(OpenStageSelect);
            _infoButton.onClick.AddListener(OpenInfo);
            _configButton.onClick.AddListener(OpenConfig);
            _backButton.onClick.AddListener(CloseWindow);
        }

        /// <summary>イントロ終了後の状態からステージ選択を開始するメソッド</summary>
        public void StartFromStageSelect()
        {
            FinishIntro();
            OpenStageSelect();
        }

        private void LateUpdate()
        {
            if (_menuReady) return;
            if (HasTitleInput() || _director.time >= _director.duration - 0.0001)
                FinishIntro();
        }

        void FinishIntro()
        {
            _director.Pause();
            _director.time = _director.duration;
            _director.Evaluate();
            // スキップでSignalを飛ばした場合も市民を出す
            _dancingCitizen?.Spawn();
            _menuReady = true;
            ShowTitle(_stageSelectButton);
        }

        /// <summary>タイトル画面でのボタン入力を取得するメソッド</summary>
        bool HasTitleInput()
        {
            return _director.time >= _enableHasInputFrame / TIMELINEFPS
                && (Keyboard.current?.anyKey.wasPressedThisFrame == true
                || Mouse.current?.leftButton.wasPressedThisFrame == true
                || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true
                || Gamepad.current?.allControls.OfType<ButtonControl>().Any(button => button.wasPressedThisFrame) == true);
        }

        void OpenStageSelect()
        {
            OpenWindow(_stageSelectButton);
            _stageSelectView.OpenStageSelect(_backButton);
        }

        void OpenInfo()
        {
            OpenWindow(_infoButton);
            _infoWindow.SetActive(true);
        }

        void OpenConfig()
        {
            OpenWindow(_configButton);
            _configWindow.SetActive(true);
        }

        void OpenWindow(Button from)
        {
            ClearSelection();
            _returnButton = from;
            _titleMenu.SetActive(false);
            foreach (var titleObject in _titleObjects)
                titleObject.SetActive(false);
            _backButton.navigation = Navigation.defaultNavigation;
            _backButton.gameObject.SetActive(true);
            _backButton.Select();
        }

        void CloseWindow()
        {
            if (!_stageSelectView.TryCloseStageSelect()) return;
            _infoWindow.SetActive(false);
            _configWindow.SetActive(false);
            ShowTitle(_returnButton);
        }

        void ShowTitle(Button focus)
        {
            _backButton.gameObject.SetActive(false);
            foreach (var titleObject in _titleObjects)
                titleObject.SetActive(true);
            _titleMenu.SetActive(true);
            focus.Select();
        }

        static void ClearSelection()
        {
            if (EventSystem.current is { } eventSystem)
                eventSystem.SetSelectedGameObject(null);
        }
    }
}
