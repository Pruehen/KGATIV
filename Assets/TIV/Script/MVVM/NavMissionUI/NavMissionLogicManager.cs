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
                _instance._inNavMissionDefaultMode = true;

                EnemySpawner.Instance.Register_onActiveEnemyCountChanged(_instance.OnActiveEnemyCountChanged);
            }
            return _instance;
        }
    }

    /// <summary>
    /// int prmStage, int secStage, bool canGiveUp, bool canRetry
    /// </summary>
    Action<int, int, bool, bool> _onStageChange;

    bool _inNavMissionDefaultMode;

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

    void OnActiveEnemyCountChanged(int count) 
    {
        if(_inNavMissionDefaultMode && count <= 0)
        {
            StageUp(out int prmStage, out int secStage);
            EnemySpawner.Instance.Spawn_NavStage(prmStage, secStage, 2);
        }
    }

    public void StageUp(out int prmStage, out int secStage)
    {
        if (_defeatedBossStage == false)
        {
            _secStage++;

            if (_secStage >= 11)
            {
                _secStage = 1;
                _prmStage++;
            }

            prmStage = _prmStage;
            secStage = _secStage;
            bool canGiveUp = (secStage == 10);            

            _onStageChange.Invoke(_prmStage, _secStage, canGiveUp, _defeatedBossStage);

            UserData.Instance.CurPrmStage = _prmStage;
            UserData.Instance.CurSecStage = _secStage;
            UserData.Save();
        }
        else
        {
            prmStage = _prmStage;
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

        _inNavMissionDefaultMode = false;
        kjh.GameLogicManager.Instance.AllActiveShipRemove();
        EnemySpawner.Instance.Spawn_NavStage(_prmStage, _secStage, 10);
        _inNavMissionDefaultMode = true;

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
