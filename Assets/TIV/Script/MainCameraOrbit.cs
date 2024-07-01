using UnityEngine;

public class MainCameraOrbit : SceneSingleton<MainCameraOrbit>
{
    [SerializeField] Transform target; // ȸ���� �߽���
    [SerializeField] float rotationSpeed = 15.0f; // ȸ�� �ӵ�
    [SerializeField] float distance = 600f; // ī�޶�� �߽��� ������ �Ÿ�
    [SerializeField] float inertia = 0.97f;
    [SerializeField] bool isTopviewFixed = false;

    Vector3 _centerPosTemp;

    float yaw = 0.0f;
    float pitch = 0.0f;

    float deltaYaw = 0;
    float deltaPitch = 0;

    void Start()
    {
        // �ʱ� ��ġ ����
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // ī�޶� �ʱ� ��ġ
        UpdateCameraPosition();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // ���콺 ���� ��ư Ŭ�� �� �巡��
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            deltaYaw += mouseX * rotationSpeed;
            deltaPitch -= (mouseY * rotationSpeed);
        }

        if (Input.touchCount == 1) // �� �հ��� ��ġ �� �巡��
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

            // ��ġ ���� (��: -30������ 30�� ����)
            pitch = Mathf.Clamp(pitch, -30f, 30f);

            // ȸ�� ���ʹϾ� ����
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            Vector3 targetPos = ((target == null) ? _centerPosTemp : target.position);
            _centerPosTemp = targetPos;
            // ī�޶� ��ġ ���
            Vector3 newPos = rotation * new Vector3(0, 0, -distance) + targetPos;

            // ī�޶� ��ġ�� ȸ�� ����
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
