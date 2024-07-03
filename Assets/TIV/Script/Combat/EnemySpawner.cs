using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SceneSingleton<EnemySpawner>
{
    [SerializeField] List<GameObject> SpawnEnemyPrf;
    [SerializeField] List<GameObject> SpawnEnemyPrf_Boss;
    Dictionary<int, ShipMaster> _activeShipDic = new Dictionary<int, ShipMaster>();

    Action<int> _onActiveEnemyCountChanged;

    public void Register_onActiveEnemyCountChanged(Action<int> callBack)
    {
        _onActiveEnemyCountChanged += callBack;
    }
    public void UpRegister_onActiveEnemyCountChanged(Action<int> callBack)
    {
        _onActiveEnemyCountChanged -= callBack;
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
        int useIndex = prmStage - 1;
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

    void Spawn(GameObject prf, float warpTime)
    {
        GameObject fleet = Instantiate(prf, this.transform);
        while (fleet.transform.childCount != 0)
        {            
            ShipMaster shipMaster = fleet.transform.GetChild(0).GetComponent<ShipMaster>();
            shipMaster.Init(warpTime);
            AddActiveShip_Enemy(shipMaster);
            shipMaster.Register_OnDead(RemoveActiveShip_Enemy);
            shipMaster.Register_OnRamove(RemoveActiveShip_Enemy);

            shipMaster.transform.SetParent(this.transform);
        }

        Destroy(fleet);
    }
}
