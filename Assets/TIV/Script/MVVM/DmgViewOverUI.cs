using EnumTypes;
using TMPro;
using UnityEngine;

public class DmgViewOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP_DmgText;

    RectTransform _rectTransform;
    float _activeTime;
    float _lifeTime;
    Vector3 _originPos;

    public Camera GetHighestPriorityCamera()
    {
        Camera[] allCameras = Camera.allCameras;
        Camera highestPriorityCamera = null;
        float maxDepth = float.MinValue;

        foreach (Camera cam in allCameras)
        {
            if (cam.depth > maxDepth)
            {
                maxDepth = cam.depth;
                highestPriorityCamera = cam;
            }
        }

        return highestPriorityCamera;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(int dmg, WeaponProjectileType type, float lifeTime, Vector3 originPos)
    {
        _activeTime = 0;
        _lifeTime = lifeTime;
        _originPos = originPos;

        TMP_DmgText.text = $"{dmg}";
        switch (type)
        {
            case WeaponProjectileType.Physics:
                TMP_DmgText.color = Color.yellow;
                break;
            case WeaponProjectileType.Optics:
                TMP_DmgText.color = Color.green;
                break;
            case WeaponProjectileType.Particle:
                TMP_DmgText.color = Color.white;
                break;
            case WeaponProjectileType.Plasma:
                TMP_DmgText.color = Color.cyan;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(GetHighestPriorityCamera(), _originPos);
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 position = screenPoint - screenSize * 0.5f;
        //position *= screenAdjustFactor;
        _rectTransform.anchoredPosition = position + new Vector2(0, _activeTime * 50);
        
        _activeTime += Time.deltaTime;
        if(_activeTime > _lifeTime )
        {
            ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
        }
    }

    //public void SetTargetObject(ShipMaster target)
    //{
    //    targetObject = target;

    //    if (target == null)
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        this.gameObject.SetActive(true);
    //    }
    //}
}
