using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : SceneSingleton<PlayerSpawner>
{
    [SerializeField] List<GameObject> Prefab_ShipList;    

    Dictionary<int, ShipMaster> _activeShipDic = new Dictionary<int, ShipMaster>();

    void AddActiveShip_Player(ShipMaster shipMaster)
    {
        if (_activeShipDic == null)
        {
            _activeShipDic = new Dictionary<int, ShipMaster>();
        }

        _activeShipDic.Add(shipMaster.GetInstanceID(), shipMaster);
        FleetLogicManager.Instance.OnFleetCostChange();
        shipMaster.Register_OnExit(RemoveActiveShip_Player);        
    }
    void RemoveActiveShip_Player(ShipMaster shipMaster)
    {
        _activeShipDic.Remove(shipMaster.GetInstanceID());
        FleetLogicManager.Instance.OnFleetCostChange();
        UserData.Instance.SetShipPosDatas(_activeShipDic);
        UserData.Save();
    }
    public int GetTotalCoat()
    {
        int total = 0;
        foreach (var item in _activeShipDic)
        {
            total += item.Value.GetCost();
        }
        return total;
    }


    private void Start()
    {        
        InitSpawn_GetSaveData();
    }

    void InitSpawn_GetSaveData()
    {        
        if (UserData.Instance.GetShipPosDataList() != null)
        {
            foreach (var shipPosData in UserData.Instance.GetShipPosDataList())
            {
                Vector3 newPos = new Vector3(shipPosData._posX, 0, shipPosData._posZ);
                ShipSpawnAndInit(shipPosData._shipKey, newPos);
            }            
        }
    }

    public void NewShipSpawn(int shipKey, Vector3 spawnPos)
    {
        int tryAddShipCost = JsonDataManager.DataLode_ShipTable(shipKey)._cost;
        if(GetTotalCoat() + tryAddShipCost > UserData.Instance.FleetCost)
        {
            UIManager.Instance.PopUpWdw_WarningPopUpUI("수용량을 초과합니다. 함선을 배치할 수 없습니다.");
        }
        else
        {
            ShipSpawnAndInit(shipKey, spawnPos);
            UserData.Instance.SetShipPosDatas(_activeShipDic);
            UserData.Save();
        }        
    }

    void ShipSpawnAndInit(int shipKey, Vector3 spawnPos)
    {
        ShipMaster shipMaster = Instantiate(Prefab_ShipList[shipKey], spawnPos, Quaternion.identity, this.transform).GetComponent<ShipMaster>();
        AddActiveShip_Player(shipMaster);
    }
}
