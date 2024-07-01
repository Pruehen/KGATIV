namespace ViewModel.Extensions
{
    public static class ShipControllerViewModelExtension
    {
        public static void Register_shipListChangeCallBack(this ShipControllerViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_shipListChangeCallBack(vm.OnAddedNewUI);
        }

        public static void UnRegister_shipListChangeCallBack(this ShipControllerViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_shipListChangeCallBack(vm.OnAddedNewUI);
        }

       
        public static void OnAddedNewUI(this ShipControllerViewModel vm, ShipMaster shipMaster, bool isAdd)//ฤÝน้
        {            
            vm.ChangedShipMaster = shipMaster;            
        }        
    }
}
