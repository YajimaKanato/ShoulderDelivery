using ShoulderDelivery.Infrastructure;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>ステージ情報を持つSOをカスタムするクラス</summary>
[CustomEditor(typeof(StageDefinitionAsset))]
public class StageDefinitionAssetEditor : Editor
{
    const string RootFolder = "Assets/ScriptableObjects/TargetDefinitions";

    public override void OnInspectorGUI()
    {
        // 通常時のInspectorを表示
        DrawDefaultInspector();

        if (GUILayout.Button("StageIdに対応するTargetのアセットを設定"))
        {
            AssignTargets();
        }
    }

    /// <summary>
    /// ターゲットをアサインするメソッド
    /// </summary>
    void AssignTargets()
    {
        // ボタンを押したアセットクラスを取得
        var stage = (StageDefinitionAsset)target;

        // 指定のフォルダの下にあるフォルダを走査
        // StageIdと一致する名前のフォルダを取得
        var folder = AssetDatabase.GetSubFolders(RootFolder)
            .FirstOrDefault(path => string.Equals(Path.GetFileName(path), stage.StageId, StringComparison.Ordinal));

        if (folder == null)
        {
            Debug.LogWarning($"StageId\"{stage.StageId}\"に対応するフォルダがありません", stage);
            return;
        }

        // アセットを取得
        var assets = AssetDatabase.FindAssets("t:TargetDefinitionAsset", new[] { folder })      // 指定のフォルダから対象のアセットを取得
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))                                // GUIDからアセットのパスを取得
            .Select(path => AssetDatabase.LoadAssetAtPath<TargetDefinitionAsset>(path))         // パスからアセットをロード
            .OrderBy(asset => int.Parse(asset.name.Split('-').Last()))                          // 配達目的地の番号で昇順ソート
            .Where(asset => asset != null)
            .ToArray();

        if (assets.Length == 0)
        {
            Debug.LogWarning($"\"{folder}\"にTargetDefinitionAssetがありません", stage);
            return;
        }

        // SOに設定
        Undo.RecordObject(stage, "Assign Target Definition");
        stage.SetTargetDefinitions(assets);
        EditorUtility.SetDirty(stage);
    }
}
