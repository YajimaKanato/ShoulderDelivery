using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>ステージの定義を持つクラス</summary>
    public class StageDefinition
    {
        StageState _currentState;
        int _requiredDeliveryCount;
        int _deliveredCount;
        float _remainingTime;

        /// <summary>プレイ中フラグ</summary>
        public bool IsPlaying => _currentState == StageState.IsPlaying;
        /// <summary>ゲーム終了フラグ</summary>
        public bool IsFinished => _currentState == StageState.Cleared || _currentState == StageState.TimeUp;
        /// <summary>ノルマ達成フラグ</summary>
        public bool IsQuataAchieved => _deliveredCount >= _requiredDeliveryCount;

        /// <summary>
        /// カウントダウン状態に遷移するメソッド
        /// </summary>
        /// <returns>遷移できたかどうか</returns>
        public bool BeginCountDown()
        {
            // カウントダウン前のステートじゃなければ遷移できない
            if (_currentState != StageState.BeforeCountDown) return false;

            _currentState = StageState.CountDown;
            return true;
        }

        /// <summary>
        /// ゲーム中状態に遷移するメソッド
        /// </summary>
        /// <returns>遷移できたかどうか</returns>
        public bool BeginPlay()
        {
            // カウントダウン状態じゃなければ遷移できない
            if (_currentState != StageState.CountDown) return false;

            _currentState = StageState.IsPlaying;
            return true;
        }

        /// <summary>
        /// 時間を進めるメソッド
        /// </summary>
        /// <param name="delta">フレーム時間</param>
        /// <returns>時間が無くなったかどうか</returns>
        public bool AdvanceTime(float delta = 0)
        {
            // ゲームプレイ中でないかフレーム時間がない場合は無視
            if (IsPlaying || delta <= 0) return false;

            // 残り時間の数値を最小で0になるようにする
            _remainingTime = Math.Max(0, _remainingTime - delta);
            if (_remainingTime > 0) return false;

            // 制限時間がなくなった
            _currentState = StageState.TimeUp;
            return true;
        }

        /// <summary>
        /// 配達時に呼ばれるメソッド
        /// </summary>
        /// <returns>ノルマ達成したかどうか</returns>
        public bool Delivery()
        {
            // プレイ中でなければ無視
            if (IsPlaying) return false;

            // 荷物を届けたことを記録
            _deliveredCount++;
            if (!IsQuataAchieved) return false;

            // ノルマ達成でゲームクリア
            _currentState = StageState.Cleared;
            return true;
        }
    }
}
