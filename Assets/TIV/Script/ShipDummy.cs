using System.Collections.Generic;
using UnityEngine;

public class ShipDummy : MonoBehaviour
{
    List<Renderer> rendererList = new List<Renderer>();
    Material material;
    bool isDeleteZoneDummy = false;

    public void Init(Material mat)
    {
        material = mat;
        foreach (Transform child in this.transform)
        {
            Renderer[] renderers = child.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material = material;
                rendererList.Add(renderer);
            }
        }
        isDeleteZoneDummy = false;
    }

    public void SetMatColor(bool isDeleteZone)
    {
        if(isDeleteZoneDummy != isDeleteZone)
        {
            isDeleteZoneDummy = isDeleteZone;
            foreach (Renderer renderer in rendererList)
            {
                if(isDeleteZoneDummy == false)
                {
                    renderer.material.SetColor("_BaseColor", new Color(0.5f, 1, 0.5f, 0.5f));
                }                
                else
                {
                    renderer.material.SetColor("_BaseColor", new Color(1, 0.5f, 0.5f, 0.5f));
                }
            }
        }
    }
}
