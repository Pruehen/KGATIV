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
        // JsonCache 인스턴스 초기화
        JsonDataManager.jsonCache.Lode();

        // JsonCache 초기화가 완료될 때까지 기다림
        yield return null;

        // 비동기 씬 로딩 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainPlayScene");
        asyncLoad.allowSceneActivation = false;

        // 로딩이 완료될 때까지 기다림
        while (!asyncLoad.isDone)
        {
            if (progressBar != null)
            {
                progressBar.value = asyncLoad.progress;
                loadingText.text = "Loading...";
            }

            if (asyncLoad.progress >= 0.9f)
            {
                // 로딩이 완료되면 씬을 활성화
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}