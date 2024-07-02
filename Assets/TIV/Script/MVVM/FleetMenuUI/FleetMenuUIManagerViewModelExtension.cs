using kjh;

namespace ViewModel.Extensions
{
    public static class FleetMenuUIManagerViewModelExtension
    {
        public static void OnAllRefreshViewModel(this FleetMenuUIManagerViewModel vm)//ฤÝน้
        {
            vm.MaxFleetCost = UserData.Instance.FleetCost;
            vm.UsingFleetCost = PlayerSpawner.Instance.GetTotalCoat();
        }
    }
}
