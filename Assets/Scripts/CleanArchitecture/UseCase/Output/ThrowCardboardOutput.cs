using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    public readonly struct ThrowCardboardOutput
    {
        public readonly ThrowContext Context;
        public readonly CardboardLaunch Launch;

        public ThrowCardboardOutput(ThrowContext context, CardboardLaunch launch)
        {
            Context = context;
            Launch = launch;
        }
    }
}
