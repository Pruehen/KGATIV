using System;
using UnityEngine;

public class FleetLogicManager
{
    static FleetLogicManager _instance = null;
    public static FleetLogicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FleetLogicManager();
            }
            return _instance;
        }
    }

    Action<int, int, long> _onFleetCostChange;

    public void Register_onFleetCostChange(Action<int, int, long> callBack)
    {
        _onFleetCostChange += callBack;
    }
    public void UnRegister_onFleetCostChange(Action<int, int, long> callBack)
    {
        _onFleetCostChange -= callBack;
    }

    public void OnFleetCostChange()
    {
        _onFleetCostChange?.Invoke(UserData.Instance.FleetCost, PlayerSpawner.Instance.GetTotalCoat(), GetUpgradeNeedCredit());
    }

    public void TryUpgradeFleetCost()
    {
        long needCredit = GetUpgradeNeedCredit();

        if (UserData.Instance.TryUseCredit(needCredit))
        {
            UserData.Instance.UpgradeFleetCost();
            OnFleetCostChange();
        }
    }
    long GetUpgradeNeedCredit()
    {
        return Calcf.GetFleetUpgradeNeedCredit(UserData.Instance.FleetCost);
    }
}
