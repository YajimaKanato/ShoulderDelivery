using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>ゲームに必要な参照を持つクラス</summary>
    public sealed class GameSession
    {
        readonly StageDefinition _stageDefinition;
        readonly StageState _stageState;
        readonly DeliveryState _deliveryState;
        readonly Score _score;
        readonly InFlightCardboardState _inFlightCardboardState;

        /// <summary>ステージ定義</summary>
        public StageDefinition StageDefinition => _stageDefinition;
        /// <summary>現在のステージの状態</summary>
        public StageState StageState => _stageState;
        /// <summary>現在の配達状況</summary>
        public DeliveryState DeliveryState => _deliveryState;
        /// <summary>スコア</summary>
        public Score Score => _score;
        /// <summary>投げた段ボールの状況</summary>
        public InFlightCardboardState InFlightCardboardState => _inFlightCardboardState;

        public GameSession(StageDefinition stageDefinition)
        {
            if (StageDefinition == null)
                throw new ArgumentNullException(nameof(StageDefinition));

            _stageDefinition = stageDefinition;
            _stageState = new StageState(stageDefinition);
            _deliveryState = new DeliveryState(stageDefinition);
            _score = new Score();
            _inFlightCardboardState = new InFlightCardboardState();
        }
    }
}
