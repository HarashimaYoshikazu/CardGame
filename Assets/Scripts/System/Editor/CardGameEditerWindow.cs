using UnityEngine;
using UnityEditor;

public class CardGameEditerWindow : EditorWindow
{
    [MenuItem("Editor/Sample")]
    private static void Create()
    {
        // 生成
        GetWindow<CardGameEditerWindow>("サンプル");
    }
}
