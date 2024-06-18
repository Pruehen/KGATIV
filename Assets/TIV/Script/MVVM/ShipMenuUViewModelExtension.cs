namespace ViewModel.Extensions
{
    public static class ShipMenuUIViewModelExtension
    {
        public static void RefreshVielModel(this ShipMenuUIViewModel vm, int shipKey)//요청 익스텐션
        {            
            kjh.GameLogicManager.Instance.RefreshShipInfo(shipKey, vm.OnRefreshViewModel);//콜백 호출
        }

        public static void OnRefreshViewModel(this ShipMenuUIViewModel vm, string[] data)//콜백
        {
            vm.Name = data[0];
            vm.Class = data[1];
            vm.Star = data[2];
            vm.Hp = data[3];
            vm.Atk = data[4];
            vm.Def = data[5];
            vm.CritRate = data[6];
            vm.CritDmg = data[7];
            vm.PhysicsDmg = data[8];
            vm.OpticsDmg = data[9];
            vm.ParticleDmg = data[10];
            vm.PlasmaDmg = data[11];
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
