using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModel.Extensions;

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
    int _weaponCount;

    private void Awake()
    {
        button = GetComponent<Button>();
        //eventTrigger = GetComponent<EventTrigger>();
        rectTransform = GetComponent<RectTransform>();

        button.onClick.AddListener(SelectTargetObject_OnClick);
    }
    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new UsingShipOverUIViewModel();
            _vm.PropertyChanged += OnPropertyChanged;            
            _vm.RefreshViewModel();
            GameObject_RightViewUI.SetActive(false);
        }
    }
    private void OnDisable()
    {
        if (_vm != null)
        {
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }
    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.TargetShipMaster):
                break;
            case nameof(_vm.WeaponList)://장착 무기 리스트가 변경되었을 경우, 무기 아이콘 이미지를 교체해줌
                _weaponList = _vm.WeaponList;
                for (int i = 0; i < _weaponList.Count; i++)
                {
                    string key = _weaponList[i].Table._spriteName;

                    Image_Array_WeaponCoolCover[i].sprite = LodeSprite(key);
                    Image_Array_WeaponCool[i].sprite = LodeSprite(key);

                    if (i == 4)
                        break;
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
            case nameof(_vm.WeaponCount)://현재 착용중인 무기 수량에 따라 무기 아이콘을 onoff해줌
                _weaponCount = _vm.WeaponCount;
                for (int i = 0; i < 5; i++)
                {
                    if (_weaponCount > i)
                    {
                        Image_Array_WeaponCoolCover[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        Image_Array_WeaponCoolCover[i].gameObject.SetActive(false);
                    }
                }
                break;            
        }
    }


    public Camera GetHighestPriorityCamera()
    {
        Camera[] allCameras = Camera.allCameras;
        Camera highestPriorityCamera = null;
        float maxDepth = float.MinValue;

        foreach (Camera cam in allCameras)
        {
            if (cam.depth > maxDepth)
            {
                maxDepth = cam.depth;
                highestPriorityCamera = cam;
            }
        }

        return highestPriorityCamera;
    }

    private void Update()
    {
        if(targetObject != null)
        {
            SetPosition_OnUpdate();
            UpdateCoolImage_OnUpdate();
        }
        else
        {
            SetTargetObject(null);
        }
    }

    public void SetTargetObject(ShipMaster target)
    {
        targetObject = target;

        if(target == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);            
        }
    }

    void SetPosition_OnUpdate()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(GetHighestPriorityCamera(), targetObject.transform.position);
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 position = screenPoint - screenSize * 0.5f;
        //position *= screenAdjustFactor;
        rectTransform.anchoredPosition = position;
    }
    void UpdateCoolImage_OnUpdate()
    {
        if(_isViewData)
        {
            for (int i = 0; i < _weaponCount; i++)
            {
                Image_Array_WeaponCool[i].fillAmount = _weaponList[i].GetCoolDownRatio();
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
        }
    }
    public void UnSelectTargetObject_OnDeSelect()
    {
        _vm.IsViewData = false;
    }

    public void SetHpbarRatio(float ratio)
    {
        Image_HpBar.fillAmount = ratio;
    }

    Sprite LodeSprite(string key)
    {
        Sprite sprite = Resources.Load<Sprite>("Sprites/ShipBuilderIcon/Sprites/" + key);
        return sprite;
    }
}
