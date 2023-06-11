using System.Collections;
using UnityEngine;

public class DashAbility : AbilityBase
{
    public float DashingPower;
    public float DashingTime;

    TrailRenderer _tr;
    PlayerMovement _pm;

    public override void Start()
    {
        IsActive = true;
        _pm = Player.Instance.GetComponent<PlayerMovement>();
        _tr = GetComponent<TrailRenderer>();
        base.Start();
    }

    public override void Cast()
    {
        _tr.emitting = true;
        StartCoroutine(_pm.MoveFastFor(DashingPower, DashingTime));
        Invoke("StopEmitting", DashingTime);
    }

    void StopEmitting()
    {
        _tr.emitting = false;
    }
}
