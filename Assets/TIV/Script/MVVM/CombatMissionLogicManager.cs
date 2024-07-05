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
        _deadEnemyCount++;
        if (count <= 0)
        {
            //스테이지 클리어 처리 
            _onStageClear?.Invoke();

            UIManager.Instance.PopUpWdw_MissionResultPopupUI(ExitCombatMission, _collectedCredit, _collectedEquipKeyList);
        }
    }

    int _collectedCredit;
    int _deadEnemyCount;
    List<string> _collectedEquipKeyList;

    void EnterCombatMission(Action missionSpawnCallBack)
    {
        UIManager.Instance.PopUpWdw_VirtualLoding(5);

        NavMissionLogicManager.Instance.SetIsEnemyRespawn_OnStageChanged(false);
        kjh.GameLogicManager.Instance.AllActiveShipRemove();
        missionSpawnCallBack.Invoke();

        EnemySpawner.Instance.Register_onActiveEnemyCountChanged(OnActiveEnemyCountChanged);
        UserData.Instance.Register_OnCreditChange(OnCreditChange);
        _collectedCredit = 0;
        _deadEnemyCount = 0;

        UIManager.Instance.SetActiveWdw_NavMissionUI(false);        
        UserData.Save();
    }
    void ExitCombatMission()
    {
        UIManager.Instance.SetActiveWdw_NavMissionUI(true);

        UserData.Instance.UnRegister_OnCreditChange(OnCreditChange);
        EnemySpawner.Instance.UnRegister_onActiveEnemyCountChanged(OnActiveEnemyCountChanged);
        
        NavMissionLogicManager.Instance.SetIsEnemyRespawn_OnStageChanged(true);
        kjh.GameLogicManager.Instance.AllActiveShipRemove();
        EnemySpawner.Instance.Invoke_onActiveEnemyCountChanged();

        UIManager.Instance.PopUpWdw_VirtualLoding(5);
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
        _onStageClear += () => AddEquipItem(SetType.Alpha, _deadEnemyCount);
    }
    public void OnClick_Beta()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Beta);
        _onStageClear += () => AddEquipItem(SetType.Beta, _deadEnemyCount);
    }
    public void OnClick_Gamma()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Gamma);
        _onStageClear += () => AddEquipItem(SetType.Gamma, _deadEnemyCount);
    }
    public void OnClick_Delta()
    {
        EnterCombatMission(EnemySpawner.Instance.Spawn_Delta);
        _onStageClear += () => AddEquipItem(SetType.Delta, _deadEnemyCount);
    }

    void AddEquipItem(SetType setType, int count)
    {
        List<string> addedEquipKeyList = EquipManager.RandomEquipDrop(setType, count);
        _onStageClear = null;

        if (_collectedEquipKeyList == null) _collectedEquipKeyList = new List<string>();

        foreach (string key in addedEquipKeyList)
        {
            _collectedEquipKeyList.Add(key);
        }
    }
}
