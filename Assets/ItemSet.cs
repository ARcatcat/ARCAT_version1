using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemSet : ScriptableObject {

#if UNITY_EDITOR
    [UnityEditor.MenuItem("innyo/Create item set")]
    public static void CreateItemSet()
    {
        var objSet = CreateInstance<ItemSet>();
        string savePath = EditorUtility.SaveFilePanel(
            "save",
            "Assets/",
            "ItemAsset",
            "asset"
        );
        if (savePath != "")
        {
            savePath = "Assets/" + savePath.Replace(Application.dataPath, "");
            UnityEditor.AssetDatabase.CreateAsset(objSet, savePath);
            UnityEditor.AssetDatabase.SaveAssets();
        }
    }
#endif

    public ItemSet[] items;
}
