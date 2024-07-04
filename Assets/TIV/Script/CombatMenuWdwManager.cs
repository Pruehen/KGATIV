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
    [SerializeField] string info_Request;
    [SerializeField] Button Btn_InfoAnnihilation;
    [SerializeField] string info_Annihilation;
    [SerializeField] Button Btn_InfoBaseAttack;
    [SerializeField] string info_BaseAttack;
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

    #region ��ư Ŭ�� ��� �޼���
    void OnClick_RequestLevel1()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level1, "���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_RequestLevel2()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level2, "���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_RequestLevel3()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level3, "���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_RequestLevel4()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Request_Level4, "���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    //=====================================================================================
    void OnClick_Alpha()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Alpha, "���� ���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_Beta()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Beta, "���� ���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_Gamma()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Gamma, "���� ���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_Delta()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(CombatMissionLogicManager.Instance.OnClick_Delta, "���� ���� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    //=====================================================================================
    void OnClick_BaseAttackLevel9()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "���� Ÿ�� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_BaseAttackLevel10()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "���� Ÿ�� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_BaseAttackLevel11()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "���� Ÿ�� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    void OnClick_BaseAttackLevel12()
    {
        UIManager.Instance.PopUpWdw_ConfirmPopUpUI(Test, "���� Ÿ�� �ӹ��� �����Ͻðڽ��ϱ�?");
    }
    #endregion


    void Test()
    {
        Debug.Log("�׽�Ʈ �޼��� ȣ�� �Ϸ�");
    }
}
