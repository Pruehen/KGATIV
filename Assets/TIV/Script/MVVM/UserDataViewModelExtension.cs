namespace ViewModel.Extensions
{
    public static class UserDataViewModelExtension
    {
        public static void Register_OnRefreshViewModel(this UserDataViewModel vm)
        {
            UserData.Instance.Register_OnRefreshViewModel(vm.OnRefreshViewModel);
        }

        public static void UnRegister_OnRefreshViewModel(this UserDataViewModel vm)
        {
            UserData.Instance.UnRegister_OnRefreshViewModel(vm.OnRefreshViewModel);
        }
        public static void RefreshViewModel_OnInit(this UserDataViewModel vm)
        {
            UserData.Instance.RefreshViewModel();
        }

        public static void OnRefreshViewModel(this UserDataViewModel vm, UserData data)//ฤÝน้
        {
            vm.Credit = data.Credit;
            vm.SuperCredit = data.SuperCredit;
            vm.Fuel = data.Fuel;
            vm.CurPrmStage = data.CurPrmStage;
            vm.CurSecStage = data.CurSecStage;
        }
    }
}
