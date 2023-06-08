using UnityEngine;

public class FireballAbility : AbilityBase
{
    public BallShooting FireBall;
    public Transform Spawn;
    public float FireRate;

    float nextFire;

    public override void SetupAbility()
    {
        Player.Instance.SetDefaultMagicPrefab(FireBall.gameObject);
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

            Destroy(obj.gameObject, 3f);
        }
    }
}
