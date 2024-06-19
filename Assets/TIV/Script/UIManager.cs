using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("함선 메뉴 조작")]
    [SerializeField] GameObject Wdw_ShipMenu;
    [SerializeField] Button Btn_ShipMenuOn;
    [SerializeField] Button Btn_ShipMenuOff;

    [Header("장비 메뉴 조작")]
    [SerializeField] GameObject Wdw_EquipMenu;
    [SerializeField] Button Btn_EquipMenuOn;
    [SerializeField] Button Btn_EquipMenuOff;

    [Header("뽑기 메뉴 조작")]
    [SerializeField] GameObject Wdw_GachaMenu;
    [SerializeField] Button Btn_GachaMenuOn;
    [SerializeField] Button Btn_GachaMenuOff;

    [Header("장비 정보창 조작")]
    [SerializeField] GameObject Wdw_EquipInfo;
    EquipInfoUIManager _equipInfoUIManager;
    [SerializeField] Button[] Btn_EquipInfoOnArray;
    [SerializeField] Button[] Btn_EquipInfoOffArray;

    //-----------------------------------------------------------

    private void Awake()
    {
        SetActiveWdw_ShipMenu(false);
        Btn_ShipMenuOn.onClick.AddListener(() => SetActiveWdw_ShipMenu(true));
        Btn_ShipMenuOff.onClick.AddListener(() => SetActiveWdw_ShipMenu(false));
        Btn_ShipMenuOff.onClick.AddListener(() => SetActiveWdw_EquipInfo(false));

        SetActiveWdw_EquipMenu(false);
        Btn_EquipMenuOn.onClick.AddListener(() => SetActiveWdw_EquipMenu(true));
        Btn_EquipMenuOff.onClick.AddListener(() => SetActiveWdw_EquipMenu(false));

        SetActiveWdw_GachaMenu(false);
        Btn_GachaMenuOn.onClick.AddListener(() => SetActiveWdw_GachaMenu(true));
        Btn_GachaMenuOff.onClick.AddListener(() => SetActiveWdw_GachaMenu(false));

        _equipInfoUIManager = Wdw_EquipInfo.GetComponent<EquipInfoUIManager>();
        if (Btn_EquipInfoOnArray != null && Btn_EquipInfoOnArray.Length > 0)
        {
            foreach (Button btn in Btn_EquipInfoOnArray)
            {
                btn.onClick.AddListener(() => SetActiveWdw_EquipInfo(true));
            }
        }
        if (Btn_EquipInfoOffArray != null && Btn_EquipInfoOffArray.Length > 0)
        {
            foreach (Button btn in Btn_EquipInfoOffArray)
            {
                btn.onClick.AddListener(() => SetActiveWdw_EquipInfo(false));
            }
        }
    }

    public Action OnShipMenuWdwOn;
    public Action OnShipMenuWdwOff;

    public void SetActiveWdw_ShipMenu(bool value)
    {
        Wdw_ShipMenu.SetActive(value);

        if(value)
        {
            OnShipMenuWdwOn?.Invoke();
        }
        else
        {
            OnShipMenuWdwOff?.Invoke();
        }
    }
    public void SetActiveWdw_EquipMenu(bool value)
    {
        Wdw_EquipMenu.SetActive(value);
    }
    public void SetActiveWdw_GachaMenu(bool value)
    {
        Wdw_GachaMenu.SetActive(value);
    }
    public void SetActiveWdw_EquipInfo(bool value)
    {
        Wdw_EquipInfo.SetActive(value);        
    }
    public void SetEquipInfo_StringKey(string key)
    {
        _equipInfoUIManager.ViewItemKey(key);
    }
}
