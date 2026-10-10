using ShoulderDelivery.Entity;
using ShoulderDerivery.Common;

namespace ShoulderDelivery.UseCase
{
    public readonly struct ThrowCardboardOutput
    {
        public readonly CardboardId CardboardId;
        public readonly CardboardWeight CardboardWeight;
        public readonly ThrowContext Context;
        public readonly CardboardLaunch Launch;

        public ThrowCardboardOutput(CardboardId cardboardId
            , CardboardWeight cardboardWeight
            , ThrowContext context
            , CardboardLaunch launch)
        {
            CardboardId = cardboardId;
            CardboardWeight = cardboardWeight;
            Context = context;
            Launch = launch;
        }
    }
}
