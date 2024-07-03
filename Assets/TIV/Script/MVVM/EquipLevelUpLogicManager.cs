using System;
using UnityEngine;

namespace kjh
{
    public class EquipLevelUpLogicManager
    {
        private static EquipLevelUpLogicManager _instance = null;
        static UserHaveEquipData _equipData;
        static int _levelUpCount = 1;
        static int _needCreditLevelUp = 1;
        /// <summary>
        /// 레벨업 카운트, 소모 크레딧
        /// </summary>
        Action<int, int> _onUpgradeInfoCallBack;

        public static EquipLevelUpLogicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EquipLevelUpLogicManager();                    
                }
                return _instance;
            }
        }
        public void Register_OnUpgradeInfoCallBack(Action<int, int> callBack)
        {
            _onUpgradeInfoCallBack += callBack;
        }
        public void UnRegister_OnUpgradeInfoCallBack(Action<int, int> callBack)
        {
            _onUpgradeInfoCallBack -= callBack;
        }

        /// <summary>
        /// 장비를 선택했을 때 호출하도록 연결
        /// </summary>
        /// <param name="shipKey"></param>
        public void RefreshEquipInfo(string equipUniqeKey, Action<UserHaveEquipData> callback)
        {
            _equipData = JsonDataManager.DataLode_UserHaveEquipData(equipUniqeKey);
            InitLevelUpCount();
            SetNeedCredit(_equipData._level, _levelUpCount);
            _onUpgradeInfoCallBack?.Invoke(_levelUpCount, _needCreditLevelUp);
            callback.Invoke(_equipData);
        }
        /// <summary>
        /// 강화 버튼 좌우의 +- 버튼을 눌렀을 때 호출하도록 연결
        /// </summary>
        /// <param name="addNum"></param>
        public void ChangeLevelUpCount(int addNum)
        {
            _levelUpCount = Mathf.Clamp(_levelUpCount + addNum, 1, UserHaveEquipData.MaxLevel() - _equipData._level);
            SetNeedCredit(_equipData._level, _levelUpCount);
            _onUpgradeInfoCallBack?.Invoke(_levelUpCount, _needCreditLevelUp);
        }
        void InitLevelUpCount()
        {
            _levelUpCount = 1;
        }
        void SetNeedCredit(int curEquipLevel, int levelUpCount)
        {            
            int creditTemp = 0;

            for (int i = 0; i < levelUpCount; i++)
            {
                curEquipLevel++;
                creditTemp += (int)(curEquipLevel * 12000 * (((curEquipLevel / 4) * (curEquipLevel / 4)) + 1));
            }
            _needCreditLevelUp = creditTemp;
        }
        /// <summary>
        /// 강화 버튼을 눌렀을 때 호출하도록 연결
        /// </summary>
        public bool UpgradeEquip(Action<UserHaveEquipData> callback)
        {
            if (JsonDataManager.DataLode_UserData().TryUseCredit(_needCreditLevelUp))
            {
                _equipData.LevelUp(_levelUpCount);
                InitLevelUpCount();
                SetNeedCredit(_equipData._level, _levelUpCount);
                callback.Invoke(_equipData);
                _onUpgradeInfoCallBack?.Invoke(_levelUpCount, _needCreditLevelUp);
                return true;
            }           
            else
            {
                return false;
            }
        }
    }
}