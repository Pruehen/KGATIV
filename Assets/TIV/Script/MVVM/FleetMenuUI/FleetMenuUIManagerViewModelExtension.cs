using kjh;
using System.Collections.Generic;

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
        public static void Register_onActiveShipChanged(this FleetMenuUIManagerViewModel vm)
        {
            PlayerSpawner.Instance._onActiveShipChanged += (vm.OnRefreshViewModel_CombatPower);
        }
        public static void UnRegister_onActiveShipChanged(this FleetMenuUIManagerViewModel vm)
        {
            PlayerSpawner.Instance._onActiveShipChanged -= (vm.OnRefreshViewModel_CombatPower);
        }

        public static void RefreshViewModel(this FleetMenuUIManagerViewModel vm)
        {
            FleetLogicManager.Instance.OnFleetCostChange();
        }
        public static void Command_TryUpgradeFleetCost(this FleetMenuUIManagerViewModel vm)
        {
            FleetLogicManager.Instance.TryUpgradeFleetCost();
        }

        public static void OnRefreshViewModel(this FleetMenuUIManagerViewModel vm, int maxCost, int usingCost, long upgradeCost)//콜백
        {
            vm.MaxFleetCost = maxCost;
            vm.UsingFleetCost = usingCost;
            vm.UpgradeCost = upgradeCost;
        }

        public static void OnRefreshViewModel_CombatPower(this FleetMenuUIManagerViewModel vm, Dictionary<int, ShipMaster> activeShipDic)//콜백
        {
            float totalCombatPower = 0;
            foreach (var item in activeShipDic)
            {
                ShipMaster shipMaster = item.Value;
                totalCombatPower += Calcf.CombatPower(shipMaster);
            }        

            vm.FleetCombatPower = totalCombatPower;
        }
    }
}
