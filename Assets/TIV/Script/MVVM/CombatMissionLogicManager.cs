using EnumTypes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatMissionLogicManager
{
    static CombatMissionLogicManager _instance = null;
    public static CombatMissionLogicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CombatMissionLogicManager();
            }
            return _instance;
        }
    }
    Action _onStageClear;
    void OnActiveEnemyCountChanged(int count)
    {
        if(count <= 0)
        {
            //스테이지 클리어 처리 
            _onStageClear?.Invoke();
            ExitCombatMission();
        }
    }

    int _collectedCredit;
    void EnterCombatMission(Action missionSpawnCallBack)
    {
        NavMissionLogicManager.Instance.SetIsEnemyRespawn_OnStageChanged(false);
        kjh.GameLogicManager.Instance.AllActiveShipRemove();
        missionSpawnCallBack.Invoke();

        EnemySpawner.Instance.Register_onActiveEnemyCountChanged(OnActiveEnemyCountChanged);
        UserData.Instance.Register_OnCreditChange(OnCreditChange);
        _collectedCredit = 0;

        UIManager.Instance.SetActiveWdw_NavMissionUI(false);        
        UserData.Save();
    }
    void ExitCombatMission()
    {
        UIManager.Instance.SetActiveWdw_NavMissionUI(true);

        NavMissionLogicManager.Instance.SetIsEnemyRespawn_OnStageChanged(true);

        UserData.Instance.UnRegister_OnCreditChange(OnCreditChange);
        EnemySpawner.Instance.UnRegister_onActiveEnemyCountChanged(OnActiveEnemyCountChanged);
        EnemySpawner.Instance.Invoke_onActiveEnemyCountChanged();
    }
    void OnCreditChange(int changedCredit)
    {
        _collectedCredit += changedCredit;
    }



    public void OnClick_Request_Level1()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Request_1);        
    }
    public void OnClick_Request_Level2()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Request_2);
    }
    public void OnClick_Request_Level3()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Request_3);
    }
    public void OnClick_Request_Level4()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Request_4);
    }
    public void OnClick_Alpha()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Alpha);
        _onStageClear += () => AddEquipItem(SetType.Alpha, UnityEngine.Random.Range(3, 6));
    }
    public void OnClick_Beta()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Beta);
        _onStageClear += () => AddEquipItem(SetType.Beta, UnityEngine.Random.Range(3, 6));
    }
    public void OnClick_Gamma()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Gamma);
        _onStageClear += () => AddEquipItem(SetType.Gamma, UnityEngine.Random.Range(3, 6));
    }
    public void OnClick_Delta()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Delta);
        _onStageClear += () => AddEquipItem(SetType.Delta, UnityEngine.Random.Range(3, 6));
    }

    void AddEquipItem(SetType setType, int count)
    {
        List<string> addedEquipKeyList = EquipManager.RandomEquipDrop(setType, count);
        _onStageClear = null;

        foreach (string key in addedEquipKeyList)
        {
            Debug.Log($"{key} 획득");
        }
    }
}
