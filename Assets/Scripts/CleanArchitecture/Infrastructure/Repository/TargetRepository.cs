using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using System;
using System.Linq;

namespace ShoulderDelivery.Infrastructure
{
    public class TargetRepository : ITargetRepository
    {
        readonly TargetDefinitionAsset[] _assets;

        public TargetRepository(TargetDefinitionAsset[] assets)
        {
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
        }

        public TargetDefinition Get(TargetId key)
        {
            var asset = _assets.FirstOrDefault(x => x.TargetId == key.Id);

            return asset.ToEntity();
        }
    }
}
