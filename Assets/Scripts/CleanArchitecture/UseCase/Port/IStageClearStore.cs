using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    public interface IStageClearStore
    {
        bool IsCleared(StageId stageId);
        void MarkCleared(StageId stageId);
    }
}
