using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UI.Extension;

//[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class UsingShipOverUI : MonoBehaviour
{
    [SerializeField] Image Image_HpBar;
    [SerializeField] GameObject GameObject_RightViewUI;

    [Header("우측 UI 필드")]
    [SerializeField] TextMeshProUGUI Text_ShipName;
    [SerializeField] Image[] Image_Array_WeaponCoolCover;
    [SerializeField] Image[] Image_Array_WeaponCool;


    Button button;
    //EventTrigger eventTrigger;
    ShipMaster targetObject;
    RectTransform rectTransform;
    UsingShipOverUIViewModel _vm;
    List<Weapon> _weaponList;
    bool _isViewData;

    private void Awake()
    {
        button = GetComponent<Button>();
        //eventTrigger = GetComponent<EventTrigger>();
        rectTransform = GetComponent<RectTransform>();

        button.onClick.AddListener(SelectTargetObject_OnClick);
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.TargetShipMaster):
                break;
            case nameof(_vm.HPRatio):
                SetHpbarRatio_OnUpdate(_vm.HPRatio);
                break;
            case nameof(_vm.WeaponList)://장착 무기 리스트가 변경되었을 경우, 무기 아이콘 이미지를 교체해줌
                _weaponList = _vm.WeaponList;
                for (int i = 0; i < 5; i++)
                {
                    if (_weaponList.Count > i)
                    {
                        string key = _weaponList[i].Table._spriteName;

                        Image_Array_WeaponCoolCover[i].gameObject.SetActive(true);
                        Image_Array_WeaponCool[i].gameObject.SetActive(true);

                        Image_Array_WeaponCoolCover[i].sprite = LodeSprite(key);
                        Image_Array_WeaponCool[i].sprite = LodeSprite(key);
                    }
                    else
                    {
                        Image_Array_WeaponCoolCover[i].gameObject.SetActive(false);
                        Image_Array_WeaponCool[i].gameObject.SetActive(false);
                    }
                }
                break;
            case nameof(_vm.IsViewData):
                _isViewData = _vm.IsViewData;
                GameObject_RightViewUI.SetActive(_isViewData);

                if(_isViewData == true)
                {
                    Text_ShipName.text = targetObject.ShipName;                    
                }
                break;         
        }
    }

    private void Update()
    {
        if(targetObject != null)
        {
            SetPosition_OnUpdate();
            UpdateCoolImage_OnUpdate();
            SetHpbarRatio_OnUpdate(targetObject.CombatData.GetHpRatio());
        }
        else
        {
            if (_vm != null)
            {
                _vm.PropertyChanged -= OnPropertyChanged;
                _vm = null;
            }
            ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
        }
    }

    public void SetViewTargetObject(ShipMaster target)
    {
        targetObject = target;        

        if (_vm == null)
        {
            _vm = new UsingShipOverUIViewModel();
            _vm.PropertyChanged += OnPropertyChanged;                       
        }
        GameObject_RightViewUI.SetActive(false);
        _vm.TargetShipMaster = targetObject;
    }

    void SetPosition_OnUpdate()
    {
        rectTransform.SetUIPos_WorldToScreenPos(targetObject.transform.position);
    }
    void UpdateCoolImage_OnUpdate()
    {
        if(_isViewData)
        {
            for (int i = 0; i < Image_Array_WeaponCool.Length && i < _weaponList.Count; i++)
            {
                Image_Array_WeaponCool[i].fillAmount = 1 - _weaponList[i].GetCoolDownRatio();
            }
        }
    }

    void SelectTargetObject_OnClick()
    {
        if(targetObject != null)
        {
            MainCameraOrbit.Instance.SetCameraTarget(targetObject.transform);
            _vm.TargetShipMaster = targetObject;
            _vm.IsViewData = true;
            kjh.GameLogicManager.Instance.SelectActiveShip(targetObject);
        }
    }
    public void UnSelectTargetObject_OnDeSelect()
    {
        _vm.IsViewData = false;
        kjh.GameLogicManager.Instance.SelectActiveShip(null);
    }

    void SetHpbarRatio_OnUpdate(float ratio)
    {
        Image_HpBar.fillAmount = ratio;
    }

    Sprite LodeSprite(string key)
    {
        Sprite sprite = Resources.Load<Sprite>("Sprites/ShipBuilderIcon/Sprites/" + key);
        return sprite;
    }
}
