using EnumTypes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace kjh
{
    public class GameLogicManager
    {
        private static GameLogicManager _instance = null;        

        private static Dictionary<int, ShipData> _shipDatas = new Dictionary<int, ShipData>();
        private Action<int, int> _levelUpCallback;

        public static GameLogicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameLogicManager();
                    TempInitPlayerList();
                }
                return _instance;
            }
        }
        public static void TempInitPlayerList()
        {
            _shipDatas.Add(0, new ShipData(JsonDataManager.DataLode_UserHaveShipData(0)));
            _shipDatas.Add(1, new ShipData(JsonDataManager.DataLode_UserHaveShipData(1)));
            _shipDatas.Add(2, new ShipData(JsonDataManager.DataLode_UserHaveShipData(2)));
            _shipDatas.Add(3, new ShipData(JsonDataManager.DataLode_UserHaveShipData(3)));
            _shipDatas.Add(4, new ShipData(JsonDataManager.DataLode_UserHaveShipData(4)));
        }

        public void RegisterLevelUpCallback(Action<int, int> levelupCallback)
        {
            _levelUpCallback += levelupCallback;
        }

        public void UnRegisterLevelUpCallback(Action<int, int> levelupCallback)
        {
            _levelUpCallback -= levelupCallback;
        }

        public void RefreshShipInfo(int requestId, Action<ShipData> callback)
        {
            ShipData shipData = _shipDatas[requestId];
            callback.Invoke(shipData);
        }
        public void RefreshEquipInfo(string equipUniqeKey, Action<UserHaveEquipData> callback)
        {
            UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(equipUniqeKey);
            callback.Invoke(equipData);
        }
        public void UpgradeEquip(string equipUniqeKey, Action<UserHaveEquipData> callback)
        {
            UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(equipUniqeKey);
            equipData.LevelUp(20);
            callback.Invoke(equipData);
        }

        public void RefreshUpgradeResult(UserHaveEquipData before, UserHaveEquipData affter, Action<int, int, int, IncreaseableStateType, List<UserHaveEquipData.EquipStateSet>> callback)
        {
            int tableKey = affter._equipTableKey;
            int prelevel = before._level;
            int affterLevel = affter._level;
            IncreaseableStateType mainStateType = affter._mainState._stateType;
            List<UserHaveEquipData.EquipStateSet> subStateList = new List<UserHaveEquipData.EquipStateSet>();
            Debug.Log($"{before._subStateList.Count} , {affter._subStateList.Count}");
            for (int i = before._subStateList.Count; i < affter._subStateList.Count; i++)
            {
                subStateList.Add(affter._subStateList[i]);
            }
            callback.Invoke(tableKey, prelevel, affterLevel, mainStateType, subStateList);
        }
    }
}