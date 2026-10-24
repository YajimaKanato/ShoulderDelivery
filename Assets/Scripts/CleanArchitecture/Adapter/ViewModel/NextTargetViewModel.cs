using ShoulderDelivery.Entity;

namespace ShoulderDelivery.Adapter
{
    public readonly struct NextTargetViewModel
    {
        public readonly TargetId TargetId;

        public NextTargetViewModel(TargetId targetId)
        {
            TargetId = targetId;
        }
    }
}
