using UnityEngine;

public class ShipMainComputer : MonoBehaviour
{
    ShipEngine shipEngine;
    [SerializeField] Vector3 TargetPos;

    private void Awake()
    {
        TryGetComponent(out shipEngine);
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveTargetPos(TargetPos);
    }

    public void SetMoveTargetPos(Vector3 pos)
    {
        if(shipEngine != null)
        {
            shipEngine.SetMoveTargetPos(pos);
        }
    }
}
