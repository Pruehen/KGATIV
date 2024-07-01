using UnityEngine;

public class FleetMenuUIManager : MonoBehaviour
{
    Plane plane;
    private void Awake()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
    }
    private void OnEnable()
    {
        UIManager.Instance.SetActiveWdw_UsingShipOverUIManager(false);        
    }
    private void OnDisable()
    {        
        UIManager.Instance.SetActiveWdw_UsingShipOverUIManager(true);        
    }

    public Vector3 RayCast_ScreenPointToRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // y=0 ��� ����
        Vector3 hitPoint = Vector3.zero;

        if (plane.Raycast(ray, out float enter))
        {
            hitPoint = ray.GetPoint(enter); // y=0 ������ ������ ���

            hitPoint.x = Mathf.Clamp(hitPoint.x, -250, 250);
            hitPoint.z = Mathf.Clamp(hitPoint.z, -500, 0);            
            // ������(hitPoint)�� ����Ͽ� ���ϴ� ���� ����
        }
        return hitPoint;
    }
}
