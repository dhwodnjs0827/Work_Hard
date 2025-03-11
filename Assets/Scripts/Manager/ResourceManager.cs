using System;
using System.Collections.Generic;
using System.IO;
using DataDeclaration;
using UnityEngine;

public class ResourceManager: Singleton<ResourceManager>
{
    private static Dictionary<PrefabType, List<GameObject>> prefabDict;

    protected override void Awake()
    {
        base.Awake();
        prefabDict = new Dictionary<PrefabType, List<GameObject>>();

        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        var prefabsDirectory = new DirectoryInfo(Application.dataPath + "/Resources/Prefabs/");
        var directories = prefabsDirectory.GetDirectories();
        foreach (var directory in directories)
        {
            if (!Enum.TryParse<PrefabType>(directory.Name, out var type))
            {
                Debug.LogWarning($"'{directory.Name}'의 enum 값 없음.");
                continue;
            }
            var prefabResource = Resources.LoadAll<GameObject>("Prefabs/" + directory.Name);
            foreach (var prefab in prefabResource)
            {
                if (!prefabDict.TryGetValue(type, out var list))
                {
                    list = new List<GameObject>();
                    prefabDict[type] = list;
                }
                if (!list.Contains(prefab))
                {
                    list.Add(prefab);
                }
            }
        }
    }

    public static GameObject GetResource(PrefabType type, string name)
    {
        return prefabDict.TryGetValue(type, out var list) ? list.Find(x => x.name == name) : null;
    }
}
