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
        static Dictionary<int, ShipMaster> _activeShipList = new Dictionary<int, ShipMaster>();
        Action<Dictionary<int, ShipMaster>> _shipListChangeCallBack;
        Action<int, WeaponProjectileType, Vector3, bool> _onDmgedCallBack;
        Action _onDmgedCallBack_NoData;
        Action<ShipData> _onShipDataChange;
        int _selectedShipID;

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
        public static ShipData GetShipData(int key)
        {
            return _shipDatas[key];
        }

        public void RegisterLevelUpCallback(Action<int, int> levelupCallback)
        {
            _levelUpCallback += levelupCallback;
        }

        public void UnRegisterLevelUpCallback(Action<int, int> levelupCallback)
        {
            _levelUpCallback -= levelupCallback;
        }

        //===============================================================================================

        public void Register_shipListChangeCallBack(Action<Dictionary<int, ShipMaster>> callback)
        {
            _shipListChangeCallBack += callback;
        }

        public void UnRegister_shipListChangeCallBack(Action<Dictionary<int, ShipMaster>> callback)
        {
            _shipListChangeCallBack -= callback;
        }
        public void AddActiveShip(ShipMaster shipMaster)
        {
            if(_activeShipList == null)
            {
                _activeShipList = new Dictionary<int, ShipMaster>();
            }

            _activeShipList.Add(shipMaster.GetInstanceID(), shipMaster);
            _shipListChangeCallBack?.Invoke(_activeShipList);
        }
        public void RemoveActiveShip(ShipMaster shipMaster)
        {
            _activeShipList.Remove(shipMaster.GetInstanceID());
            _shipListChangeCallBack.Invoke(_activeShipList);
        }
        public void RefreshActiveShip(Action<Dictionary<int, ShipMaster>> callback)
        {            
            if(_activeShipList == null)
            {
                _activeShipList = new Dictionary<int, ShipMaster>();
            }
            callback.Invoke(_activeShipList);
        }
        //=====================================================================================
        public void Register_onDmgedCallBack(Action<int, WeaponProjectileType, Vector3, bool> callback)
        {
            _onDmgedCallBack += callback;
        }

        public void UnRegister_onDmgedCallBack(Action<int, WeaponProjectileType, Vector3, bool> callback)
        {
            _onDmgedCallBack -= callback;
        }
        public void Register_onDmgedCallBack(Action callback)
        {
            _onDmgedCallBack_NoData += callback;
        }

        public void UnRegister_onDmgedCallBack(Action callback)
        {
            _onDmgedCallBack_NoData -= callback;
        }

        public void OnDameged(float viewDmg, WeaponProjectileType type, Vector3 position, bool isCrit)
        {
            _onDmgedCallBack_NoData.Invoke();
            _onDmgedCallBack.Invoke((int)viewDmg, type, position, isCrit);
        }

        //======================================================================================
        public void Register_OnShipDataChange(Action<ShipData> callback)
        {
            _onShipDataChange += callback;
        }

        public void UnRegister_OnShipDataChange(Action<ShipData> callback)
        {
            _onShipDataChange -= callback;
        }
        public void RefreshShipInfo(int requestId, Action<ShipData> callback)
        {
            Debug.Log($"선택 함선 {requestId}");
            _selectedShipID = requestId;
            ShipData shipData = _shipDatas[requestId];
            shipData.AllStaticDataUpdate();            
            callback.Invoke(shipData);
        }
        public void OnShipDataChenge(int requestId)
        {
            if (_selectedShipID == requestId)
            {
                Debug.Log("데이터 갱신");
                _onShipDataChange?.Invoke(_shipDatas[requestId]);
            }
       }
        public void RefreshUserItem(Action<long, long> callback)
        {
            long credit = JsonDataManager.DataLode_UserHaveItemData(ItemType.Credit)._value;
            long superCredit = JsonDataManager.DataLode_UserHaveItemData(ItemType.SuperCredit)._value;
            callback.Invoke(credit, superCredit);
        }

        public void ShipEquipItem(string equipUniqeKey, int shipKey, Action<ShipData> callback)
        {            
            UserHaveShipData shipDataOrigin = JsonDataManager.DataLode_UserHaveShipData(shipKey);

            shipDataOrigin.Equip(equipUniqeKey);

            //JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache.AllDicItemUpdate_EquipedShipKey();
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache, UserHaveEquipDataDictionary.FilePath());
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveShipDataListCache, UserHaveShipDataList.FilePath());

            ShipData shipData = _shipDatas[shipKey];
            shipData.AllStaticDataUpdate();
            callback.Invoke(shipData);
        }
        public void ShipUnEquipItem(string equipUniqeKey, Action<ShipData> callback)
        {
            int shipKey = JsonDataManager.DataLode_UserHaveEquipData(equipUniqeKey)._equipedShipKey;

            JsonDataManager.DataLode_UserHaveShipData(shipKey).Unequip(equipUniqeKey);

            //JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache.AllDicItemUpdate_EquipedShipKey();
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache, UserHaveEquipDataDictionary.FilePath());
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveShipDataListCache, UserHaveShipDataList.FilePath());

            ShipData shipData = _shipDatas[shipKey];
            shipData.AllStaticDataUpdate();
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
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache, UserHaveEquipDataDictionary.FilePath());
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