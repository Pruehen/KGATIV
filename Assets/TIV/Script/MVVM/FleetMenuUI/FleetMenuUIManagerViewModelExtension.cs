using kjh;

namespace ViewModel.Extensions
{
    public static class FleetMenuUIManagerViewModelExtension
    {
        public static void Register_onFleetCostChange(this FleetMenuUIManagerViewModel vm)
        {
            FleetLogicManager.Instance.Register_onFleetCostChange(vm.OnRefreshViewModel);
        }
        public static void UnRegister_onFleetCostChange(this FleetMenuUIManagerViewModel vm)
        {
            FleetLogicManager.Instance.UnRegister_onFleetCostChange(vm.OnRefreshViewModel);
        }
        public static void RefreshViewModel(this FleetMenuUIManagerViewModel vm)
        {
            FleetLogicManager.Instance.OnFleetCostChange();
        }
        public static void Command_TryUpgradeFleetCost(this FleetMenuUIManagerViewModel vm)
        {
            FleetLogicManager.Instance.TryUpgradeFleetCost();
        }

        public static void OnRefreshViewModel(this FleetMenuUIManagerViewModel vm, int maxCost, int usingCost, long upgradeCost)//ฤÝน้
        {
            vm.MaxFleetCost = maxCost;
            vm.UsingFleetCost = usingCost;
            vm.UpgradeCost = upgradeCost;
        }
    }
}
