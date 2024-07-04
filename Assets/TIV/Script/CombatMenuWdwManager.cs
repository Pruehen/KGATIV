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
    [SerializeField] string info_Request;
    [SerializeField] Button Btn_InfoAnnihilation;
    [SerializeField] string info_Annihilation;
    [SerializeField] Button Btn_InfoBaseAttack;
    [SerializeField] string info_BaseAttack;
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

        Btn_InfoRequest.onClick.AddListener(() => UIManager.Instance.PopUpWdw_InfoPopUpUI(info_Request, 10));
        Btn_InfoAnnihilation.onClick.AddListener(() => UIManager.Instance.PopUpWdw_InfoPopUpUI(info_Annihilation, 10));
        Btn_InfoBaseAttack.onClick.AddListener(() => UIManager.Instance.PopUpWdw_InfoPopUpUI(info_BaseAttack, 10));

        //============================================
        Btn_SelectLevel1.onClick.AddListener(OnClick_RequestLevel1);
        Btn_SelectLevel2.onClick.AddListener(OnClick_RequestLevel2);
        Btn_SelectLevel3.onClick.AddListener(OnClick_RequestLevel3);
        Btn_SelectLevel4.onClick.AddListener(OnClick_RequestLevel4);

        Btn_SelectAlpha.onClick.AddListener(OnClick_Alpha);
        Btn_SelectBeta.onClick.AddListener(OnClick_Beta);
        Btn_SelectGamma.onClick.AddListener(OnClick_Gamma);
        Btn_SelectDelta.onClick.AddListener(OnClick_Delta);

        Btn_SelectLevel9.onClick.AddListener(OnClick_BaseAttackLevel9);
        Btn_SelectLevel10.onClick.AddListener(OnClick_BaseAttackLevel10);
        Btn_SelectLevel11.onClick.AddListener(OnClick_BaseAttackLevel11);
        Btn_SelectLevel12.onClick.AddListener(OnClick_BaseAttackLevel12);
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

    #region 버튼 클릭 등록 메서드
    void OnClick_RequestLevel1()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level1, "협약 임무에 입장하시겠습니까?");
    }
    void OnClick_RequestLevel2()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level2, "협약 임무에 입장하시겠습니까?");
    }
    void OnClick_RequestLevel3()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level3, "협약 임무에 입장하시겠습니까?");
    }
    void OnClick_RequestLevel4()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level4, "협약 임무에 입장하시겠습니까?");
    }
    //=====================================================================================
    void OnClick_Alpha()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Alpha, "추적 섬멸 임무에 입장하시겠습니까?");
    }
    void OnClick_Beta()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Beta, "추적 섬멸 임무에 입장하시겠습니까?");
    }
    void OnClick_Gamma()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Gamma, "추적 섬멸 임무에 입장하시겠습니까?");
    }
    void OnClick_Delta()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Delta, "추적 섬멸 임무에 입장하시겠습니까?");
    }
    //=====================================================================================
    void OnClick_BaseAttackLevel9()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "기지 타격 임무에 입장하시겠습니까?");
    }
    void OnClick_BaseAttackLevel10()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "기지 타격 임무에 입장하시겠습니까?");
    }
    void OnClick_BaseAttackLevel11()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "기지 타격 임무에 입장하시겠습니까?");
    }
    void OnClick_BaseAttackLevel12()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "기지 타격 임무에 입장하시겠습니까?");
    }
    #endregion


    void Test()
    {
        Debug.Log("테스트 메서드 호출 완료");
    }
}
