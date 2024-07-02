using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDummy : MonoBehaviour
{
    [SerializeField] Material Mat_Material;

    List<Renderer> rendererList = new List<Renderer>();
    List<MeshFilter> meshFilterList = new List<MeshFilter>();

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
            MeshFilter[] meshFilters = child.GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter meshFilter in meshFilters)
            {
                meshFilterList.Add(meshFilter);
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
        foreach (MeshFilter meshFilter in meshFilterList)
        {
            StartCoroutine(FillMeshSlowly(meshFilter, spawnTime));
        }
    }    

    IEnumerator FillMeshSlowly(MeshFilter meshFilter, float time)
    {
        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];

        // 아래에서부터 시작하여 채우기
        float fillSpeed = 0.1f; // 채우는 속도
        float currentFillAmount = 0f;

        while (currentFillAmount < 1f)
        {
            // 색상 변경 또는 다른 처리
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].y <= currentFillAmount)
                {
                    colors[i] = Color.white; // 예시: 아래에서부터 채우는 부분을 흰색으로 채움
                }
                else
                {
                    colors[i] = Color.clear; // 채우지 않는 부분은 투명으로 처리
                }
            }

            mesh.colors = colors;

            // 채우는 속도에 따라 기다림
            yield return new WaitForSeconds(fillSpeed);
            currentFillAmount += 0.1f / time;
        }

        // 최종적으로 모든 메시를 채움
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i] = Color.white; // 예시: 마지막에 전체를 흰색으로 채움
        }
        mesh.colors = colors;
    }
}
