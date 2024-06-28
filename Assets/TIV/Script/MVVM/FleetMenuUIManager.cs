using UnityEngine;

public class FleetMenuUIManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero); // y=0 ��� ����

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter); // y=0 ������ ������ ���
                Debug.Log("Hit Point: " + hitPoint);
                // ������(hitPoint)�� ����Ͽ� ���ϴ� ���� ����
            }
        }
    }
}
