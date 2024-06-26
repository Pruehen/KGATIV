using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SceneSingleton<EnemySpawner>
{
    [SerializeField] List<GameObject> SpawnEnemyPrf;
    [SerializeField] List<GameObject> SpawnEnemyPrf_Boss;
    Dictionary<int, ShipMaster> _activeShipDic = new Dictionary<int, ShipMaster>();

    private void Start()
    {
        UserData userData = JsonDataManager.DataLode_UserData();        
        Spawn_SetIndex(userData.CurPrmStage, userData.CurSecStage);
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
        NextStageCheck_OnRemoveActiveShip_Enemy();
    }
    void NextStageCheck_OnRemoveActiveShip_Enemy()
    {
        if(_activeShipDic.Count == 0)
        {
            UserData userData = JsonDataManager.DataLode_UserData();
            userData.StageUp();
            Spawn_SetIndex(userData.CurPrmStage, userData.CurSecStage);
        }
    }

    void Spawn_SetIndex(int prmStage, int secStage)
    {
        int useIndex = prmStage - 1;
        if (secStage != 10)//일반 스테이지
        {
            if (SpawnEnemyPrf.Count <= useIndex)
            {
                useIndex = SpawnEnemyPrf.Count - 1;
            }
            Spawn(SpawnEnemyPrf[useIndex]);
        }
        else//보스 스테이지
        {
            if (SpawnEnemyPrf_Boss.Count <= useIndex)
            {
                useIndex = SpawnEnemyPrf_Boss.Count - 1;
            }
            Spawn(SpawnEnemyPrf_Boss[useIndex]);
        }
    }

    void Spawn(GameObject prf)
    {
        GameObject fleet = Instantiate(prf, this.transform);
        while (fleet.transform.childCount != 0)
        {            
            ShipMaster shipMaster = fleet.transform.GetChild(0).GetComponent<ShipMaster>();
            AddActiveShip_Enemy(shipMaster);
            shipMaster.Register_OnDead(RemoveActiveShip_Enemy);

            shipMaster.transform.SetParent(this.transform);
        }

        Destroy(fleet);
    }
}
