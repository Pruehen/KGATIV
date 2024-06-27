using System;
using UnityEngine;

namespace kjh
{
    public class ShipLevelUpLogicManager
    {
        private static ShipLevelUpLogicManager _instance = null;
        static UserHaveShipData controlShipData;
        static int _levelUpCount = 1;
        static int _needCreditLevelUp = 1;
        /// <summary>
        /// 레벨업 카운트, 함선 레벨, 소모 크레딧
        /// </summary>
        Action<int, int, int> OnLevelUpInfoCallBack;

        public static ShipLevelUpLogicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShipLevelUpLogicManager();                    
                }
                return _instance;
            }
        }
        public void Register_OnLevelUpInfoCallBack(Action<int, int, int> callBack)
        {
            OnLevelUpInfoCallBack += callBack;
        }
        public void UnRegister_OnLevelUpInfoCallBack(Action<int, int, int> callBack)
        {
            OnLevelUpInfoCallBack -= callBack;
        }

        /// <summary>
        /// 함선을 선택했을 때 호출하도록 연결
        /// </summary>
        /// <param name="shipKey"></param>
        public void SetShipData(int shipKey)
        {
            controlShipData = JsonDataManager.DataLode_UserHaveShipData(shipKey);
            InitLevelUpCount();
            SetNeedCredit(controlShipData._level, _levelUpCount);
            OnLevelUpInfoCallBack?.Invoke(_levelUpCount, controlShipData._level, _needCreditLevelUp);
        }
        /// <summary>
        /// 레벨업 버튼 좌우의 +- 버튼을 눌렀을 때 호출하도록 연결
        /// </summary>
        /// <param name="addNum"></param>
        public void ChangeLevelUpCount(int addNum)
        {
            _levelUpCount = Mathf.Clamp(_levelUpCount + addNum, 1, UserHaveShipData.MaxLevel() - controlShipData._level);
            SetNeedCredit(controlShipData._level, _levelUpCount);
            OnLevelUpInfoCallBack?.Invoke(_levelUpCount, controlShipData._level, _needCreditLevelUp);
        }
        void InitLevelUpCount()
        {
            _levelUpCount = 1;
        }
        void SetNeedCredit(int curShipLevel, int levelUpCount)
        {
            ShipTable table = JsonDataManager.DataLode_ShipTable(controlShipData._shipTablekey);
            float multiple = table._maxCombatSlot * table._maxCombatSlot * table._star * table._star + 37;
            int creditTemp = 0;

            for (int i = 0; i < levelUpCount; i++)
            {
                curShipLevel++;
                creditTemp += (int)(curShipLevel * multiple);                
            }
            _needCreditLevelUp = creditTemp;
        }
        /// <summary>
        /// 레벨업 버튼을 눌렀을 때 호출하도록 연결
        /// </summary>
        public void LevelUp()
        {                        
            if(JsonDataManager.DataLode_UserData().TryUseCredit(_needCreditLevelUp))
            {
                controlShipData.LevelUp(_levelUpCount);
                InitLevelUpCount();
                SetNeedCredit(controlShipData._level, _levelUpCount);
                OnLevelUpInfoCallBack?.Invoke(_levelUpCount, controlShipData._level, _needCreditLevelUp);
                kjh.GameLogicManager.Instance.OnShipDataChenge();
            }            
        }
    }
}