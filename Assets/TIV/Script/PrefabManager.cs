using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : SceneSingleton<PrefabManager>
{
    [SerializeField] List<GameObject> Prefab_ProjectilesList;

    public GameObject GetProjectilePrf()
    {
        return Prefab_ProjectilesList[0];
    }
}
