using UnityEngine;

public class FleetMenuUIManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero); // y=0 평면 생성

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter); // y=0 평면과의 교차점 계산
                Debug.Log("Hit Point: " + hitPoint);
                // 교차점(hitPoint)을 사용하여 원하는 동작 수행
            }
        }
    }
}
