using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : SceneSingleton<PrefabManager>
{
    [SerializeField] List<GameObject> Prefab_ProjectilesList;
    Dictionary<string, GameObject> Prefab_ProjectilesDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Instance.Prefab_ProjectilesDic = new Dictionary<string, GameObject>();

        foreach (GameObject prf in Prefab_ProjectilesList)
        {
            Prefab_ProjectilesDic.Add(prf.name, prf);
        }
    }

    public GameObject GetProjectilePrf(string key)
    {
        if(Prefab_ProjectilesDic.ContainsKey(key) == false)
        {
            Debug.LogWarning("투사체 키에 해당하는 오브젝트가 존재하지 않습니다. 기본 투사체를 반환합니다.");
            return Prefab_ProjectilesList[0];
        }

        return Prefab_ProjectilesDic[key];
    }
}
