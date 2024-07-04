using EnumTypes;
using System.ComponentModel;
using TMPro;
using UI.Extension;
using UnityEngine;
using ViewModel.Extensions;

public class UsingShipOverUIManager : MonoBehaviour
{
    [Header("선택 함선 정보 표시 필드")]
    [SerializeField] GameObject Wdw_SelectShipInfoView;
    [SerializeField] TextMeshProUGUI TMP_Hp;
    [SerializeField] TextMeshProUGUI TMP_Atk;
    [SerializeField] TextMeshProUGUI TMP_Def;
    [SerializeField] TextMeshProUGUI TMP_CritRate;
    [SerializeField] TextMeshProUGUI TMP_CritDmg;
    [SerializeField] TextMeshProUGUI TMP_PhysicsDmg;
    [SerializeField] TextMeshProUGUI TMP_OpticsDmg;
    [SerializeField] TextMeshProUGUI TMP_ParticleDmg;
    [SerializeField] TextMeshProUGUI TMP_PlasmaDmg;

    [Header("UI 프리팹")]
    [SerializeField] GameObject Prefab_Icon_ShipOverUIPrf;    

    UsingShipOverUIManagerViewModel _vm;
    ShipBuffManager _buffManagerRef;
    private void Awake()
    {
        if (_vm == null)
        {
            _vm = new UsingShipOverUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_shipListChangeCallBack();
            _vm.Register_OnSelectShipCallBack();
            _vm.OnRefreshViewModel_ShipStateData(null);
        }
    }

    private void Update()
    {
        if (_buffManagerRef == null)
        {
            Wdw_SelectShipInfoView.SetActive(false);
        }
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.ChangedShipMaster):
                if (_vm.IsAdded)//새로 생성된 경우
                {
                    UsingShipOverUI useUI = ObjectPoolManager.Instance.DequeueObject(Prefab_Icon_ShipOverUIPrf).GetComponent<UsingShipOverUI>();
                    useUI.transform.SetParent(this.transform);
                    useUI.SetViewTargetObject(_vm.ChangedShipMaster);
                }
                break;
            case nameof(_vm.IsActiveInfoView):
                Wdw_SelectShipInfoView.SetActive(_vm.IsActiveInfoView);
                break;
            case nameof(_vm.Hp):
                TextSetColor(TMP_Hp, $"{_vm.Hp.SimplifyNumber()} / {_vm.MaxHp.SimplifyNumber()}", CombatStateType.Hp);
                break;
            case nameof(_vm.MaxHp):
                TextSetColor(TMP_Hp, $"{_vm.Hp.SimplifyNumber()} / {_vm.MaxHp.SimplifyNumber()}", CombatStateType.Hp);
                break;
            case nameof(_vm.Atk):
                TextSetColor(TMP_Atk, $"{_vm.Atk:F0}", CombatStateType.Atk);
                break;
            case nameof(_vm.Def):
                TextSetColor(TMP_Def, $"{_vm.Def:F0}", CombatStateType.Def);
                break;
            case nameof(_vm.CritRate):
                TextSetColor(TMP_CritRate, $"{_vm.CritRate:F1}%", CombatStateType.CritRate);
                break;
            case nameof(_vm.CritDmg):
                TextSetColor(TMP_CritDmg, $"{_vm.CritDmg:F1}%", CombatStateType.CritDmg);
                break;
            case nameof(_vm.PhysicsDmg):
                TextSetColor(TMP_PhysicsDmg, $"{_vm.PhysicsDmg:F1}%", CombatStateType.PhysicsDmg);
                break;
            case nameof(_vm.OpticsDmg):
                TextSetColor(TMP_OpticsDmg, $"{_vm.OpticsDmg:F1}%", CombatStateType.OpticsDmg);
                break;
            case nameof(_vm.ParticleDmg):
                TextSetColor(TMP_ParticleDmg, $"{_vm.ParticleDmg:F1}%", CombatStateType.ParticleDmg);
                break;
            case nameof(_vm.PlasmaDmg):
                TextSetColor(TMP_PlasmaDmg, $"{_vm.PlasmaDmg:F1}%", CombatStateType.PlasmaDmg);
                break;
            case nameof(_vm.BuffManagerRef):
                _buffManagerRef = _vm.BuffManagerRef;
                break;
        }
    }

    void TextSetColor(TextMeshProUGUI targetText, string text, CombatStateType checkType)
    {
        if(_buffManagerRef == null)
        {
            targetText.text = text;
            return;
        }

        State state = _buffManagerRef.GetFinalState(checkType);
        if (state == null || state.CurrentState() == 0)
        {
            targetText.color = Color.white;            
        }
        else if(state.CurrentState() > 0)
        {
            targetText.color = Color.green;
        }
        else
        {
            targetText.color = Color.red;
        }
        targetText.text = text;
    }
}
