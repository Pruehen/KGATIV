namespace ViewModel.Extensions
{
    public static class NavMissionUIManagerViewModelExtension
    {
        public static void Register_OnStageChange(this NavMissionUIManagerViewModel vm)
        {
            NavMissionLogicManager.Instance.Register_OnStageChange(vm.OnRefreshViewModel);
        }
        public static void UnRegister_OnStageChange(this NavMissionUIManagerViewModel vm)
        {
            NavMissionLogicManager.Instance.UnRegister_OnStageChange(vm.OnRefreshViewModel);
        }
        public static void Command_GiveUp(this NavMissionUIManagerViewModel vm)
        {
            NavMissionLogicManager.Instance.StageDown_OnBossStageDefeat();
        }
        public static void Command_Retry(this NavMissionUIManagerViewModel vm)
        {
            NavMissionLogicManager.Instance.Retry();
        }
        public static void Command_Refresh(this NavMissionUIManagerViewModel vm)
        {
            NavMissionLogicManager.Instance.RefreshViewModel();
        }

        public static void OnRefreshViewModel(this NavMissionUIManagerViewModel vm, int prmStage, int secStage, bool canGiveUp, bool canRetry)//ฤÝน้
        {
            vm.CurPrmStage = prmStage;
            vm.CurSecStage = secStage;
            vm.CanGiveUp = canGiveUp;
            vm.CanRetry = canRetry;
        }
    }
}
