// Guarda esto en Assets/Editor/FindMissingScripts.cs
using UnityEngine;
using UnityEditor;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void ShowWindow()
        => GetWindow<FindMissingScripts>("Find Missing Scripts");

    void OnGUI()
    {
        if (GUILayout.Button("Buscar en la escena actual"))
            SearchInScene();
    }

    static void SearchInScene()
    {
        int missingCount = 0;
        foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
        {
            Component[] comps = go.GetComponents<Component>();
            for (int i = 0; i < comps.Length; i++)
            {
                if (comps[i] == null)
                {
                    Debug.LogWarning(
                        $"Missing script on: {FullPath(go)}", go
                    );
                    missingCount++;
                }
            }
        }
        Debug.Log($"Total de componentes faltantes: {missingCount}");
    }

    static string FullPath(GameObject go)
    {
        string path = go.name;
        while (go.transform.parent != null)
        {
            go = go.transform.parent.gameObject;
            path = go.name + "/" + path;
        }
        return path;
    }
}