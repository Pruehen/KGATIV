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

    [Header("함선 선택 관리")]
    [SerializeField] Camera Camera_MainCam;
    [SerializeField] Transform Transform_ShipDummyParent;
    [SerializeField] Button Btn_SelectShip_4F1;
    [SerializeField] Button Btn_SelectShip_4D1;
    [SerializeField] Button Btn_SelectShip_4C1;
    [SerializeField] Button Btn_SelectShip_4B1;
    [SerializeField] Button Btn_SelectShip_5T1;
    Transform _transform_CamViewTarget = null;

    [Header("함선 메뉴 관리")]
    [SerializeField] Button Btn_Info;    
    [SerializeField] Button Btn_CombatSlot;
    [SerializeField] Button Btn_UtilSlot;
    [SerializeField] GameObject Wdw_Info;
    [SerializeField] GameObject Wdw_CombatSlot;
    [SerializeField] GameObject Wdw_UtilSlot;
    [SerializeField] GameObject Wdw_EquipList;
    [SerializeField] GameObject Wdw_EquipInfo;

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
    [SerializeField] GameObject[] EquipSlotArray;

    [Header("함선 부품 필드")]
    [SerializeField] Button Btn_EquipEngine;
    [SerializeField] Button Btn_EquipReactor;
    [SerializeField] Button Btn_EquipRadiator;

    [Header("장비 리스트 필드")]
    [SerializeField] RectTransform RectTransform_SCV_Content;
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
                TMP_Class.text = _vm.Class;
                break;
            case nameof(_vm.Star):
                TMP_Star.text = _vm.Star;
                break;
            case nameof(_vm.Hp):
                TMP_Hp.text = _vm.Hp;
                break;
            case nameof(_vm.Atk):
                TMP_Atk.text = _vm.Atk;
                break;
            case nameof(_vm.Def):
                TMP_Def.text = _vm.Def;
                break;
            case nameof(_vm.CritRate):
                TMP_CritRate.text = _vm.CritRate;
                break;
            case nameof(_vm.CritDmg):
                TMP_CritDmg.text = _vm.CritDmg;
                break;
            case nameof(_vm.PhysicsDmg):
                TMP_PhysicsDmg.text = _vm.PhysicsDmg;
                break;
            case nameof(_vm.OpticsDmg):
                TMP_OpticsDmg.text = _vm.OpticsDmg;
                break;
            case nameof(_vm.ParticleDmg):
                TMP_ParticleDmg.text = _vm.ParticleDmg;
                break;
            case nameof(_vm.PlasmaDmg):
                TMP_PlasmaDmg.text = _vm.PlasmaDmg;
                break;
            case nameof(_vm.SlotCount):
                SetActive_EquipSlotCount(_vm.SlotCount);
                break;
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
        Btn_EquipRadiator.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Radiator));
        Btn_EquipReactor.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Reactor));
        Btn_EquipEngine.onClick.AddListener(() => SetActiveEquipListWdw(true, EquipType.Thruster));

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
    //public void RegisterValueChangeCallback(Action<string[]> valueChangeCallback)
    //{
    //    _valueChangeCallback += valueChangeCallback;
    //}

    //public void UnRegisterValueChangeCallback(Action<string[]> valueChangeCallback)
    //{
    //    _valueChangeCallback -= valueChangeCallback;
    //}


    public void SelectShip(string name, int shipKey)
    {
        _transform_CamViewTarget = Transform_ShipDummyParent.Find(name); 
        if(shipKey >= 0)
        {
            _vm.RefreshVielModel(shipKey);
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
            Wdw_EquipInfo.SetActive(false);
        }
        else if(wdw == Wdw_CombatSlot)
        {
            Wdw_Info.SetActive(false);
            Wdw_CombatSlot.SetActive(true);
            Wdw_UtilSlot.SetActive(false);
            Wdw_EquipList.SetActive(false);
            Wdw_EquipInfo.SetActive(false);
        }
        else if(wdw == Wdw_UtilSlot)
        {
            Wdw_Info.SetActive(false);
            Wdw_CombatSlot.SetActive(false);
            Wdw_UtilSlot.SetActive(true);
            Wdw_EquipList.SetActive(false);
            Wdw_EquipInfo.SetActive(false);
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
    public void SetActiveEquipInfoWdw(bool value, string equipDataKey)
    {
        Wdw_EquipInfo.SetActive(value);
    }


    void SetEquipIconList(EquipType equipType)
    {
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
                icon.SetListener(() => SetActiveEquipInfoWdw(true, data._itemUniqueKey));
            }
        }
        Vector2 sizeDelta = RectTransform_SCV_Content.sizeDelta;
        sizeDelta.y = 1200 + Mathf.Max(((_activeIconCount/5)-6) * 150, 0);
        RectTransform_SCV_Content.sizeDelta = sizeDelta;

        Vector2 pos = RectTransform_SCV_Content.anchoredPosition;
        pos.y = 0;
        RectTransform_SCV_Content.anchoredPosition = pos;
    }

    void SetActive_EquipSlotCount(int count)
    {
        for (int i = 0; i < EquipSlotArray.Length; i++)
        {
            if (i < count)
            {
                EquipSlotArray[i].SetActive(true);
            }
            else
            {
                EquipSlotArray[i].SetActive(false);
            }
        }
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

            //Camera_MainCam.transform.position = Vector3.Lerp(Camera_MainCam.transform.position, targetPos, Time.deltaTime * 5);
            Camera_MainCam.transform.position = targetPos;
            Camera_MainCam.transform.LookAt(_transform_CamViewTarget);
        }
    }
}