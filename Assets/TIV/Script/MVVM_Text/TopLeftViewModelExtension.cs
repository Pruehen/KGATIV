namespace ViewModel.Extensions
{
    public static class TopLeftViewModelExtension
    {
        public static void RefreshVielModel(this TopLeftViewModel vm)//��û �ͽ��ټ�
        {
            int tempId = 2;
            GameLogicManager.Inst.RefreshCharacterInfo(tempId, vm.OnRefreshViewModel);//�ݹ� ȣ��
        }

        public static void OnRefreshViewModel(this TopLeftViewModel vm, int userId, string name, int level)//�ݹ�
        {
            vm.UserId = userId;
            vm.Name = name;
            vm.Level = level;
        }

        //public static void BindRegisterEvents(this TopLeftViewModel vm, bool isRegistring)
        //{
        //    if(isRegistring)
        //    {

        //    }
        //    else
        //    {

        //    }
        //}

        public static void RegisterEventsOnEnable(this TopLeftViewModel vm)
        {
            GameLogicManager.Inst.RegisterLevelUpCallback(vm.OnResponseLevelUp);
        }
        public static void UnRegisterEventsOnDisable(this TopLeftViewModel vm)
        {
            GameLogicManager.Inst.UnRegisterLevelUpCallback(vm.OnResponseLevelUp);
        }
        public static void OnResponseLevelUp(this TopLeftViewModel vm, int userId, int level)
        {
            if (vm.UserId != userId) return;

            vm.Level = level;
        }
    }
}
