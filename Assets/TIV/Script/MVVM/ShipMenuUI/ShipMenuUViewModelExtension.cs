using EnumTypes;
using kjh;

namespace ViewModel.Extensions
{
    public static class ShipMenuUIViewModelExtension
    {
        public static void Register_OnShipDataChange(this ShipMenuUIViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_OnShipDataChange(vm.OnChangeShipData);
        }

        public static void UnRegister_OnShipDataChange(this ShipMenuUIViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_OnShipDataChange(vm.OnChangeShipData);
        }

        public static void RefreshShipData(this ShipMenuUIViewModel vm, int shipKey)//��û �ͽ��ټ�
        {            
            kjh.GameLogicManager.Instance.RefreshShipInfo(shipKey, vm.OnChangeShipData);//�ݹ� ȣ��
        }
        public static void CommandEquip(this ShipMenuUIViewModel vm, string equipUniqeKey, int shipKey)
        {
            kjh.GameLogicManager.Instance.ShipEquipItem(equipUniqeKey, shipKey, vm.OnChangeShipData);//�ݹ� ȣ��
        }
        public static void CommandUnEquip(this ShipMenuUIViewModel vm, string equipUniqeKey)
        {
            kjh.GameLogicManager.Instance.ShipUnEquipItem(equipUniqeKey, vm.OnChangeShipData);//�ݹ� ȣ��
        }

        public static void OnChangeShipData(this ShipMenuUIViewModel vm, ShipData shipData)//�ݹ�
        {
            vm.Name = shipData.GetName();
            vm.Class = shipData.GetShipClass();
            vm.Star = shipData.GetShipStar();
            vm.Level = shipData.GetLevel();
            vm.Hp = shipData.GetFinalState(CombatStateType.Hp);
            vm.Atk = shipData.GetFinalState(CombatStateType.Atk);
            vm.Def = shipData.GetFinalState(CombatStateType.Def);
            vm.CritRate = shipData.GetFinalState(CombatStateType.CritRate);
            vm.CritDmg = shipData.GetFinalState(CombatStateType.CritDmg);
            vm.PhysicsDmg = shipData.GetFinalState(CombatStateType.PhysicsDmg);
            vm.OpticsDmg = shipData.GetFinalState(CombatStateType.OpticsDmg);
            vm.ParticleDmg = shipData.GetFinalState(CombatStateType.ParticleDmg);
            vm.PlasmaDmg = shipData.GetFinalState(CombatStateType.PlasmaDmg);
            vm.MaxSlotCount = shipData.GetMaxSlot();
            vm.UseSlotCount = shipData.GetUseSlot();
            vm.EquipedCombatKeyList = shipData.GetEquipedCombatItemList();
            vm.EquipedEngineKey = shipData.GetEquipedEngine();
            vm.EquipedReactorKey = shipData.GetEquipedReactor();
            vm.EquipedRadiatorKey = shipData.GetEquipedRadiator();
        }

        //������ ���� Ŀ�ǵ�, �ݹ�===================================================================
        public static void Register_OnLevelUpInfoCallBack(this ShipMenuUIViewModel vm)
        {
            ShipLevelUpLogicManager.Instance.Register_OnLevelUpInfoCallBack(vm.OnChange_LevelUpInfo);
        }
        public static void UnRegister_OnLevelUpInfoCallBack(this ShipMenuUIViewModel vm)
        {
            ShipLevelUpLogicManager.Instance.UnRegister_OnLevelUpInfoCallBack(vm.OnChange_LevelUpInfo);
        }


        public static void Command_SetShipData_LevelUpInfo(this ShipMenuUIViewModel vm, int shipKey)
        {
            ShipLevelUpLogicManager.Instance.SetShipData(shipKey);
        }
        public static void Command_ChangeLevelUpCount(this ShipMenuUIViewModel vm, int changedLevelUpCount)
        {
            ShipLevelUpLogicManager.Instance.ChangeLevelUpCount(changedLevelUpCount);
        }
        public static bool Command_LevelUp(this ShipMenuUIViewModel vm)
        {
            return ShipLevelUpLogicManager.Instance.LevelUp();
        }

        public static void OnChange_LevelUpInfo(this ShipMenuUIViewModel vm, int levelUpCount, int shipLevel, int needCredit)
        {
            vm.Level = shipLevel;
            vm.LevelUpCount = levelUpCount;
            vm.NeedCreditLevelUp = needCredit;
        }
    }
}
