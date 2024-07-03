using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenuWdwManager : MonoBehaviour
{
    [Header("이동시킬 UI")]
    [SerializeField] RectTransform Rect_BtnGroup;

    [Header("메인 뎁스")]
    [SerializeField] RectTransform Rect_MainDepth;
    [SerializeField] Button Btn_SelectRequest;
    [SerializeField] Button Btn_SelectAnnihilation;
    [SerializeField] Button Btn_SelectBaseAttack;
    [SerializeField] Button Btn_InfoRequest;
    [SerializeField] Button Btn_InfoAnnihilation;
    [SerializeField] Button Btn_InfoBaseAttack;
    Vector2 _mainDepthPos;

    [Header("협약의뢰 뎁스")]
    [SerializeField] RectTransform Rect_RequestDepth;
    [SerializeField] Button Btn_SelectLevel1;
    [SerializeField] Button Btn_SelectLevel2;
    [SerializeField] Button Btn_SelectLevel3;
    [SerializeField] Button Btn_SelectLevel4;
    Vector2 _requestDepthPos;

    [Header("섬멸작전 뎁스")]
    [SerializeField] RectTransform Rect_AnnihilationDepth;
    [SerializeField] Button Btn_SelectAlpha;
    [SerializeField] Button Btn_SelectBeta;
    [SerializeField] Button Btn_SelectGamma;
    [SerializeField] Button Btn_SelectDelta;
    Vector2 _annihilationDepthPos;

    [Header("기지타격 뎁스")]
    [SerializeField] RectTransform Rect_BaseAttackDepth;
    [SerializeField] Button Btn_SelectLevel9;
    [SerializeField] Button Btn_SelectLevel10;
    [SerializeField] Button Btn_SelectLevel11;
    [SerializeField] Button Btn_SelectLevel12;
    Vector2 _baseAttackDepthPos;

    [Header("뒤로가기 버튼")]
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
        float duration = 0.3f; // 이동 시간
        float startTime = Time.time; // 시작 시간

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration; // 보간 계수 (0에서 1 사이)
            t = 1 - Mathf.Pow(1 - t, 2); // 이동 속도를 점진적으로 감소시키는 보정

            // 보간된 위치 계산
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            // 다음 프레임까지 대기
            yield return null;
        }

        // 최종 위치 보정
        rectTransform.anchoredPosition = targetPos;
    }
}
