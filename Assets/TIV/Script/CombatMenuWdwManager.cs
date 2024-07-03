using System.Collections;
using UnityEngine;

public class CombatMenuWdwManager : MonoBehaviour
{
    RectTransform _rectTransform;
    Vector2 _initPos;
    Vector2 _activePos;
    bool _isActive;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _initPos = _rectTransform.anchoredPosition;
        _activePos = _initPos + new Vector2(_rectTransform.sizeDelta.x, 0);
        _isActive = false;
    }

    public void SetWdwToggle()
    {
        StopAllCoroutines();
        _isActive = !_isActive;

        if (_isActive)
        {
            StartCoroutine(SetWdwPos_Lerp(_activePos));
        }
        else
        {
            StartCoroutine(SetWdwPos_Lerp(_initPos));
        }
    }

    IEnumerator SetWdwPos_Lerp(Vector2 targetPos)
    {
        Vector2 startPos = _rectTransform.anchoredPosition;
        float duration = 0.3f; // 이동 시간
        float startTime = Time.time; // 시작 시간

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration; // 보간 계수 (0에서 1 사이)
            t = 1 - Mathf.Pow(1 - t, 2); // 이동 속도를 점진적으로 감소시키는 보정

            // 보간된 위치 계산
            _rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            // 다음 프레임까지 대기
            yield return null;
        }

        // 최종 위치 보정
        _rectTransform.anchoredPosition = targetPos;
    }
}
