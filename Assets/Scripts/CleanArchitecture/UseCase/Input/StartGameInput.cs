using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームを開始するときに必要な情報を持つDTO</summary>
    public readonly struct StartGameInput
    {
        public readonly StageId StageId;

        public StartGameInput(StageId stageId)
        {
            StageId = stageId;
        }
    }
}
