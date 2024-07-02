using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropManager : SceneSingleton<DragAndDropManager>
{    
    Action OnPointerDown;
    public void Register_OnPointerDown(Action callBack) { OnPointerDown += callBack; }
    public void UnRegister_OnPointerDown(Action callBack) { if(OnPointerDown != null) OnPointerDown -= callBack; }

    Action<Vector3, bool> OnPointerUp;
    public void Register_OnPointerUp(Action<Vector3, bool> callBack) { OnPointerUp += callBack; }
    public void UnRegister_OnPointerUp(Action<Vector3, bool> callBack) { if (OnPointerUp != null) OnPointerUp -= callBack; }
    Action<Vector3, bool> OnDrag;
    public void Register_OnDrag(Action<Vector3, bool> callBack) { OnDrag += callBack; }
    public void UnRegister_OnDrag(Action<Vector3, bool> callBack) { if (OnDrag != null) OnDrag -= callBack; }

    public Vector3 RayCast_ScreenPointToRay(out bool isDeleteZone)
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
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

    private void Update()
    {
        // 마우스 버튼이 눌렸을 때
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("OnPointerDown");
            OnPointerDown?.Invoke();
        }

        // 마우스 버튼이 떼어졌을 때
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("OnPointerUp");
            OnPointerUp?.Invoke(RayCast_ScreenPointToRay(out bool isDeleteZone), isDeleteZone);
        }

        // 마우스를 드래그할 때
        if (Input.GetMouseButton(0))
        {
            Debug.Log("OnDrag");
            OnDrag?.Invoke(RayCast_ScreenPointToRay(out bool isDeleteZone), isDeleteZone);
        }
    }
}
