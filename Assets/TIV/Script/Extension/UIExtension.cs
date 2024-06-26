
using UnityEngine;

namespace UI.Extension
{
    public static class UIExtension
    {
        public static void SetUIPos_WorldToScreenPos(this RectTransform rectTransform, Vector3 originPos)
        {
            Camera cam = GetHighestPriorityCamera();
            if (false)//cam.transform.InverseTransformDirection(cam.transform.position - originPos).z < 0)
            {
                //rectTransform.anchoredPosition = new Vector2(-3000, -3000);
            }
            else
            {
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(GetHighestPriorityCamera(), originPos);
                Vector2 screenSize = new Vector2(Screen.width, Screen.height);
                Vector2 position = screenPoint - screenSize * 0.5f;
                //position *= screenAdjustFactor;
                rectTransform.anchoredPosition = position;
            }
        }

        public static Camera GetHighestPriorityCamera()
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
    }


}
