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
        /// ������ ī��Ʈ, �Լ� ����, �Ҹ� ũ����
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
        /// �Լ��� �������� �� ȣ���ϵ��� ����
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
        /// ������ ��ư �¿��� +- ��ư�� ������ �� ȣ���ϵ��� ����
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
        /// ������ ��ư�� ������ �� ȣ���ϵ��� ����
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