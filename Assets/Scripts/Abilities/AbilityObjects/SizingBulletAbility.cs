using UnityEngine;

public class SizingBulletAbility : AbilityBase
{
    [SerializeField]
    public SizingBullet SizingBullet;

    public override void AddPassiveEffect()
    {
        var defaultMage = Player.Instance.GetShootingElement();

        if (defaultMage != null)
        {
            var component = defaultMage.AddComponent<SizingBullet>();

            component.SizingSpeed = SizingBullet.SizingSpeed;
        }
    }

    public override void RemovePassiveEffect()
    {
        var defaultMage = Player.Instance.GetShootingElement();

        if (defaultMage != null)
        {
            var component = defaultMage.GetComponent<SizingBullet>();
            DestroyImmediate(component);
        }
    }
}
