using UnityEngine;

public class MainCameraOrbit : SceneSingleton<MainCameraOrbit>
{
    [SerializeField] Transform target; // 회전할 중심점
    [SerializeField] float rotationSpeed = 15.0f; // 회전 속도
    [SerializeField] float distance = 600f; // 카메라와 중심점 사이의 거리
    [SerializeField] float inertia = 0.97f;
    [SerializeField] bool isTopviewFixed = false;

    Vector3 _centerPosTemp;

    float yaw = 0.0f;
    float pitch = 0.0f;

    float deltaYaw = 0;
    float deltaPitch = 0;

    void Start()
    {
        // 초기 위치 설정
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // 카메라 초기 위치
        UpdateCameraPosition();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 클릭 및 드래그
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            deltaYaw += mouseX * rotationSpeed;
            deltaPitch -= (mouseY * rotationSpeed);
        }

        if (Input.touchCount == 1) // 한 손가락 터치 및 드래그
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float touchX = touch.deltaPosition.x;
                float touchY = touch.deltaPosition.y;

                deltaYaw += touchX * rotationSpeed * 0.1f;
                deltaPitch -= touchY * rotationSpeed * 0.1f;
            }
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (isTopviewFixed == false)
        {
            pitch += deltaPitch * Time.deltaTime;
            yaw += deltaYaw * Time.deltaTime;

            // 피치 제한 (예: -30도에서 30도 사이)
            pitch = Mathf.Clamp(pitch, -30f, 30f);

            // 회전 쿼터니언 생성
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            Vector3 targetPos = ((target == null) ? _centerPosTemp : target.position);
            _centerPosTemp = targetPos;
            // 카메라 위치 계산
            Vector3 newPos = rotation * new Vector3(0, 0, -distance) + targetPos;

            // 카메라 위치와 회전 적용
            transform.position = newPos;
            //transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2);
            transform.LookAt(targetPos);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - this.transform.position), Time.deltaTime * 5);

            deltaPitch *= inertia;
            deltaYaw *= inertia;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 800, 50), Time.deltaTime * 5);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, -90, 0), Time.deltaTime * 5);
        }
    }

    public void SetCameraTarget(Transform target)
    {
        this.target = target;
    }
    public void SetIsTopviewFixed(bool value)
    {
        isTopviewFixed = value;
        SetCameraTarget(null);
    }
}
