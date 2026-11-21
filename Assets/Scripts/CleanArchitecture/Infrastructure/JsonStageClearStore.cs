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
            public List<StageBestScore> bestScores = new();
        }

        [Serializable]
        sealed class StageBestScore
        {
            public string stageId;
            public int score;
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

        public int? GetBestScore(StageId stageId)
            => _data.bestScores.Find(record => record.stageId == stageId.Id)?.score;

        public void RecordResult(StageId stageId, int score, bool cleared)
        {
            var record = _data.bestScores.Find(entry => entry.stageId == stageId.Id);
            if (record == null)
            {
                _data.bestScores.Add(new StageBestScore { stageId = stageId.Id, score = score });
                _hasUnsavedChanges = true;
            }
            else if (score > record.score)
            {
                record.score = score;
                _hasUnsavedChanges = true;
            }

            if (cleared && !IsCleared(stageId))
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

        public bool TryReset()
        {
            try
            {
                File.Delete(_path + ".tmp");
                File.Delete(_path);
                _data.clearedStageIds.Clear();
                _data.bestScores.Clear();
                _hasUnsavedChanges = false;
                return true;
            }
            catch (Exception exception) when (exception is IOException || exception is UnauthorizedAccessException)
            {
                Debug.LogWarning($"セーブデータをリセットできませんでした。{exception.Message}");
                return false;
            }
        }

        SaveData Load()
        {
            try
            {
                var data = JsonUtility.FromJson<SaveData>(File.ReadAllText(_path));
                if (data?.clearedStageIds != null)
                {
                    data.bestScores ??= new List<StageBestScore>();
                    return data;
                }
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
