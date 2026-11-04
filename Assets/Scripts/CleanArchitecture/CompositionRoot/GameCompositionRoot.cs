using ShoulderDelivery.Infrastructure;
using UnityEngine;

namespace ShoulderDelivery.CompositionRoot
{
    /// <summary>ゲーム全般で使うクラスを持つクラス</summary>
    public class GameCompositionRoot : MonoBehaviour
    {
        [Header("Master")]
        [SerializeField] StageDefinitionAsset[] _stages;
        StageRepository _stageRepository;

        public StageRepository StageRepository => _stageRepository;

        static GameCompositionRoot _instance;
        public static GameCompositionRoot Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                _stageRepository = new(_stages);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
