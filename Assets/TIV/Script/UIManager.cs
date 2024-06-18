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

    //-----------------------------------------------------------

    [Header("장비 메뉴 관리")]
    [SerializeField] GameObject Prefab_EquipIconBtn;

    private void Awake()
    {
        SetActiveWdw_ShipMenu(false);
        Btn_ShipMenuOn.onClick.AddListener(() => SetActiveWdw_ShipMenu(true));
        Btn_ShipMenuOff.onClick.AddListener(() => SetActiveWdw_ShipMenu(false));

        SetActiveWdw_EquipMenu(false);
        Btn_EquipMenuOn.onClick.AddListener(() => SetActiveWdw_EquipMenu(true));
        Btn_EquipMenuOff.onClick.AddListener(() => SetActiveWdw_EquipMenu(false));

        SetActiveWdw_GachaMenu(false);
        Btn_GachaMenuOn.onClick.AddListener(() => SetActiveWdw_GachaMenu(true));
        Btn_GachaMenuOff.onClick.AddListener(() => SetActiveWdw_GachaMenu(false));
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
}
