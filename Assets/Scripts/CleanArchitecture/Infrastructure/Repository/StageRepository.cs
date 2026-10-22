using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using System;
using System.Linq;
using UnityEditor.SceneManagement;

namespace ShoulderDelivery.Infrastructure
{
    public class StageRepository : IStageRepository
    {
        readonly StageDefinitionAsset[] _assets;

        public StageRepository(StageDefinitionAsset[] assets)
        {
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
        }

        public StageDefinition Get(StageId key)
        {
            var asset = _assets.FirstOrDefault(x => x != null && x.StageId == key.Id);

            if (asset == null)
                throw new InvalidOperationException(nameof(asset));

            return asset.ToEntity();
        }
    }
}
