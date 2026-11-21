using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    public interface IStageClearStore
    {
        bool IsCleared(StageId stageId);
        int? GetBestScore(StageId stageId);
        void RecordResult(StageId stageId, int score, bool cleared);
        bool TryReset();
    }
}
