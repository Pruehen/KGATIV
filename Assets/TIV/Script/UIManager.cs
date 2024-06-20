using System;
using System.Collections;
using Unity.VisualScripting;
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

    [Header("장비 강화 결과창 조작")]
    [SerializeField] GameObject Popup_UpgradeResult;
    EquipUpgradePopupUIManager _equipUpgradePopupUIManager;
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

        _equipUpgradePopupUIManager = Popup_UpgradeResult.GetComponent<EquipUpgradePopupUIManager>();
    }

    public Action OnShipMenuWdwOn;
    public Action OnShipMenuWdwOff;

    public void SetActiveWdw_ShipMenu(bool value)
    {
        SetActiveWdw(Wdw_ShipMenu, value);

        if (value)
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
        SetActiveWdw(Wdw_EquipMenu, value);
    }
    public void SetActiveWdw_GachaMenu(bool value)
    {
        SetActiveWdw(Wdw_GachaMenu, value);
    }
    public void SetActiveWdw_EquipInfo(bool value)
    {        
        SetActiveWdw(Wdw_EquipInfo, value);
    }
    public void SetEquipInfo_StringKey(string key)
    {
        _equipInfoUIManager.ViewItemKey(key);
    }
    public string GetEquipInfo_StringKey()
    {
        return _equipInfoUIManager.UniqueKey;
    }

    public void PopupWdw_UpgradeResult(float time, UserHaveEquipData before, UserHaveEquipData affter)
    {
        PopupWdw(Popup_UpgradeResult, time);
        _equipUpgradePopupUIManager.ViewResult(before, affter);
    }


    void PopupWdw(GameObject popUpWdw, float popUpTime)
    {
        SetActiveWdw(popUpWdw, true);
        StartCoroutine(SetActiveWdw_Delay(popUpWdw, false, popUpTime));
    }
    IEnumerator SetActiveWdw_Delay(GameObject wdw, bool value, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SetActiveWdw(wdw, value);
    }

    void SetActiveWdw(GameObject wdw, bool value)
    {
        wdw.SetActive(value);
    }
}
