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
        shipMaster.Register_OnDead(RemoveActiveShip_Player);
        UserData.Instance.SetShipPosDatas(_activeShipDic);
    }
    public void RemoveActiveShip_Player(ShipMaster shipMaster)
    {
        _activeShipDic.Remove(shipMaster.GetInstanceID());
        UserData.Instance.SetShipPosDatas(_activeShipDic);
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
        ShipSpawnAndInit(shipKey, spawnPos);        
    }

    void ShipSpawnAndInit(int shipKey, Vector3 spawnPos)
    {
        ShipMaster shipMaster = Instantiate(Prefab_ShipList[shipKey], spawnPos, Quaternion.identity, this.transform).GetComponent<ShipMaster>();
        AddActiveShip_Player(shipMaster);
    }
}
