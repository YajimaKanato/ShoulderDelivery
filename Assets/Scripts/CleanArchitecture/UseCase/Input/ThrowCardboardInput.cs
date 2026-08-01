using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲時の情報を持つDTO</summary>
    public readonly struct ThrowCardboardInput
    {
        public readonly ThrowContext Context;

        public ThrowCardboardInput(ThrowContext context)
        {
            Context = context;
        }
    }
}
