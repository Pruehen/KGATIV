using EnumTypes;

namespace ViewModel.Extensions
{
    public static class ShipMenuUIViewModelExtension
    {
        public static void RefreshVielModel(this ShipMenuUIViewModel vm, int shipKey)//요청 익스텐션
        {            
            kjh.GameLogicManager.Instance.RefreshShipInfo(shipKey, vm.OnRefreshViewModel);//콜백 호출
        }

        public static void OnRefreshViewModel(this ShipMenuUIViewModel vm, ShipData shipData)//콜백
        {
            vm.Name = shipData.GetName();
            vm.Class = shipData.GetShipClass();
            vm.Star = shipData.GetShipStar();
            vm.Hp = shipData.GetFinalState(CombatStateType.Hp);
            vm.Atk = shipData.GetFinalState(CombatStateType.Atk);
            vm.Def = shipData.GetFinalState(CombatStateType.Def);
            vm.CritRate = shipData.GetFinalState(CombatStateType.CritRate);
            vm.CritDmg = shipData.GetFinalState(CombatStateType.CritDmg);
            vm.PhysicsDmg = shipData.GetFinalState(CombatStateType.PhysicsDmg);
            vm.OpticsDmg = shipData.GetFinalState(CombatStateType.OpticsDmg);
            vm.ParticleDmg = shipData.GetFinalState(CombatStateType.ParticleDmg);
            vm.PlasmaDmg = shipData.GetFinalState(CombatStateType.PlasmaDmg);
            vm.SlotCount = shipData.GetMaxSlot();
        }


        //public static void RegisterEventsOnEnable(this ShipMenuUIViewModel vm)
        //{
        //    ShipMenuUIManager.Instance.RegisterLevelUpCallback(vm.OnResponseLevelUp);
        //}
        //public static void UnRegisterEventsOnDisable(this ShipMenuUIViewModel vm)
        //{
        //    GameLogicManager.Inst.UnRegisterLevelUpCallback(vm.OnResponseLevelUp);
        //}
        //public static void OnResponseLevelUp(this ShipMenuUIViewModel vm, int userId, int level)
        //{
        //    if (vm.UserId != userId) return;

        //    vm.Level = level;
        //}
    }
}
