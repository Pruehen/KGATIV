using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : SceneSingleton<PlayerSpawner>
{
    [SerializeField] List<GameObject> Prefab_ShipList;
    int _useIndex;

    private void Start()
    {        
        InitSpawn_GetSaveData();
    }

    void InitSpawn_GetSaveData()
    {
        _useIndex = 0;
        if (UserData.Instance.GetShipPosDataList() != null)
        {
            foreach (var shipPosData in UserData.Instance.GetShipPosDataList())
            {
                Vector3 newPos = new Vector3(shipPosData._posX, 0, shipPosData._posZ);
                SpawndShipInit(shipPosData._shipKey, newPos);
            }
        }
    }

    public void NewShipSpawn(int shipKey, Vector3 spawnPos)
    {
        SpawndShipInit(shipKey, spawnPos);
        UserData.Instance.AddShipPosData(shipKey, spawnPos);
    }

    void SpawndShipInit(int shipKey, Vector3 spawnPos)
    {
        GameObject ship = Instantiate(Prefab_ShipList[shipKey], spawnPos, Quaternion.identity, this.transform);
        ship.GetComponent<ShipMaster>().ShipIndex = _useIndex++;        
    }
}
