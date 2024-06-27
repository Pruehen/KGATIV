using EnumTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace kjh
{
    public class GameLogicManager
    {
        private static GameLogicManager _instance = null;        

        private static Dictionary<int, ShipData> _shipDatas = new Dictionary<int, ShipData>();
        static Dictionary<int, ShipMaster> _activeShipDic = new Dictionary<int, ShipMaster>();
        Action<ShipMaster, bool> _shipListChangeCallBack;
        private TaskCompletionSource<bool> _callbackRegisteredTcs;

        Action<int, WeaponProjectileType, Vector3, bool> _onDmgedCallBack;
        Action _onDmgedCallBack_NoData;
        Action<ShipData> _onShipDataChange;
        int _selectedShipID;        
        

        public static GameLogicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameLogicManager();
                    //TempInitPlayerList();
                }
                return _instance;
            }
        }
        public static ShipData GetShipData(int key)
        {
            if (_shipDatas.ContainsKey(key))
            {
                return _shipDatas[key];
            }
            else
            {
                _shipDatas.Add(key, new ShipData(JsonDataManager.DataLode_UserHaveShipData(key)));
                return _shipDatas[key];
            }
        }

        //===============================================================================================

        public void Register_shipListChangeCallBack(Action<ShipMaster, bool> callback)
        {
            _shipListChangeCallBack += callback;
            _callbackRegisteredTcs?.TrySetResult(true);
        }

        public void UnRegister_shipListChangeCallBack(Action<ShipMaster, bool> callback)
        {
            _shipListChangeCallBack -= callback;
        }
        public async void AddActiveShip(ShipMaster shipMaster)
        {
            if(_activeShipDic == null)
            {
                _activeShipDic = new Dictionary<int, ShipMaster>();
            }

            _activeShipDic.Add(shipMaster.GetInstanceID(), shipMaster);

            if (_shipListChangeCallBack == null)
            {
                await WaitForCallbackRegistration();
            }
            _shipListChangeCallBack.Invoke(shipMaster, true);
        }
        async Task WaitForCallbackRegistration()
        {
            if (_callbackRegisteredTcs == null || _callbackRegisteredTcs.Task.IsCompleted)
            {
                _callbackRegisteredTcs = new TaskCompletionSource<bool>();
            }

            await _callbackRegisteredTcs.Task;
        }

        public void RemoveActiveShip(ShipMaster shipMaster)
        {
            _activeShipDic.Remove(shipMaster.GetInstanceID());
            _shipListChangeCallBack.Invoke(shipMaster, false);
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
            _onDmgedCallBack_NoData?.Invoke();
            _onDmgedCallBack?.Invoke((int)viewDmg, type, position, isCrit);
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
            ShipData shipData = GetShipData(requestId);
            shipData.AllStaticDataUpdate();            
            callback.Invoke(shipData);
        }
        public void OnShipDataChenge(int requestId)
        {
            if (_selectedShipID == requestId)
            {
                Debug.Log("데이터 갱신");
                _onShipDataChange?.Invoke(GetShipData(requestId));
            }
       }

        public void ShipEquipItem(string equipUniqeKey, int shipKey, Action<ShipData> callback)
        {            
            UserHaveShipData shipDataOrigin = JsonDataManager.DataLode_UserHaveShipData(shipKey);

            shipDataOrigin.Equip(equipUniqeKey);

            //JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache.AllDicItemUpdate_EquipedShipKey();
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache, UserHaveEquipDataDictionary.FilePath());
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveShipDataListCache, UserHaveShipDataList.FilePath());

            ShipData shipData = GetShipData(shipKey);
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

            ShipData shipData = GetShipData(shipKey);
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