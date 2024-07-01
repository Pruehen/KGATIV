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

    Plane plane;
    private void Awake()
    {
        plane = new Plane(Vector3.up, Vector3.zero);

        Btn_ShipSpawn_4F1.onClick.AddListener(() => EnterCreateMode(0));
        Btn_ShipSpawn_4D1.onClick.AddListener(() => EnterCreateMode(1));
        Btn_ShipSpawn_4C1.onClick.AddListener(() => EnterCreateMode(2));
        Btn_ShipSpawn_4B1.onClick.AddListener(() => EnterCreateMode(3));
        Btn_ShipSpawn_5T1.onClick.AddListener(() => EnterCreateMode(4));

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

    public Vector3 RayCast_ScreenPointToRay(out bool isDeleteZone)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // y=0 평면 생성
        Vector3 hitPoint = Vector3.zero;

        if (plane.Raycast(ray, out float enter))
        {
            hitPoint = ray.GetPoint(enter); // y=0 평면과의 교차점 계산

            hitPoint.x = Mathf.Clamp(hitPoint.x, -250, 250);
            hitPoint.z = Mathf.Clamp(hitPoint.z, -625, 0);            
            // 교차점(hitPoint)을 사용하여 원하는 동작 수행
        }
        isDeleteZone = hitPoint.z < -500;
        return hitPoint;
    }

    void EnterCreateMode(int shipKey)
    {
        Label_SpawnMode.gameObject.SetActive(true);
        _shipController.SelectTargetObject_OnClick(shipKey);
    }

    public void ExitCreateMode()
    {
        Label_SpawnMode.gameObject.SetActive(false);
    }
}
