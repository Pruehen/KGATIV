using System.ComponentModel;
using TMPro;
using UI.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModel.Extensions;

public class FleetMenuUIManager : MonoBehaviour
{
    [SerializeField] ShipController _shipController;

    [SerializeField] TextMeshProUGUI Label_SpawnMode;
    [SerializeField] TextMeshProUGUI Text_FleetCost;
    [SerializeField] EventTrigger EventTrigger_ShipSpawn_4F1;
    [SerializeField] EventTrigger EventTrigger_ShipSpawn_4D1;
    [SerializeField] EventTrigger EventTrigger_ShipSpawn_4C1;
    [SerializeField] EventTrigger EventTrigger_ShipSpawn_4B1;
    [SerializeField] EventTrigger EventTrigger_ShipSpawn_5T1;

    [Header("코스트 업그레이드 관련")]
    [SerializeField] TextMeshProUGUI Text_FleetCostUpgradeCost;
    [SerializeField] Button Btn_Upgrade;

    FleetMenuUIManagerViewModel _vm;

    private void Awake()
    {
        AddEventTrigger(EventTrigger_ShipSpawn_4F1, EventTriggerType.PointerDown, () => EnterCreateMode_OnEventTriggerPointerDown(0));
        AddEventTrigger(EventTrigger_ShipSpawn_4D1, EventTriggerType.PointerDown, () => EnterCreateMode_OnEventTriggerPointerDown(1));
        AddEventTrigger(EventTrigger_ShipSpawn_4C1, EventTriggerType.PointerDown, () => EnterCreateMode_OnEventTriggerPointerDown(2));
        AddEventTrigger(EventTrigger_ShipSpawn_4B1, EventTriggerType.PointerDown, () => EnterCreateMode_OnEventTriggerPointerDown(3));
        AddEventTrigger(EventTrigger_ShipSpawn_5T1, EventTriggerType.PointerDown, () => EnterCreateMode_OnEventTriggerPointerDown(4));

        Label_SpawnMode.gameObject.SetActive(false);
    }    

    void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, UnityEngine.Events.UnityAction action)
    {
        var entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action()); // AddListener takes a UnityAction<BaseEventData>
        trigger.triggers.Add(entry);
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.MaxFleetCost):
                Text_FleetCost.text = $"{_vm.UsingFleetCost} / {_vm.MaxFleetCost}";
                break;
            case nameof(_vm.UsingFleetCost):
                Text_FleetCost.text = $"{_vm.UsingFleetCost} / {_vm.MaxFleetCost}";
                break;
            case nameof(_vm.UpgradeCost):
                Text_FleetCostUpgradeCost.SetTmpCostText(_vm.UpgradeCost);
                break;
        }
    }

    private void OnEnable()
    {
        if(_vm == null)
        {
            _vm = new FleetMenuUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_onFleetCostChange();
            _vm.RefreshViewModel();
            Btn_Upgrade.onClick.AddListener(_vm.Command_TryUpgradeFleetCost);
        }

        UIManager.Instance.SetActiveWdw_UsingShipOverUIManager(false);        
    }
    private void OnDisable()
    {        
        if(_vm != null)
        {
            Btn_Upgrade.onClick.RemoveAllListeners();
            _vm.UnRegister_onFleetCostChange();
            _vm.PropertyChanged -= OnPropertyChanged;            
            _vm = null;
        }
        UIManager.Instance.SetActiveWdw_UsingShipOverUIManager(true);        
    }


    void EnterCreateMode_OnEventTriggerPointerDown(int shipKey)
    {
        Label_SpawnMode.gameObject.SetActive(true);
        _shipController.SelectTargetObject_OnEventTriggerPointerDown(shipKey);        
    }

    public void ExitCreateMode_OnPointerUp()
    {             
        Label_SpawnMode.gameObject.SetActive(false);
    }
}
