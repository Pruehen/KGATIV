using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;

public class EquipInfoUIManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;

    [Header("Àåºñ ºä ÇÊµå")]
    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Type;
    [SerializeField] TextMeshProUGUI TMP_MainStateType;
    [SerializeField] TextMeshProUGUI TMP_MainStateValue;
    [SerializeField] TextMeshProUGUI TMP_Level;
    [SerializeField] TextMeshProUGUI TMP_StateField;
    [SerializeField] TextMeshProUGUI TMP_SetEffectField;
    [SerializeField] EquipIcon EquipIcon_ViewIcon;
    [SerializeField] Button Btn_Upgrade;
    [SerializeField] GameObject Label_MaxLevel;

    EquipInfoUIManagerViewModel _vm;
    string _uniqueKey;
    public string UniqueKey
    {
        get { return _uniqueKey; }
        set { _uniqueKey = value; }
    }

    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new EquipInfoUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            Btn_Upgrade.onClick.AddListener(TryUpgrade);
            //_vm.RegisterEventsOnEnable();            
        }
    }
    private void OnDisable()
    {
        if (_vm != null)
        {
            //_vm.UnRegisterEventsOnDisable();
            Btn_Upgrade.onClick.RemoveListener(TryUpgrade);
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    public void ViewItemKey(string key)
    {
        _vm.RefreshViewModel(key);
    }

    void TryUpgrade()
    {
        UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(_uniqueKey);
        UserHaveEquipData equipDataTemp = new UserHaveEquipData(equipData);
        _vm.CommandUpgrade(_uniqueKey);

        this.UIManager.PopupWdw_UpgradeResult(3, equipDataTemp, equipData);
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.UniqueKey):
                _uniqueKey = _vm.UniqueKey;
                break;
            case nameof(_vm.TableKey):
                EquipIcon_ViewIcon.SetSprite(_vm.TableKey);                
                break;
            case nameof(_vm.Name):
                TMP_Name.text = _vm.Name;
                break;
            case nameof(_vm.Type):
                TMP_Type.text = EquipTable.GetEquipType(_vm.Type);
                break;
            case nameof(_vm.MainStateType):
                TMP_MainStateType.text = StateType_StateMultipleTable.GetStateText(_vm.MainStateType);
                break;
            case nameof(_vm.Level):
                TMP_MainStateValue.text = $"+{StateType_StateMultipleTable.GetStateText(_vm.MainStateType, _vm.Level + 5)[1]}";
                TMP_Level.text = $"+{_vm.Level}";
                if(_vm.Level >= 20)
                {
                    Btn_Upgrade.gameObject.SetActive(false);
                    Label_MaxLevel.SetActive(true);
                }
                else
                {
                    Btn_Upgrade.gameObject.SetActive(true);
                    Label_MaxLevel.SetActive(false);
                }
                break;
            case nameof(_vm.SubStateList):
                SetSubStateText(_vm.SubStateList);
                break;
        }
    }

    void SetSubStateText(List<UserHaveEquipData.EquipStateSet> equipStateSets)
    {
        string text = string.Empty;
        foreach (var item in equipStateSets)
        {
            string[] temp = StateType_StateMultipleTable.GetStateText(item._stateType, item._level);
            text += $" - {temp[0]} +{temp[1]}\n";
        }
        TMP_StateField.text = text;
    }
}
