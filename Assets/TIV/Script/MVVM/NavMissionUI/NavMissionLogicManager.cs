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
                _instance._isEnemyRespawn_OnStageChanged = true;

                EnemySpawner.Instance.Register_onActiveEnemyCountChanged(_instance.OnActiveEnemyCountChanged);
            }
            return _instance;
        }
    }

    /// <summary>
    /// int prmStage, int secStage, bool canGiveUp, bool canRetry
    /// </summary>
    Action<int, int, bool, bool> _onStageChange;

    bool _isEnemyRespawn_OnStageChanged;

    /// <summary>
    /// false일 경우, 모든 적이 죽어도 자동으로 적을 재생성하지 않음
    /// </summary>
    /// <param name="value"></param>
    public void SetIsEnemyRespawn_OnStageChanged(bool value)
    {
        _isEnemyRespawn_OnStageChanged = value;
    }

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
        if(_isEnemyRespawn_OnStageChanged && count <= 0)
        {
            TryStageUp(out int prmStage, out int secStage);
            EnemySpawner.Instance.Spawn_NavStage(prmStage, secStage, 2);
        }
    }

    public void TryStageUp(out int prmStage, out int secStage)
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

        SetIsEnemyRespawn_OnStageChanged(false);
        kjh.GameLogicManager.Instance.AllActiveShipRemove();
        SetIsEnemyRespawn_OnStageChanged(true);

        EnemySpawner.Instance.Spawn_NavStage(_prmStage, _secStage, 10);        

        UserData.Instance.CurPrmStage = _prmStage;
        UserData.Instance.CurSecStage = _secStage;
        UserData.Save();
    }

    public float GetValue_StageState()
    {
        float value = MathF.Pow(_prmStage + _secStage * 0.05f, 1.5f);     
        return value;
    }
}
