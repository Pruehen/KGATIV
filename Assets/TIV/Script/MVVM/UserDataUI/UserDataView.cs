using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using ViewModel.Extensions;

public class UserDataView : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> TMPList_Credit;
    [SerializeField] List<TextMeshProUGUI> TMPList_SuperCredit;
    [SerializeField] List<TextMeshProUGUI> TMPList_Fuel;

    UserDataViewModel _vm;

    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new UserDataViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_OnRefreshViewModel();
            _vm.RefreshViewModel_OnInit();
        }
    }
    private void OnDisable()
    {
        if (_vm != null)
        {
            _vm.UnRegister_OnRefreshViewModel();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.Credit):
                TextUpdate(TMPList_Credit, $"{_vm.Credit}");
                break;
            case nameof(_vm.SuperCredit):
                TextUpdate(TMPList_SuperCredit, $"{_vm.SuperCredit}");
                break;
            case nameof(_vm.Fuel):
                TextUpdate(TMPList_Fuel, $"{_vm.Fuel}");
                break;
            case nameof(_vm.CurPrmStage):
                break;
            case nameof(_vm.CurSecStage):
                break;
        }
    }

    void TextUpdate(List<TextMeshProUGUI> tmpList, string newtext)
    {
        if (tmpList == null || tmpList.Count == 0) return;

        foreach (TextMeshProUGUI tmp in tmpList)
        {
            tmp.text = newtext;
        }
    }
}
