using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>ステージ上のゲームの進行状況を持つクラス</summary>
    public sealed class StageState
    {
        StagePhase _currentPhase;
        float _remainingCountDownSeconds;
        float _remainingTime;

        /// <summary>カウントダウンの残り時間</summary>
        public float RemainingCountDownSeconds => _remainingCountDownSeconds;
        /// <summary>ゲームの残り時間</summary>
        public float RemainingTime => _remainingTime;
        /// <summary>プレイ中フラグ</summary>
        public bool IsPlaying => _currentPhase == StagePhase.IsPlaying;
        /// <summary>ゲーム終了フラグ</summary>
        public bool IsFinished => _currentPhase == StagePhase.Finished;

        public StageState(StageDefinition definition)
        {
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));

            _currentPhase = definition.CountDownSeconds > 0
                ? StagePhase.CountDown
                : StagePhase.IsPlaying;

            _remainingCountDownSeconds = definition.CountDownSeconds;
            _remainingTime = definition.TimeLimitSeconds;
        }

        /// <summary>
        /// カウントダウンを行うメソッド
        /// </summary>
        /// <param name="delta">フレーム時間</param>
        /// <returns>時間についての状態</returns>
        /// <exception cref="ArgumentOutOfRangeException">フレーム時間が不正である</exception>
        public StageTickResult CountDown(float delta = 0)
        {
            if (delta <= 0)
                throw new ArgumentOutOfRangeException();

            if (_currentPhase == StagePhase.CountDown)
            {
                // カウントダウンフェーズの場合はカウントダウン
                _remainingCountDownSeconds = Math.Max(0, _remainingCountDownSeconds - delta);
                if (RemainingCountDownSeconds <= 0)
                {
                    _currentPhase = StagePhase.IsPlaying;
                    return StageTickResult.CountDownFinished;
                }
            }

            return StageTickResult.None;
        }

        /// <summary>
        /// 制限時間を減らすメソッド
        /// </summary>
        /// <param name="delta">フレーム時間</param>
        /// <returns>時間についての状態</returns>
        /// <exception cref="ArgumentOutOfRangeException">フレーム時間が不正である</exception>
        public StageTickResult Tick(float delta = 0)
        {
            if (delta <= 0)
                throw new ArgumentOutOfRangeException();

            if (_currentPhase == StagePhase.IsPlaying)
            {
                // ゲームが進行中の場合は制限時間を減らす
                _remainingTime = Math.Max(0, _remainingTime - delta);
                if (RemainingTime <= 0)
                {
                    _currentPhase = StagePhase.Finished;
                    return StageTickResult.TimeUp;
                }
            }

            return StageTickResult.None;
        }

        /// <summary>
        /// ゲームを終了するメソッド
        /// </summary>
        public void Finish()
        {
            _currentPhase = StagePhase.Finished;
        }
    }
}
