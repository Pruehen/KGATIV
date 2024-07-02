using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDummy : MonoBehaviour
{
    [SerializeField] Material Mat_Material;

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

    private void Awake()
    {
        if(Mat_Material != null)
        {
            Init(Mat_Material);
        }
    }

    public void SetMatColor_DeleteZoneCheck(bool isDeleteZone)
    {
        if(isDeleteZoneDummy != isDeleteZone)
        {
            isDeleteZoneDummy = isDeleteZone;
            foreach (Renderer renderer in rendererList)
            {
                if(isDeleteZoneDummy == false)
                {
                    renderer.material.SetColor("_BaseColor", new Color(0.5f, 1, 0.5f, 0.15f));
                }                
                else
                {
                    renderer.material.SetColor("_BaseColor", new Color(1, 0.5f, 0.5f, 0.15f));
                }
            }
        }
    }
    public void SetMatColor_Spawn(float spawnTime)
    {
        foreach (Renderer renderer in rendererList)
        {
            renderer.material.SetColor("_BaseColor", new Color(0.75f, 1f, 1f, 0.05f));            
        }
    }
}
