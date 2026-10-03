using ShoulderDerivery.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class LogManager : MonoBehaviour
    {
        [SerializeField] ContextText[] _cardboardContextTexts;
        [SerializeField] float[] _showPosY;
        [SerializeField] float _entryPosX;
        [SerializeField] float _exitPosX;
        [SerializeField] int _viewCount = 5;
        List<ContextText> _showedTexts = new();
        ActionDisposer _disposer = new();

        private void OnEnable()
        {
            foreach (var context in _cardboardContextTexts)
            {
                if (context != null)
                    _disposer.AddActionDisposing(context.OnExit(ExitEvent));
            }
        }

        private void OnDisable()
        {
            _disposer.Dispose();
        }

        /// <summary>
        /// ログを出すメソッド
        /// </summary>
        /// <param name="context">ログの内容</param>
        /// <param name="color">ログの色</param>
        public void ShowContext(string context, Color color)
        {
            // ログとして使われていないオブジェクトを取得
            var text = _cardboardContextTexts.Where(t => !t.IsShowing).First();
            // 使用中リストに追加
            _showedTexts.Insert(0, text);

            for (int i = _showedTexts.Count - 1; 0 <= i; i--)
            {
                var moveText = _showedTexts[i];
                if (moveText == null) continue;
                if (i > _viewCount) continue;   // 退場中

                if (i == _viewCount)
                {
                    // 表示限界を超えているものは消す
                    moveText.ExitTween(_exitPosX);
                }
                else if (i == 0)
                {
                    // 新しく表示する
                    moveText.EntryTween(_entryPosX, _exitPosX, _showPosY[i], color, context);
                }
                else
                {
                    // ずらす
                    moveText.MoveTween(_showPosY[i]);
                }
            }
        }

        void ExitEvent(ContextText context)
        {
            // 退場したものをリストから削除
            _showedTexts.Remove(context);
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
