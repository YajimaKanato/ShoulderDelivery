using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームを開始するときに必要な情報を持つDTO</summary>
    public readonly struct GameStartInput
    {
        public readonly StageId StageId;

        public GameStartInput(StageId stageId)
        {
            StageId = stageId;
        }
    }
}
