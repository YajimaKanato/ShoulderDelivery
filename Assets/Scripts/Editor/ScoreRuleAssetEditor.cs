using ShoulderDelivery.Infrastructure;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>スコアルールを定義するアセットをカスタムするクラス</summary>
[CustomEditor(typeof(ScoreRulesAsset))]
public class ScoreRuleAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("スコア計算に必要なアセットをアサイン"))
        {
            AssignBonusTables();
        }
    }

    /// <summary>
    /// ボーナステーブルアセットをアサインするメソッド
    /// </summary>
    void AssignBonusTables()
    {
        // ボタンを押したアセットのクラスを取得
        var scoreRule = (ScoreRulesAsset)target;

        var folder = AssetDatabase.GetAssetPath(scoreRule);
        folder = folder.Substring(0, folder.LastIndexOf('/'));

        if (!TryGetAsset("t:DeliveryComboBonusRuleAsset", folder, out DeliveryComboBonusRuleAsset deliveryComboBonusRuleAsset))
            Debug.LogWarning($"\"{folder}\"にDeliveryComboBonusRuleAssetがありません", scoreRule);

        if (!TryGetAsset("t:SpeedBonusRuleAsset", folder, out SpeedBonusRuleAsset speedBonusRuleAsset))
            Debug.LogWarning($"\"{folder}\"にSpeedBonusRuleAssetがありません", scoreRule);

        if (!TryGetAsset("t:DistanceBonusRuleAsset", folder, out DistanceBonusRuleAsset distanceBonusRuleAsset))
            Debug.LogWarning($"\"{folder}\"にDistanceBonusRuleAssetがありません", scoreRule);

        if (!TryGetAsset("t:RemainingTimeBonusRuleAsset", folder, out RemainingTimeBonusRuleAsset remainingTimeBonusRuleAsset))
            Debug.LogWarning($"\"{folder}\"にRemainingTimeBonusRuleAssetがありません", scoreRule);

        // すべてなかった場合はreturn
        if (!(deliveryComboBonusRuleAsset
            || speedBonusRuleAsset
            || distanceBonusRuleAsset
            || remainingTimeBonusRuleAsset))
            return;

        Undo.RecordObject(scoreRule, "Assign ScoreRule Assets");
        scoreRule.SetDeliveryComboBonusRuleAsset(deliveryComboBonusRuleAsset);
        scoreRule.SetSpeedBonusRuleAsset(speedBonusRuleAsset);
        scoreRule.SetDistanceBonusRuleAsset(distanceBonusRuleAsset);
        scoreRule.SetRemainingTimeBonusRuleAsset(remainingTimeBonusRuleAsset);
        EditorUtility.SetDirty(scoreRule);
    }

    /// <summary>
    /// アセットを取得するメソッド
    /// </summary>
    /// <typeparam name="TAsset">取得するアセットのデータ型</typeparam>
    /// <param name="assetType">取得するアセットのデータ型を示す文字列</param>
    /// <param name="filter">フォルダのフィルターをかける文字列</param>
    /// <param name="asset">アセット</param>
    /// <returns>アセットが取得できたかどうか</returns>
    bool TryGetAsset<TAsset>(string assetType, string filter, out TAsset asset) where TAsset : Object
    {
        asset = AssetDatabase.FindAssets(assetType, new[] { filter })       // 範囲を絞って検索
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))            // guidをpathに変換
            .Select(path => AssetDatabase.LoadAssetAtPath<TAsset>(path))    // pathからアセットをロード
            .Where(asset => asset != null)
            .FirstOrDefault();

        return asset != null;
    }
}
