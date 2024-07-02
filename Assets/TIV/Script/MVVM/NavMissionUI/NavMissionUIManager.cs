using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;

public class NavMissionUIManager : MonoBehaviour
{
    [SerializeField] Button Btn_GiveUp;
    [SerializeField] Button Btn_ReTry;
    [SerializeField] CurStagePopUp PopUp_CurStage;
    [SerializeField] TextMeshProUGUI Text_CurStage;

    NavMissionUIManagerViewModel _vm;

    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new NavMissionUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            Btn_GiveUp.onClick.AddListener(_vm.Command_GiveUp);
            Btn_ReTry.onClick.AddListener(_vm.Command_Retry);
            _vm.Register_OnStageChange();
            _vm.Command_Refresh();
        }
    }
    private void OnDisable()
    {
        if(_vm != null)
        {
            _vm.UnRegister_OnStageChange();
            Btn_ReTry.onClick.RemoveAllListeners();
            Btn_GiveUp.onClick.RemoveAllListeners();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }    


    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.CurPrmStage):
                PopUp_CurStage.PopUpCurStageUI(_vm.CurPrmStage, _vm.CurSecStage);
                Text_CurStage.text = $"{_vm.CurPrmStage} - {_vm.CurSecStage}";
                break;
            case nameof(_vm.CurSecStage):
                PopUp_CurStage.PopUpCurStageUI(_vm.CurPrmStage, _vm.CurSecStage);
                Text_CurStage.text = $"{_vm.CurPrmStage} - {_vm.CurSecStage}";
                break;
            case nameof(_vm.CanGiveUp):
                Btn_GiveUp.gameObject.SetActive(_vm.CanGiveUp);
                break;
            case nameof(_vm.CanRetry):
                Btn_ReTry.gameObject.SetActive(_vm.CanRetry);
                break;
        }
    }
}
