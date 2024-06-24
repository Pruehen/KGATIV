using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UsingShipOverUI : MonoBehaviour
{
    [SerializeField] Image hpBar;
    Button button;
    ShipMaster targetObject;
    RectTransform rectTransform;

    private void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
    }
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

    private void Update()
    {
        if(targetObject != null)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(GetHighestPriorityCamera(), targetObject.transform.position);
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 position = screenPoint - screenSize * 0.5f;
            //position *= screenAdjustFactor;
            rectTransform.anchoredPosition = position;
        }
        else
        {
            SetTargetObject(null);
        }
    }

    public void SetTargetObject(ShipMaster target)
    {
        targetObject = target;

        if(target == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);            
        }
    }

    public void SetHpbarRatio(float ratio)
    {
        hpBar.fillAmount = ratio;
    }
}
