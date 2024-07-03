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

        public static void Register_OnRefuelRemaningChange(this UserDataViewModel vm)
        {
            UserData.Instance.Register_OnRefuelRemaningChange(vm.OnRefuelRemaningChange);
        }

        public static void UnRegister_OnRefuelRemaningChange(this UserDataViewModel vm)
        {
            UserData.Instance.UnRegister_OnRefuelRemaningChange(vm.OnRefuelRemaningChange);
        }

        public static void RefreshViewModel_OnInit(this UserDataViewModel vm)
        {
            UserData.Instance.RefreshViewModel();
        }

        public static void OnRefreshViewModel(this UserDataViewModel vm, UserData data)//콜백
        {
            vm.Credit = data.Credit;
            vm.SuperCredit = data.SuperCredit;
            vm.Fuel = data.Fuel;
            vm.CurPrmStage = data.CurPrmStage;
            vm.CurSecStage = data.CurSecStage;
        }
        public static void OnRefuelRemaningChange(this UserDataViewModel vm, int reFuelRemaning)//콜백
        {
            vm.ReFuelRemaning = reFuelRemaning;
        }
    }
}
