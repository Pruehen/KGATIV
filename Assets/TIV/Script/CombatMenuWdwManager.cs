using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenuWdwManager : MonoBehaviour
{
    [Header("�̵���ų UI")]
    [SerializeField] RectTransform Rect_BtnGroup;

    [Header("���� ����")]
    [SerializeField] RectTransform Rect_MainDepth;
    [SerializeField] Button Btn_SelectRequest;
    [SerializeField] Button Btn_SelectAnnihilation;
    [SerializeField] Button Btn_SelectBaseAttack;
    [SerializeField] Button Btn_InfoRequest;
    [SerializeField] Button Btn_InfoAnnihilation;
    [SerializeField] Button Btn_InfoBaseAttack;
    Vector2 _mainDepthPos;

    [Header("�����Ƿ� ����")]
    [SerializeField] RectTransform Rect_RequestDepth;
    [SerializeField] Button Btn_SelectLevel1;
    [SerializeField] Button Btn_SelectLevel2;
    [SerializeField] Button Btn_SelectLevel3;
    [SerializeField] Button Btn_SelectLevel4;
    Vector2 _requestDepthPos;

    [Header("�������� ����")]
    [SerializeField] RectTransform Rect_AnnihilationDepth;
    [SerializeField] Button Btn_SelectAlpha;
    [SerializeField] Button Btn_SelectBeta;
    [SerializeField] Button Btn_SelectGamma;
    [SerializeField] Button Btn_SelectDelta;
    Vector2 _annihilationDepthPos;

    [Header("����Ÿ�� ����")]
    [SerializeField] RectTransform Rect_BaseAttackDepth;
    [SerializeField] Button Btn_SelectLevel9;
    [SerializeField] Button Btn_SelectLevel10;
    [SerializeField] Button Btn_SelectLevel11;
    [SerializeField] Button Btn_SelectLevel12;
    Vector2 _baseAttackDepthPos;

    [Header("�ڷΰ��� ��ư")]
    [SerializeField] List<Button> BtnList_ToBack;

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

        _mainDepthPos = Rect_MainDepth.anchoredPosition;
        _requestDepthPos = Rect_RequestDepth.anchoredPosition;
        _annihilationDepthPos = Rect_AnnihilationDepth.anchoredPosition;
        _baseAttackDepthPos = Rect_BaseAttackDepth.anchoredPosition;

        Btn_SelectRequest.onClick.AddListener(() => SetRectPosCoroutine_Lerp(Rect_BtnGroup, -_requestDepthPos));
        Btn_SelectAnnihilation.onClick.AddListener(() => SetRectPosCoroutine_Lerp(Rect_BtnGroup, -_annihilationDepthPos));
        Btn_SelectBaseAttack.onClick.AddListener(() => SetRectPosCoroutine_Lerp(Rect_BtnGroup, -_baseAttackDepthPos));

        foreach (Button btn in BtnList_ToBack)
        {
            btn.onClick.AddListener(() => SetRectPosCoroutine_Lerp(Rect_BtnGroup, -_mainDepthPos));
        }
    }

    public void SetWdwToggle()
    {                
        _isActive = !_isActive;

        if (_isActive)
        {
            SetRectPosCoroutine_Lerp(_rectTransform, _activePos);
        }
        else
        {
            SetRectPosCoroutine_Lerp(_rectTransform, _initPos);
            SetRectPosCoroutine_Lerp(Rect_BtnGroup, -_mainDepthPos);
        }
    }

    void SetRectPosCoroutine_Lerp(RectTransform rectTransform, Vector2 targetPos)
    {
        StartCoroutine(SetRectPos_Lerp(rectTransform, targetPos));
    }
    IEnumerator SetRectPos_Lerp(RectTransform rectTransform, Vector2 targetPos)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        float duration = 0.3f; // �̵� �ð�
        float startTime = Time.time; // ���� �ð�

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration; // ���� ��� (0���� 1 ����)
            t = 1 - Mathf.Pow(1 - t, 2); // �̵� �ӵ��� ���������� ���ҽ�Ű�� ����

            // ������ ��ġ ���
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            // ���� �����ӱ��� ���
            yield return null;
        }

        // ���� ��ġ ����
        rectTransform.anchoredPosition = targetPos;
    }
}
