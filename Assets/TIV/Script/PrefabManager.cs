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
            Debug.LogWarning("����ü Ű�� �ش��ϴ� ������Ʈ�� �������� �ʽ��ϴ�. �⺻ ����ü�� ��ȯ�մϴ�.");
            return Prefab_ProjectilesList[0];
        }

        return Prefab_ProjectilesDic[key];
    }
}
