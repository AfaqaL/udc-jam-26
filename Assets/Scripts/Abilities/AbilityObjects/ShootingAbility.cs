using UnityEngine;

public class ShootingAbility : AbilityBase
{
    public Shooting FireBall;
    public Transform Spawn;
    public float FireRate;

    float nextFire;

    private float damage = 10f;

    public override void Start()
    {
        IsActive = true;
        base.Start();
    }

    public override void Update()
    {
        if (Input.GetButton(Key) && IsActive)
        {
            Cast();
        }
    }

    public override void Cast()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;

            //Destroy-მ performance თუ დააგდო შევცვალოთ inactive
            var obj = Instantiate(FireBall, Spawn.position, Spawn.rotation);
            obj.gameObject.SetActive(true);
            obj.damage = damage;
            Destroy(obj.gameObject, 3f);
        }
    }
}
