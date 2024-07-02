using kjh;

namespace ViewModel.Extensions
{
    public static class FleetMenuUIManagerViewModelExtension
    {
        public static void OnAllRefreshViewModel(this FleetMenuUIManagerViewModel vm)//�ݹ�
        {
            vm.MaxFleetCost = UserData.Instance.FleetCost;
            vm.UsingFleetCost = PlayerSpawner.Instance.GetTotalCoat();
        }
    }
}
