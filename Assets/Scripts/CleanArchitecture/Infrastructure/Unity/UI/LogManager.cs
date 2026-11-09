using ShoulderDerivery.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>ログ管理クラス</summary>
    public sealed class LogManager : MonoBehaviour
    {
        [SerializeField] ShowContextBase[] _showContexts;
        [SerializeField] ContextText[] _cardboardContextTexts;
        [SerializeField] float[] _showPosY;
        [SerializeField] float _entryPosX;
        [SerializeField] float _exitPosX;
        [SerializeField] int _viewCount = 5;
        List<ContextText> _showedTexts = new();
        ActionDisposer _disposer = new();

        private void OnEnable()
        {
            _showedTexts.RemoveAll(text => text == null || !text.IsShowing);
            MoveRowsFrom(0);

            foreach (var context in _cardboardContextTexts)
            {
                if (context != null)
                    _disposer.AddActionDisposing(context.OnExit(ExitEvent));
            }

            foreach (var showContext in _showContexts)
            {
                if (showContext != null)
                    _disposer.AddActionDisposing(showContext.OnShowContext(ShowContext));
            }
        }

        private void OnDisable()
        {
            _disposer.Dispose();
            _disposer = new();
        }

        /// <summary>
        /// ログを出すメソッド
        /// </summary>
        /// <param name="context">ログの内容</param>
        /// <param name="color">ログの色</param>
        void ShowContext(string context, Color color)
        {
            if (_viewCount <= 0) return;

            // ログとして使われていないオブジェクトを取得
            var text = _cardboardContextTexts.FirstOrDefault(t => t != null && !t.IsShowing);
            if (text == null) return;

            if (_showedTexts.Count >= _viewCount)
            {
                var lastIndex = _showedTexts.Count - 1;
                var oldest = _showedTexts[lastIndex];
                _showedTexts.RemoveAt(lastIndex);
                oldest.ExitTween(_exitPosX);
            }

            _showedTexts.Insert(0, text);
            text.EntryTween(_entryPosX, _exitPosX, _showPosY[0], color, context);
            MoveRowsFrom(1);
        }

        void ExitEvent(ContextText context)
        {
            var index = _showedTexts.IndexOf(context);
            if (index < 0) return;

            _showedTexts.RemoveAt(index);
            MoveRowsFrom(index);
        }

        void MoveRowsFrom(int index)
        {
            for (int i = index; i < _showedTexts.Count; i++)
                _showedTexts[i].MoveTween(_showPosY[i]);
        }

        private void OnValidate()
        {
            var count = _viewCount * 2 + 1;
            if (_cardboardContextTexts == null || _cardboardContextTexts.Length != count)
                _cardboardContextTexts = new ContextText[count];

            if (_showPosY == null || _showPosY.Length != count)
                _showPosY = new float[count];
        }
    }
}
