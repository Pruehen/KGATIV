using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurStagePopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_Stage;
    [SerializeField] Image Img_PopUpImage;    
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void PopUpCurStageUI(int prmStage, int secStage)
    {
        StopAllCoroutines();

        this.gameObject.SetActive(true);
        Text_Stage.text = $"스테이지\n{prmStage} - {secStage}";

        StartCoroutine(FadeOutCoroutine(2));
    }
    IEnumerator FadeOutCoroutine(float fadeDuration)
    {
        Color imageColor = Img_PopUpImage.color;
        float startAlpha = 1;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            imageColor.a = alpha;
            Img_PopUpImage.color = imageColor;
            yield return null;
        }

        // 최종적으로 알파 값을 0으로 설정
        imageColor.a = 0f;
        Img_PopUpImage.color = imageColor;
        this.gameObject.SetActive(false);
    }
}
