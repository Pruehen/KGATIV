using EnumTypes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;

public class EquipInfoUIManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;

    [Header("장비 뷰 필드")]
    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Type;
    [SerializeField] TextMeshProUGUI TMP_MainStateType;
    [SerializeField] TextMeshProUGUI TMP_MainStateValue;
    [SerializeField] TextMeshProUGUI TMP_Level;
    [SerializeField] TextMeshProUGUI TMP_StateField;
    [SerializeField] TextMeshProUGUI TMP_EffectField;
    [SerializeField] EquipIcon EquipIcon_ViewIcon;

    [Header("업그레이드 UI 필드")]    
    [SerializeField] GameObject Label_MaxLevel;
    [SerializeField] TextMeshProUGUI TMP_CreditLevelUpNeed;
    [SerializeField] TextMeshProUGUI TMP_LevelUpCount;
    [SerializeField] Button Btn_Upgrade;
    [SerializeField] Button Btn_p1;
    [SerializeField] Button Btn_p10;
    [SerializeField] Button Btn_m1;
    [SerializeField] Button Btn_m10;


    EquipInfoUIManagerViewModel _vm;
    string _uniqueKey;
    public string UniqueKey
    {
        get { return _uniqueKey; }
        set { _uniqueKey = value; }
    }

    private void Awake()
    {
        Btn_Upgrade.onClick.AddListener(TryUpgrade);
        Btn_p1.onClick.AddListener(() => Command_ChangeLevelUpCount(1));
        Btn_p10.onClick.AddListener(() => Command_ChangeLevelUpCount(10));
        Btn_m1.onClick.AddListener(() => Command_ChangeLevelUpCount(-1));
        Btn_m10.onClick.AddListener(() => Command_ChangeLevelUpCount(-10));

        _vm = new EquipInfoUIManagerViewModel();
        _vm.PropertyChanged += OnPropertyChanged;
        _vm.Register_OnUpgradeInfoCallBack();
    }

    //private void OnEnable()
    //{
    //    if (_vm == null)
    //    {
    //        _vm = new EquipInfoUIManagerViewModel();
    //        _vm.PropertyChanged += OnPropertyChanged;
    //        _vm.Register_OnUpgradeInfoCallBack();
    //    }
    //}
    //private void OnDisable()
    //{
    //    if (_vm != null)
    //    {
    //        _vm.UnRegister_OnUpgradeInfoCallBack();
    //        _vm.PropertyChanged -= OnPropertyChanged;
    //        _vm = null;
    //    }
    //}

    public void ViewItemKey(string key)
    {
        _vm.RefreshViewModel(key);
    }

    void TryUpgrade()
    {
        UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(_uniqueKey);
        UserHaveEquipData equipDataTemp = new UserHaveEquipData(equipData);
        _vm.CommandUpgrade();

        this.UIManager.PopupWdw_UpgradeResult(2, equipDataTemp, equipData);
    }
    void Command_ChangeLevelUpCount(int value)
    {
        _vm.Command_ChangeLevelUpCount(value);
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
                if(_vm.Level >= UserHaveEquipData.MaxLevel())
                {
                    TMP_CreditLevelUpNeed.gameObject.SetActive(false);
                    Btn_Upgrade.gameObject.SetActive(false);                    
                    Btn_p1.gameObject.SetActive(false);
                    Btn_p10.gameObject.SetActive(false);
                    Btn_m1.gameObject.SetActive(false);
                    Btn_m10.gameObject.SetActive(false);

                    Label_MaxLevel.SetActive(true);
                }
                else
                {
                    TMP_CreditLevelUpNeed.gameObject.SetActive(true);
                    Btn_Upgrade.gameObject.SetActive(true);
                    Btn_p1.gameObject.SetActive(true);
                    Btn_p10.gameObject.SetActive(true);
                    Btn_m1.gameObject.SetActive(true);
                    Btn_m10.gameObject.SetActive(true);

                    Label_MaxLevel.SetActive(false);
                }
                break;
            case nameof(_vm.LevelUpCount):
                TMP_LevelUpCount.text = $"강화 +{_vm.LevelUpCount}";
                break;
            case nameof(_vm.NeedCreditLevelUp):
                if (JsonDataManager.DataLode_UserData().Credit >= _vm.NeedCreditLevelUp)
                {
                    TMP_CreditLevelUpNeed.color = Color.white;
                }
                else
                {
                    TMP_CreditLevelUpNeed.color = Color.red;
                }
                TMP_CreditLevelUpNeed.text = $"{_vm.NeedCreditLevelUp}";
                break;
            case nameof(_vm.SubStateList):
                SetSubStateText(_vm.SubStateList);
                break;
            case nameof(_vm.SetType):
                Set_EffectText(_vm.SetType, _vm.TableKey);
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
    void Set_EffectText(SetType setType, int tableKey)
    {
        EquipTable table = JsonDataManager.DataLode_EquipTable(tableKey);
        if (setType >= 0)
        {
            EquipSetTable setTable = JsonDataManager.DataLode_SetEffectTable(setType);

            BuffTable set1Table = JsonDataManager.DataLode_BuffTable(setTable._set1Key);
            BuffTable set2Table = JsonDataManager.DataLode_BuffTable(setTable._set2Key);
            BuffTable set4Table = JsonDataManager.DataLode_BuffTable(setTable._set4Key);

            string set1Text = string.Format(set1Table._buffInfo, set1Table._buffValueList.Cast<object>().ToArray());
            string set2Text = string.Format(set2Table._buffInfo, set2Table._buffValueList.Cast<object>().ToArray());
            string set4Text = string.Format(set4Table._buffInfo, set4Table._buffValueList.Cast<object>().ToArray());

            TMP_EffectField.text = $"{table._info}\n\n{setTable._setEffectName}\n    1세트 효과: {set1Text}\n    2세트 효과: {set2Text}\n    4세트 효과: {set4Text}";
        }
        else
        {
            WeaponSkillTable skillTable = JsonDataManager.DataLode_WeaponSkillTableList(tableKey);
            string skillText = string.Format(skillTable._info, skillTable._dmg, skillTable._maxRange, skillTable._collTime, skillTable._halfDistance);
            TMP_EffectField.text = $"{table._info}\n\n{skillText}";
        }
    }
}
