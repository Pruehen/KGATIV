using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;

public class TopLeftView : MonoBehaviour
{
    [SerializeField] Text Text_Name;
    [SerializeField] Text Text_Level;
    [SerializeField] Image Image_Icon;

    //ºä¸ðµ¨ ¼ÒÀ¯
    TopLeftViewModel _vm;

    private void OnEnable()
    {
        if(_vm == null)
        {
            _vm = new TopLeftViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.RegisterEventsOnEnable();
            _vm.RefreshVielModel();
        }
    }
    private void OnDisable()
    {
        if(_vm != null)
        {
            _vm.UnRegisterEventsOnDisable();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.Name):
                Text_Name.text = _vm.Name;
                break;
            case nameof(_vm.Level):
                Text_Level.text = $"{_vm.Level}";
                break;
            case nameof(_vm.IconName):
                break;
        }
    }
}
