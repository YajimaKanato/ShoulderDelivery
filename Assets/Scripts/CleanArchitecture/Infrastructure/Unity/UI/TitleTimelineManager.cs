using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Playables;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>タイトル演出とステージ選択への切り替えを管理するクラス</summary>
    [RequireComponent(typeof(PlayableDirector))]
    public class TitleTimelineManager : MonoBehaviour
    {
        [SerializeField] StageSelectView _stageSelectView;
        [SerializeField] GameObject[] _titleObjects;
        [SerializeField] PlayableDirector _director;
        [SerializeField] int _skipFrame = 220;
        [SerializeField] int _enableHasInputFrame = 60;
        const double TIMELINEFPS = 60;

        private void LateUpdate()
        {
            if (!HasTitleInput()) return;

            // 演出中の入力はスキップに使い、次の入力でステージ選択に進む
            if (_director.time < _skipFrame / TIMELINEFPS)
            {
                _director.time = _skipFrame / TIMELINEFPS;
                return;
            }

            OpenStageSelect();
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
            foreach (var titleObject in _titleObjects)
                titleObject.SetActive(false);

            _stageSelectView.OpenStageSelect();
            enabled = false;
        }
    }
}
