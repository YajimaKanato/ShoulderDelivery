using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>ステージ管理をするクラス</summary>
    public sealed class StageManager : MonoBehaviour
    {
        /// <summary>現在選択中のステージID</summary>
        static string _stageId;

        /// <summary>
        /// ゲームをスタートするメソッド
        /// </summary>
        /// <param name="asset">ステージID</param>
        public void StartGame(StageDefinitionAsset asset)
        {
            LoadStage(asset.StageId);
        }

        /// <summary>選択したステージIDを引き継いでシーンをロードするメソッド</summary>
        /// <param name="stageId">選択したステージID</param>
        public static void LoadStage(string stageId)
        {
            _stageId = stageId;
            SceneManager.LoadSceneAsync("Ingame");
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
