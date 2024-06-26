using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class LodingManager : MonoBehaviour
{    
    public TextMeshProUGUI loadingText;
    public Slider progressBar;

    void Start()
    {
        StartCoroutine(InitializeJsonCache());
    }

    IEnumerator InitializeJsonCache()
    {
        // JsonCache �ν��Ͻ� �ʱ�ȭ
        JsonDataManager.jsonCache.Lode();

        // JsonCache �ʱ�ȭ�� �Ϸ�� ������ ��ٸ�
        yield return null;

        // �񵿱� �� �ε� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainPlayScene");
        asyncLoad.allowSceneActivation = false;

        // �ε��� �Ϸ�� ������ ��ٸ�
        while (!asyncLoad.isDone)
        {
            if (progressBar != null)
            {
                progressBar.value = asyncLoad.progress;
                loadingText.text = "Loading...";
            }

            if (asyncLoad.progress >= 0.9f)
            {
                // �ε��� �Ϸ�Ǹ� ���� Ȱ��ȭ
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}