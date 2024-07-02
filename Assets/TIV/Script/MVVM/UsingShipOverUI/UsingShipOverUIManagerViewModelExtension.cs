using EnumTypes;

namespace ViewModel.Extensions
{
    public static class UsingShipOverUIManagerViewModelExtension
    {
        public static void Register_shipListChangeCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_shipListChangeCallBack(vm.OnAddedNewUI);
        }

        public static void UnRegister_shipListChangeCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_shipListChangeCallBack(vm.OnAddedNewUI);
        }

        public static void Register_OnSelectShipCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_OnSelectShipCallBack(vm.OnRefreshViewModel_ShipStateData);
        }

        public static void UnRegister_OnSelectShipCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_OnSelectShipCallBack(vm.OnRefreshViewModel_ShipStateData);
        }
        public static void OnAddedNewUI(this UsingShipOverUIManagerViewModel vm, ShipMaster shipMaster, bool isAdd)//콜백
        {
            vm.IsAdded = isAdd;
            vm.ChangedShipMaster = shipMaster;            
        }
        public static void OnRefreshViewModel_ShipStateData(this UsingShipOverUIManagerViewModel vm, ShipMaster shipMaster)//콜백
        {
            if(shipMaster == null)
            {
                vm.IsActiveInfoView = false;
            }
            else
            {
                vm.IsActiveInfoView = true;

                ShipCombatData combatData = shipMaster.CombatData;
                ShipData shipData = combatData.ShipData;
                ShipBuffManager buffManager = combatData.BuffManager;
                vm.BuffManagerRef = buffManager;

                vm.Hp = combatData.CurHp;
                vm.MaxHp = shipData.GetFinalState(CombatStateType.Hp, buffManager);
                vm.Atk = shipData.GetFinalState(CombatStateType.Atk, buffManager);
                vm.Def = shipData.GetFinalState(CombatStateType.Def, buffManager);
                vm.CritRate = shipData.GetFinalState(CombatStateType.CritRate, buffManager);
                vm.CritDmg = shipData.GetFinalState(CombatStateType.CritDmg, buffManager);
                vm.PhysicsDmg = shipData.GetFinalState(CombatStateType.PhysicsDmg, buffManager);
                vm.OpticsDmg = shipData.GetFinalState(CombatStateType.OpticsDmg, buffManager);
                vm.ParticleDmg = shipData.GetFinalState(CombatStateType.ParticleDmg, buffManager);
                vm.PlasmaDmg = shipData.GetFinalState(CombatStateType.PlasmaDmg, buffManager);
            }
        }
    }
}
