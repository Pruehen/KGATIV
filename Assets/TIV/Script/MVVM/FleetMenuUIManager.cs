using UnityEngine;

public class FleetMenuUIManager : MonoBehaviour
{
    private void OnEnable()
    {
        //Time.timeScale = 0.3f;        
    }
    private void OnDisable()
    {
        //Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            Debug.Log(RayCast_ScreenPointToRay());
        }
    }

    Vector3 RayCast_ScreenPointToRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // y=0 ��� ����
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
