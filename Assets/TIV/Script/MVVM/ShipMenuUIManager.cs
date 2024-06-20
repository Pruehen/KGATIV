using EnumTypes;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;

public class ShipMenuUIManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;

    [Header("상단 바")]
    [SerializeField] TextMeshProUGUI TMP_Credit;
    [SerializeField] TextMeshProUGUI TMP_SuperCredit;

    [Header("함선 선택 관리")]
    [SerializeField] Camera Camera_RotateCam;
    [SerializeField] Transform Transform_ShipDummyParent;
    [SerializeField] Button Btn_SelectShip_4F1;
    [SerializeField] Button Btn_SelectShip_4D1;
    [SerializeField] Button Btn_SelectShip_4C1;
    [SerializeField] Button Btn_SelectShip_4B1;
    [SerializeField] Button Btn_SelectShip_5T1;
    Transform _transform_CamViewTarget = null;
    int _selectedShip = -1;

    [Header("함선 메뉴 관리")]
    [SerializeField] Button Btn_Info;    
    [SerializeField] Button Btn_CombatSlot;
    [SerializeField] Button Btn_UtilSlot;
    [SerializeField] GameObject Wdw_Info;
    [SerializeField] GameObject Wdw_CombatSlot;
    [SerializeField] GameObject Wdw_UtilSlot;
    [SerializeField] GameObject Wdw_EquipList;

    [Header("함선 정보 필드")]
    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Class;
    [SerializeField] TextMeshProUGUI TMP_Star;
    [SerializeField] TextMeshProUGUI TMP_Hp;
    [SerializeField] TextMeshProUGUI TMP_Atk;
    [SerializeField] TextMeshProUGUI TMP_Def;
    [SerializeField] TextMeshProUGUI TMP_CritRate;
    [SerializeField] TextMeshProUGUI TMP_CritDmg;
    [SerializeField] TextMeshProUGUI TMP_PhysicsDmg;
    [SerializeField] TextMeshProUGUI TMP_OpticsDmg;
    [SerializeField] TextMeshProUGUI TMP_ParticleDmg;
    [SerializeField] TextMeshProUGUI TMP_PlasmaDmg;

    [Header("함선 장비 필드")]
    [SerializeField] Button Btn_EquipWeapon;
    [SerializeField] Button Btn_EquipArmor;
    [SerializeField] GameObject[] CombatEquipedSlotArray;
    List<EquipIcon> _equipedCombatItemIconList;

    [Header("함선 부품 필드")]
    [SerializeField] Button Btn_EquipEngine;
    [SerializeField] Button Btn_EquipReactor;
    [SerializeField] Button Btn_EquipRadiator;
    [SerializeField] EquipIcon Icon_Engine;
    [SerializeField] EquipIcon Icon_Reactor;
    [SerializeField] EquipIcon Icon_Radiator;

    [Header("장비 리스트 필드")]
    [SerializeField] RectTransform RectTransform_SCV_Content;
    [SerializeField] Button Btn_EquipSelected;
    [SerializeField] Button Btn_UnEquipSelected;
    List<EquipIcon> _equipIconList;
    int _activeIconCount;
    EquipType _viewEquipType;

    ShipMenuUIViewModel _vm;

    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new ShipMenuUIViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.RefreshUserItemData();
            //_vm.RegisterEventsOnEnable();            
        }
    }
    private void OnDisable()
    {
        if (_vm != null)
        {
            //_vm.UnRegisterEventsOnDisable();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.Name):
                TMP_Name.text = _vm.Name;
                break;
            case nameof(_vm.Class):
                TMP_Class.text = ShipTable.GetClass(_vm.Class);
                break;
            case nameof(_vm.Star):
                TMP_Star.text = ShipTable.GetStar(_vm.Star);
                break;
            case nameof(_vm.Hp):
                TMP_Hp.text = $"{_vm.Hp:F0}";
                break;
            case nameof(_vm.Atk):
                TMP_Atk.text = $"{_vm.Atk:F0}";
                break;
            case nameof(_vm.Def):
                TMP_Def.text = $"{_vm.Def:F0}";
                break;
            case nameof(_vm.CritRate):
                TMP_CritRate.text = $"{_vm.CritRate:F1}%";
                break;
            case nameof(_vm.CritDmg):
                TMP_CritDmg.text = $"{_vm.CritDmg:F1}%";
                break;
            case nameof(_vm.PhysicsDmg):
                TMP_PhysicsDmg.text = $"{_vm.PhysicsDmg:F1}%";
                break;
            case nameof(_vm.OpticsDmg):
                TMP_OpticsDmg.text = $"{_vm.OpticsDmg:F1}%";
                break;
            case nameof(_vm.ParticleDmg):
                TMP_ParticleDmg.text = $"{_vm.ParticleDmg:F1}%";
                break;
            case nameof(_vm.PlasmaDmg):
                TMP_PlasmaDmg.text = $"{_vm.PlasmaDmg:F1}%";
                break;
            case nameof(_vm.SlotCount):
                //SetActive_EquipSlotCount(_vm.SlotCount);
                break;
            case nameof(_vm.EquipedCombatKeyList):
                for (int i = 0; i < _equipedCombatItemIconList.Count; i++)//12번 순회.
                {
                    if(i < _vm.EquipedCombatKeyList.Count)
                    {
                        SetEquipIcon(_vm.EquipedCombatKeyList[i], _equipedCombatItemIconList[i]);
                    }
                    else
                    {
                        SetEquipIcon(null, _equipedCombatItemIconList[i]);
                    }
                }
                break;
            case nameof(_vm.EquipedEngineKey):
                SetEquipIcon(_vm.EquipedEngineKey, Icon_Engine);
                break;
            case nameof(_vm.EquipedReactorKey):
                SetEquipIcon(_vm.EquipedReactorKey, Icon_Reactor);
                break;
            case nameof(_vm.EquipedRadiatorKey):
                SetEquipIcon(_vm.EquipedRadiatorKey, Icon_Radiator);
                break;
            case nameof(_vm.Credit):
                TMP_Credit.text = $"{_vm.Credit}";
                break;
            case nameof(_vm.SuperCredit):
                TMP_SuperCredit.text = $"{_vm.SuperCredit}";
                break;
        }
    }
    void SetEquipIcon(string equipUniqueKey, EquipIcon icon)
    {
        if (equipUniqueKey == null || equipUniqueKey == string.Empty)
        {
            icon.RemoveAllListeners();
            icon.SetSprite(-1);            
        }
        else
        {            
            int equipTableKey = JsonDataManager.DataLode_UserHaveEquipData(equipUniqueKey)._equipTableKey;
            icon.SetSprite(equipTableKey);
            icon.AddListener(() => SetActiveEquipListWdw(true, JsonDataManager.DataLode_EquipTable(equipTableKey)._type));
            icon.AddListener(() => SetEquipInfoData_SelectedEquip(equipUniqueKey));
        }
    }
    private void Awake()
    {
        Btn_SelectShip_4F1.onClick.AddListener(() => SelectShip("4F1", 0));
        Btn_SelectShip_4D1.onClick.AddListener(() => SelectShip("4D1", 1));
        Btn_SelectShip_4C1.onClick.AddListener(() => SelectShip("4C1", 2));
        Btn_SelectShip_4B1.onClick.AddListener(() => SelectShip("4B1", 3));
        Btn_SelectShip_5T1.onClick.AddListener(() => SelectShip("5T1", 4));

        Btn_Info.onClick.AddListener(() => SelectWdw(Wdw_Info));
        Btn_CombatSlot.onClick.AddListener(() => SelectWdw(Wdw_CombatSlot));
        Btn_UtilSlot.onClick.AddListener(() => SelectWdw(Wdw_UtilSlot));

        Btn_EquipWeapon.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Weapon));
        Btn_EquipArmor.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Armor));
        _equipedCombatItemIconList = new List<EquipIcon>();
        foreach (GameObject item in CombatEquipedSlotArray)
        {
            _equipedCombatItemIconList.Add(item.transform.GetChild(0).GetComponent<EquipIcon>());
        }

        Btn_EquipRadiator.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Radiator));
        Btn_EquipRadiator.onClick.AddListener(() => SetEquipInfoData_SelectedEquip(EquipType.Radiator));
        Btn_EquipReactor.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Reactor));
        Btn_EquipReactor.onClick.AddListener(() => SetEquipInfoData_SelectedEquip(EquipType.Reactor));
        Btn_EquipEngine.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Thruster));
        Btn_EquipEngine.onClick.AddListener(() => SetEquipInfoData_SelectedEquip(EquipType.Thruster));

        Btn_EquipSelected.onClick.AddListener(TryEquip_OnBtn_EquipSelectedClick);
        Btn_UnEquipSelected.onClick.AddListener(UnEquip_OnBtn_UnEquipSelectedClick);

        UIManager.OnShipMenuWdwOn += () => SelectShip("4F1", 0);
        UIManager.OnShipMenuWdwOn += () => SelectWdw(Wdw_Info);
        UIManager.OnShipMenuWdwOff += () => SelectShip("null", -1);

        _equipIconList = new List<EquipIcon>();
        for (int i = 0; i < RectTransform_SCV_Content.childCount; i++)
        {
            _equipIconList.Add(RectTransform_SCV_Content.GetChild(i).GetComponent<EquipIcon>());
        }
        _activeIconCount = 0;        
    }

    public void SelectShip(string name, int shipKey)
    {
        _selectedShip = shipKey;
        _transform_CamViewTarget = Transform_ShipDummyParent.Find(name); 
        if(shipKey >= 0)
        {
            _vm.RefreshShipData(shipKey);
        }
    }
    public void SelectWdw(GameObject wdw)
    {
        if(wdw == Wdw_Info)
        {
            Wdw_Info.SetActive(true);
            Wdw_CombatSlot.SetActive(false);
            Wdw_UtilSlot.SetActive(false);
            Wdw_EquipList.SetActive(false);
            SetActiveEquipInfoWdw(false);
        }
        else if(wdw == Wdw_CombatSlot)
        {
            Wdw_Info.SetActive(false);
            Wdw_CombatSlot.SetActive(true);
            Wdw_UtilSlot.SetActive(false);
            Wdw_EquipList.SetActive(false);
            SetActiveEquipInfoWdw(false);
        }
        else if(wdw == Wdw_UtilSlot)
        {
            Wdw_Info.SetActive(false);
            Wdw_CombatSlot.SetActive(false);
            Wdw_UtilSlot.SetActive(true);
            Wdw_EquipList.SetActive(false);
            SetActiveEquipInfoWdw(false);
        }
    }
    public void SetActiveEquipListWdw(bool value, EquipType equipType)
    {
        Wdw_EquipList.SetActive(value);
        if (value == true)
        {
            _viewEquipType = equipType;
            SetEquipIconList(equipType);
        }                    
    }
    void SetEquipInfoData_SelectedEquip(EquipType equipType)
    {
        string equipedItemKey = JsonDataManager.DataLode_UserHaveShipData(_selectedShip).GetUtilEquipedItemKey(equipType);
        if (equipedItemKey != null && equipedItemKey != string.Empty)
        {
            SetEquipInfoData_SelectedEquip(equipedItemKey);
        }
    }
    void SetEquipInfoData_SelectedEquip(string selectedEquipKey)
    {
        SetActiveEquipInfoWdw(true);
        SetEquipInfoData(selectedEquipKey);
    }
    void SetActiveEquipInfoWdw(bool value)
    {
        this.UIManager.SetActiveWdw_EquipInfo(value);
    }
    void SetEquipInfoData(string key)
    {
        this.UIManager.SetEquipInfo_StringKey(key);
        SetActive_EquipSelectedBtns(key);
    }    
    void SetEquipIconList(EquipType equipType)
    {
        _viewEquipType = equipType;
        for (int i = 0; i < _activeIconCount; i++)
        {
            _equipIconList[i].gameObject.SetActive(false);            
        }
        _activeIconCount = 0;

        foreach (var item in JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache._dic)
        {
            UserHaveEquipData data = item.Value;
            EquipTable table = JsonDataManager.DataLode_EquipTable(data._equipTableKey);
            if (table._type == equipType)
            {
                EquipIcon icon = _equipIconList[_activeIconCount];
                icon.gameObject.SetActive(true);
                _activeIconCount++;

                Sprite sprite = Resources.Load<Sprite>("Sprites/ShipBuilderIcon/Sprites/" + table._spriteName);
                if (sprite != null)
                {
                    icon.SetSprite(sprite);
                }
                icon.SetIsEquipedLabel(data._equipedShipKey >= 0);

                icon.RemoveAllListeners();
                icon.AddListener(() => SetActiveEquipInfoWdw(true));
                icon.AddListener(() => SetEquipInfoData(data._itemUniqueKey));
            }
        }
        Vector2 sizeDelta = RectTransform_SCV_Content.sizeDelta;
        sizeDelta.y = 1200 + Mathf.Max(((_activeIconCount/5)-6) * 150, 0);
        RectTransform_SCV_Content.sizeDelta = sizeDelta;

        Vector2 pos = RectTransform_SCV_Content.anchoredPosition;
        pos.y = 0;
        RectTransform_SCV_Content.anchoredPosition = pos;
    }

    //void SetActive_EquipSlotCount(int count)
    //{
    //    for (int i = 0; i < CombatEquipedSlotArray.Length; i++)
    //    {
    //        if (i < count)
    //        {
    //            CombatEquipedSlotArray[i].SetActive(true);
    //        }
    //        else
    //        {
    //            CombatEquipedSlotArray[i].SetActive(false);
    //        }
    //    }
    //}
    void SetActive_EquipSelectedBtns(string selectedEquipUniqueKey)
    {
        if(selectedEquipUniqueKey == null || selectedEquipUniqueKey == string.Empty)//장비를 선택하지 않은 경우
        {
            Btn_EquipSelected.gameObject.SetActive(false);
            Btn_UnEquipSelected.gameObject.SetActive(false);
            return;
        }

        UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(selectedEquipUniqueKey);
        if(data._equipedShipKey == -1)//선택한 장비가 장착되어있지 않은 경우
        {
            Btn_EquipSelected.gameObject.SetActive(true);
            Btn_UnEquipSelected.gameObject.SetActive(false);
        }
        else//선택한 장비가 이미 장착되어있는 경우
        {
            Btn_EquipSelected.gameObject.SetActive(true);
            Btn_UnEquipSelected.gameObject.SetActive(true);
        }
    }
    void TryEquip_OnBtn_EquipSelectedClick()
    {
        string key = this.UIManager.GetEquipInfo_StringKey();
        _vm.CommandEquip(key, _selectedShip);
        SetEquipIconList(_viewEquipType);
        SetActive_EquipSelectedBtns(key);
    }
    void UnEquip_OnBtn_UnEquipSelectedClick()
    {
        string key = this.UIManager.GetEquipInfo_StringKey();
        _vm.CommandUnEquip(key);
        SetEquipIconList(_viewEquipType);
        SetActive_EquipSelectedBtns(key);
    }

    private void LateUpdate()
    {
        CamMoveOnLateUpdate();
    }
    float radius = 200;
    float currentAngle = 0f;
    void CamMoveOnLateUpdate()
    {
        if (_transform_CamViewTarget != null)
        {
            Camera_RotateCam.gameObject.SetActive(true);
            // 현재 프레임의 회전 각도 갱신
            currentAngle += 10 * Time.deltaTime;
            if (currentAngle >= 360.0f)
            {
                currentAngle -= 360.0f;
            }

            // 라디안 단위로 변환
            float angleRad = currentAngle * Mathf.Deg2Rad;

            // 새로운 위치 계산
            Vector3 offSet = new Vector3(
                Mathf.Cos(angleRad) * radius,
                50.0f,
                Mathf.Sin(angleRad) * radius
            );

            // 타겟 위치를 기준으로 오프셋 적용
            Vector3 targetPos = _transform_CamViewTarget.position + offSet;

            //Camera_RotateCam.transform.position = Vector3.Lerp(Camera_RotateCam.transform.position, targetPos, Time.deltaTime * 5);
            Camera_RotateCam.transform.position = targetPos;
            Camera_RotateCam.transform.LookAt(_transform_CamViewTarget);
        }
        else
        {
            Camera_RotateCam.gameObject.SetActive(false);
        }
    }
}