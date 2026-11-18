using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>ステージ管理をするクラス</summary>
    public sealed class StageManager : MonoBehaviour
    {
        /// <summary>現在選択中のステージID</summary>
        static string _stageId;
        static bool _openStageSelect;

        /// <summary>選択したステージIDを引き継いでシーンをロードするメソッド</summary>
        /// <param name="stageId">選択したステージID</param>
        public static async UniTask LoadStage(string stageId)
        {
            _stageId = stageId;
            await SceneManager.LoadSceneAsync("Ingame");
        }

        /// <summary>タイトルシーンをロードするメソッド</summary>
        public static AsyncOperation LoadTitle(bool openStageSelect = false)
        {
            _openStageSelect = openStageSelect;
            return SceneManager.LoadSceneAsync("Title");
        }

        /// <summary>タイトルでステージ選択から開始する指定を取得するメソッド</summary>
        public static bool ConsumeOpenStageSelect()
        {
            var openStageSelect = _openStageSelect;
            _openStageSelect = false;
            return openStageSelect;
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
