using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VirtualLodingWdw : MonoBehaviour
{
    [SerializeField] Image Img_Cover;
    [SerializeField] Image Img_LodingBar;
    [SerializeField] Image Img_LodingIcon;
    [SerializeField] TextMeshProUGUI TMP_Tip;

    public void StartLoding(float time)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(LoadingCoroutine(time));
    }

    private void Update()
    {
        Img_LodingIcon.transform.Rotate(transform.forward, -90 * Time.deltaTime);
    }

    IEnumerator LoadingCoroutine(float time)
    {
        float elapsedTime = 0f;
        Img_LodingBar.fillAmount = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Random.Range(0, 0.6f);
            Img_LodingBar.fillAmount = Mathf.Clamp01(0.7f * elapsedTime / time);

            // 시간 간격을 설정하여 FillAmount가 뚝뚝 끊기듯이 채워지도록 함
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }

        Img_LodingBar.fillAmount = 1f; // 로딩 완료 시 FillAmount를 1로 설정
        yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));
        this.gameObject.SetActive(false);
    }
}
