using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SceneSingleton<EnemySpawner>
{
    [Header("탐색 임무")]
    [SerializeField] List<GameObject> SpawnEnemyPrf;
    [SerializeField] List<GameObject> SpawnEnemyPrf_Boss;

    [Header("전투 임무")]
    [SerializeField] GameObject Prefab_EnemyFleet_Request_1;
    [SerializeField] GameObject Prefab_EnemyFleet_Request_2;
    [SerializeField] GameObject Prefab_EnemyFleet_Request_3;
    [SerializeField] GameObject Prefab_EnemyFleet_Request_4;

    [SerializeField] GameObject Prefab_EnemyFleet_Alpha;
    [SerializeField] GameObject Prefab_EnemyFleet_Beta;
    [SerializeField] GameObject Prefab_EnemyFleet_Gamma;
    [SerializeField] GameObject Prefab_EnemyFleet_Delta;


    Dictionary<int, ShipMaster> _activeShipDic = new Dictionary<int, ShipMaster>();

    Action<int> _onActiveEnemyCountChanged;

    public void Register_onActiveEnemyCountChanged(Action<int> callBack)
    {
        _onActiveEnemyCountChanged += callBack;
    }
    public void UnRegister_onActiveEnemyCountChanged(Action<int> callBack)
    {
        _onActiveEnemyCountChanged -= callBack;        
    }
    public void Invoke_onActiveEnemyCountChanged()
    {
        _onActiveEnemyCountChanged.Invoke(_activeShipDic.Count);
    }

    private void Start()
    {               
        Spawn_NavStage(UserData.Instance.CurPrmStage, UserData.Instance.CurSecStage, 2);
    }

    void AddActiveShip_Enemy(ShipMaster shipMaster)
    {
        if (_activeShipDic == null)
        {
            _activeShipDic = new Dictionary<int, ShipMaster>();
        }

        _activeShipDic.Add(shipMaster.GetInstanceID(), shipMaster);
    }
    public void RemoveActiveShip_Enemy(ShipMaster shipMaster)
    {
        _activeShipDic.Remove(shipMaster.GetInstanceID());
        _onActiveEnemyCountChanged.Invoke(_activeShipDic.Count);
    }

    public void Spawn_NavStage(int prmStage, int secStage, float warpTime)
    {
        int useIndex = prmStage % SpawnEnemyPrf.Count;
        if (secStage != 10)//일반 스테이지
        {
            if (SpawnEnemyPrf.Count <= useIndex)
            {
                useIndex = SpawnEnemyPrf.Count - 1;
            }
            Spawn(SpawnEnemyPrf[useIndex], warpTime);
        }
        else//보스 스테이지
        {
            if (SpawnEnemyPrf_Boss.Count <= useIndex)
            {
                useIndex = SpawnEnemyPrf_Boss.Count - 1;
            }
            Spawn(SpawnEnemyPrf_Boss[useIndex], warpTime);
        }
    }
    public void Spawn_Request_1()
    {
        Spawn(Prefab_EnemyFleet_Request_1, 15);
    }
    public void Spawn_Request_2()
    {
        Spawn(Prefab_EnemyFleet_Request_2, 15);
    }
    public void Spawn_Request_3()
    {
        Spawn(Prefab_EnemyFleet_Request_3, 15);
    }
    public void Spawn_Request_4()
    {
        Spawn(Prefab_EnemyFleet_Request_4, 15);
    }
    public void Spawn_Alpha()
    {
        Spawn(Prefab_EnemyFleet_Alpha, 15);
    }
    public void Spawn_Beta()
    {
        Spawn(Prefab_EnemyFleet_Beta, 15);
    }
    public void Spawn_Gamma()
    {
        Spawn(Prefab_EnemyFleet_Gamma, 15);
    }
    public void Spawn_Delta()
    {
        Spawn(Prefab_EnemyFleet_Delta, 15);
    }


    void Spawn(GameObject prf, float warpTime)
    {
        GameObject fleet = Instantiate(prf, this.transform);
        while (fleet.transform.childCount != 0)
        {            
            ShipMaster shipMaster = fleet.transform.GetChild(0).GetComponent<ShipMaster>();
            shipMaster.Init(warpTime * UnityEngine.Random.Range(0.9f, 1.1f));
            AddActiveShip_Enemy(shipMaster);
            shipMaster.Register_OnDead(RemoveActiveShip_Enemy);
            shipMaster.Register_OnRamove(RemoveActiveShip_Enemy);

            shipMaster.transform.SetParent(this.transform);
        }

        Destroy(fleet);
    }
}
