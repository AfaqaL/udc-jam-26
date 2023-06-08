using System;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    public bool IsPassive;
    public bool IsActive;
    public string Key;
    public float Cooldown;
    public MagicType MagicType;

    float _timer;

    public virtual void Start()
    {
        _timer = Cooldown;
    }

    public virtual void Update()
    {
        if (!IsPassive && Input.GetButtonDown(Key) && 
            _timer >= Cooldown && 
            IsActive)
        {
            Cast();

            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    public virtual void Cast()
    {
    }

    public virtual void AddPassiveEffect()
    {
    }

    public virtual void RemovePassiveEffect()
    {
    }

    public void SetActive(bool active)
    {
        IsActive = active;
        gameObject.SetActive(active);

        SetupAbility();
        if (IsPassive)
        {
            if (active)
            {
                AddPassiveEffect();
            }
            else
            {
                RemovePassiveEffect();
            }
        }
    }

    public virtual void SetupAbility()
    {
    }
}

/// <summary>
/// 0 - Mouse click default მაგიაა (ცეცხლის ბურთების სროლა, წყლის ბურთების სროლა და მსგავსი), მხოლოდ ერთი მაგია უნდა იყოს ეს, ერთმანეთს ჩაენაცვლება
/// 1 - Player-ზე მორგებული მაგია (ზომის შეცვლა, სიცოცხლის მომატება, სიჩქარე, დეში და მსგავსი), ეს შეიძლება ბევრი
/// 2 - Mouse click-ის მაგიაზე მორგებული (ცეცხლის ბურთის ზომის შეცვლა, სიჩქარის შეცვლა, ეფექტები ნასროლი მაგიის), ეს შეიძლება ბევრი
/// 3 - გარემოზე მორგებული მაგია (რენდომად ეცემოდეთ მტრებს მეხი ან ყველა მტერი ნელი იყოს ან მტრის ნასროლი ტყვიები იყოს ნელი ან რამე მსგავსი), ეს შეიძლება ბევრი
/// </summary>
public enum MagicType
{
    Default = 0,
    Player = 1,
    Bullet = 2,
    Environment = 3,
}
