using System;
using System.Collections.Generic;

namespace kjh
{
    public class GameLogicManager
    {
        private static GameLogicManager _instance = null;        

        private static Dictionary<int, ShipData> _shipDatas = new Dictionary<int, ShipData>();
        private Action<int, int> _levelUpCallback;

        public static GameLogicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameLogicManager();
                    TempInitPlayerList();
                }
                return _instance;
            }
        }
        public static void TempInitPlayerList()
        {
            _shipDatas.Add(0, new ShipData(JsonDataManager.DataLode_UserHaveShipData(0)));
            _shipDatas.Add(1, new ShipData(JsonDataManager.DataLode_UserHaveShipData(1)));
            _shipDatas.Add(2, new ShipData(JsonDataManager.DataLode_UserHaveShipData(2)));
            _shipDatas.Add(3, new ShipData(JsonDataManager.DataLode_UserHaveShipData(3)));
            _shipDatas.Add(4, new ShipData(JsonDataManager.DataLode_UserHaveShipData(4)));
        }

        public void RegisterLevelUpCallback(Action<int, int> levelupCallback)
        {
            _levelUpCallback += levelupCallback;
        }

        public void UnRegisterLevelUpCallback(Action<int, int> levelupCallback)
        {
            _levelUpCallback -= levelupCallback;
        }

        public void RefreshShipInfo(int requestId, Action<string[], int> callback)
        {
            ShipData shipData = _shipDatas[requestId];
            callback.Invoke(shipData.GetAllFinalStateText(), shipData.GetMaxSlot());
        }
    }
}