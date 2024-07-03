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
        float duration = 0.3f; // �̵� �ð�
        float startTime = Time.time; // ���� �ð�

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration; // ���� ��� (0���� 1 ����)
            t = 1 - Mathf.Pow(1 - t, 2); // �̵� �ӵ��� ���������� ���ҽ�Ű�� ����

            // ������ ��ġ ���
            _rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            // ���� �����ӱ��� ���
            yield return null;
        }

        // ���� ��ġ ����
        _rectTransform.anchoredPosition = targetPos;
    }
}
