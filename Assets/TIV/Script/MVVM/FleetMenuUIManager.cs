using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleetMenuUIManager : MonoBehaviour
{
    [SerializeField] ShipController _shipController;

    [SerializeField] TextMeshProUGUI Label_SpawnMode;
    [SerializeField] TextMeshProUGUI Text_FleetCost;
    [SerializeField] Button Btn_ShipSpawn_4F1;
    [SerializeField] Button Btn_ShipSpawn_4D1;
    [SerializeField] Button Btn_ShipSpawn_4C1;
    [SerializeField] Button Btn_ShipSpawn_4B1;
    [SerializeField] Button Btn_ShipSpawn_5T1;

    private void Awake()
    {
        Btn_ShipSpawn_4F1.onClick.AddListener(() => EnterCreateMode_OnClick(0));
        Btn_ShipSpawn_4D1.onClick.AddListener(() => EnterCreateMode_OnClick(1));
        Btn_ShipSpawn_4C1.onClick.AddListener(() => EnterCreateMode_OnClick(2));
        Btn_ShipSpawn_4B1.onClick.AddListener(() => EnterCreateMode_OnClick(3));
        Btn_ShipSpawn_5T1.onClick.AddListener(() => EnterCreateMode_OnClick(4));

        Label_SpawnMode.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        UIManager.Instance.SetActiveWdw_UsingShipOverUIManager(false);        
    }
    private void OnDisable()
    {        
        UIManager.Instance.SetActiveWdw_UsingShipOverUIManager(true);        
    }


    void EnterCreateMode_OnClick(int shipKey)
    {
        Label_SpawnMode.gameObject.SetActive(true);
        _shipController.SelectTargetObject_OnBtnClick(shipKey);        
    }

    public void ExitCreateMode_OnPointerUp()
    {             
        Label_SpawnMode.gameObject.SetActive(false);
    }
}
