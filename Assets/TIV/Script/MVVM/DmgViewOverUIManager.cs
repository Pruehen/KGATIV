using System.ComponentModel;
using UnityEngine;
using ViewModel.Extensions;

public class DmgViewOverUIManager : MonoBehaviour
{
    [SerializeField] GameObject Prefab_Text_DmgView;

    DmgViewOverUIManagerViewModel _vm;
    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new DmgViewOverUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_onDmgedCallBack();            
        }
    }
    private void OnDisable()
    {
        if (_vm != null)
        {
            _vm.UnRegister_onDmgedCallBack();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.NewDmg):
                
                DmgViewOverUI text = ObjectPoolManager.Instance.DequeueObject(Prefab_Text_DmgView).GetComponent<DmgViewOverUI>();
                text.transform.SetParent(this.transform);
                text.Init(_vm.NewDmg, _vm.Type, 1, _vm.Position);
                break;
        }
    }
}
