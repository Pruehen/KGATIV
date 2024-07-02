using System;

public class NavMissionLogicManager
{
    static NavMissionLogicManager _instance = null;
    public static NavMissionLogicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NavMissionLogicManager();
                _instance._prmStage = UserData.Instance.CurPrmStage;
                _instance._secStage = UserData.Instance.CurSecStage;
                _instance._defeatedBossStage = false;
            }
            return _instance;
        }
    }

    /// <summary>
    /// int prmStage, int secStage, bool canGiveUp, bool canRetry
    /// </summary>
    Action<int, int, bool, bool> _onStageChange;

    int _prmStage;
    int _secStage;
    bool _defeatedBossStage;

    public void Register_OnStageChange(Action<int, int, bool, bool> callBack)
    {
        _onStageChange += callBack;
    }
    public void UnRegister_OnStageChange(Action<int, int, bool, bool> callBack)
    {
        _onStageChange -= callBack;
    }
    public void RefreshViewModel()
    {
        bool canGiveUp = (_secStage == 10);
        _onStageChange?.Invoke(_prmStage, _secStage, canGiveUp, _defeatedBossStage);
    }

    public void StageUp(out int preStage, out int secStage)
    {
        if (_defeatedBossStage == false)
        {
            _secStage++;

            if (_secStage >= 11)
            {
                _secStage = 1;
                _prmStage++;
            }

            preStage = _prmStage;
            secStage = _secStage;
            bool canGiveUp = (secStage == 10);            

            _onStageChange.Invoke(_prmStage, _secStage, canGiveUp, _defeatedBossStage);

            UserData.Instance.CurPrmStage = _prmStage;
            UserData.Instance.CurSecStage = _secStage;
            UserData.Save();
        }
        else
        {
            preStage = _prmStage;
            secStage = _secStage;
        }
    }
    public void Retry()
    {
        _defeatedBossStage = false;
        _onStageChange.Invoke(_prmStage, _secStage, false, _defeatedBossStage);
    }

    public void StageDown_OnBossStageDefeat()
    {
        _secStage = 9;
        _defeatedBossStage = true;
        _onStageChange.Invoke(_prmStage, _secStage, false, _defeatedBossStage);

        UserData.Instance.CurPrmStage = _prmStage;
        UserData.Instance.CurSecStage = _secStage;
        UserData.Save();
    }

    public float GetValue_StageState()
    {
        float value = MathF.Pow(_prmStage + _secStage * 0.05f, 1.5f);
        if (_secStage == 10) value *= 3;        
        return value;
    }
}
