using System;
using System.Collections.Generic;
using System.IO;
using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public sealed class JsonStageClearStore : IStageClearStore
    {
        [Serializable]
        sealed class SaveData
        {
            public List<string> clearedStageIds = new();
        }

        readonly string _path;
        readonly SaveData _data;
        bool _hasUnsavedChanges;

        public JsonStageClearStore(string directory)
        {
            _path = Path.Combine(directory, "stage-progress.json");
            _data = Load();
        }

        public bool IsCleared(StageId stageId) => _data.clearedStageIds.Contains(stageId.Id);

        public void MarkCleared(StageId stageId)
        {
            if (!IsCleared(stageId))
            {
                _data.clearedStageIds.Add(stageId.Id);
                _hasUnsavedChanges = true;
            }

            if (!_hasUnsavedChanges) return;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path));
                var temporaryPath = _path + ".tmp";
                File.WriteAllText(temporaryPath, JsonUtility.ToJson(_data, true));
                if (File.Exists(_path))
                    File.Replace(temporaryPath, _path, null);
                else
                    File.Move(temporaryPath, _path);

                _hasUnsavedChanges = false;
            }
            catch (Exception exception) when (exception is IOException || exception is UnauthorizedAccessException)
            {
                Debug.LogWarning($"ステージのクリア状況を保存できませんでした。{exception.Message}");
            }
        }

        SaveData Load()
        {
            try
            {
                var data = JsonUtility.FromJson<SaveData>(File.ReadAllText(_path));
                if (data?.clearedStageIds != null) return data;
                Debug.LogWarning("ステージのセーブデータにクリア済みステージの一覧がありません。");
            }
            catch (FileNotFoundException)
            {
                // 初回プレイ
            }
            catch (DirectoryNotFoundException)
            {
                // 初回プレイ
            }
            catch (Exception exception) when (exception is IOException
                || exception is UnauthorizedAccessException || exception is ArgumentException)
            {
                Debug.LogWarning($"ステージのクリア状況を読み込めませんでした。{exception.Message}");
            }

            return new SaveData();
        }
    }
}
