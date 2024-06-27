using kjh;

namespace ViewModel.Extensions
{
    public static class EquipInfoUIManagerViewModelExtension
    {
        public static void Register_OnUpgradeInfoCallBack(this EquipInfoUIManagerViewModel vm)
        {
            EquipLevelUpLogicManager.Instance.Register_OnUpgradeInfoCallBack(vm.OnRefreshViewModel_UpgradeUI);
        }
        public static void UnRegister_OnUpgradeInfoCallBack(this EquipInfoUIManagerViewModel vm)
        {
            EquipLevelUpLogicManager.Instance.UnRegister_OnUpgradeInfoCallBack(vm.OnRefreshViewModel_UpgradeUI);
        }

        public static void RefreshViewModel(this EquipInfoUIManagerViewModel vm, string equipUniqeKey)//��û �ͽ��ټ�
        {            
            EquipLevelUpLogicManager.Instance.RefreshEquipInfo(equipUniqeKey, vm.OnAllRefreshViewModel);//�ݹ� ȣ��
        }
        public static void Command_ChangeLevelUpCount(this EquipInfoUIManagerViewModel vm, int changedLevelUpCount)
        {
            EquipLevelUpLogicManager.Instance.ChangeLevelUpCount(changedLevelUpCount);
        }
        public static void CommandUpgrade(this EquipInfoUIManagerViewModel vm)
        {
            EquipLevelUpLogicManager.Instance.UpgradeEquip(vm.OnAllRefreshViewModel);//�ݹ� ȣ��
        }

        public static void OnAllRefreshViewModel(this EquipInfoUIManagerViewModel vm, UserHaveEquipData equipData)//�ݹ�
        {
            vm.UniqueKey = equipData._itemUniqueKey;
            vm.TableKey = equipData._equipTableKey;
            vm.Name = equipData.GetName();
            vm.Type = equipData.GetEquipType();
            vm.MainStateType = equipData._mainState._stateType;
            vm.Level = equipData._level;
            vm.SubStateList = equipData._subStateList;
            vm.SetType = equipData._setType;            
        }
        public static void OnRefreshViewModel_UpgradeUI(this EquipInfoUIManagerViewModel vm, int levelUpCount, int needCredit)
        {
            vm.LevelUpCount = levelUpCount;
            vm.NeedCreditLevelUp = needCredit;
        }
    }
}
