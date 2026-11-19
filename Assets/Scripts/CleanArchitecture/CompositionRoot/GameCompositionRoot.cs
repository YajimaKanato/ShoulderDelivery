using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShoulderDelivery.CompositionRoot
{
    /// <summary>ゲーム全般で使うクラスを持つクラス</summary>
    public class GameCompositionRoot : MonoBehaviour
    {
        [Header("Master")]
        [SerializeField] StageDefinitionAsset[] _stages;
        static StageRepository _stageRepository;

        public static StageRepository StageRepository => _stageRepository;
        public static IStageClearStore StageClearStore { get; private set; }

        private void Awake()
        {
            _stageRepository = new(_stages);
            StageClearStore = new JsonStageClearStore(Application.persistentDataPath);
        }
    }
}
