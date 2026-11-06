using ShoulderDelivery.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShoulderDelivery.CompositionRoot
{
    /// <summary>ゲーム全般で使うクラスを持つクラス</summary>
    public class GameCompositionRoot : MonoBehaviour
    {
        [Header("Master")]
        [SerializeField] StageDefinitionAsset[] _stages;
        static string _stageId;
        static StageRepository _stageRepository;

        public static StageRepository StageRepository => _stageRepository;

        private void Awake()
        {
            _stageRepository ??= new(_stages);
        }

        /// <summary>
        /// ゲームをスタートするメソッド
        /// </summary>
        /// <param name="asset">ステージID</param>
        public void StartGame(StageDefinitionAsset asset)
        {
            _stageId = asset.StageId;
            LoadScene();
        }

        /// <summary>
        /// シーンをロードするメソッド
        /// </summary>
        public void LoadScene()
        {
            SceneManager.LoadSceneAsync("Ingame");
        }

        /// <summary>
        /// 現在選択しているステージIDを取得するメソッド
        /// </summary>
        /// <returns>現在選択しているステージID</returns>
        public static string GetStageId()
        {
            var stageId = _stageId;
            _stageId = null;
            return stageId;
        }
    }
}
