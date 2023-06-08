using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities")]
public  class Ability : ScriptableObject
{
    public bool IsPassive;


    public virtual void Activate() {
    }
}

/// <summary>
/// 0 - Mouse click default მაგიაა (ცეცხლის ბურთების სროლა, წყლის ბურთების სროლა და მსგავსი)
/// 1 - Player-ზე მორგებული მაგია (ზომის შეცვლა, სიცოცხლის მომატება, სიჩქარე, დეში და მსგავსი)
/// 2 - Mouse click-ის მაგიაზე მორგებული (ცეცხლის ბურთის ზომის შეცვლა, სიჩქარის შეცვლა, ეფექტები ნასროლი მაგიის)
/// 3 - გარემოზე მორგებული მაგია (რენდომად ეცემოდეთ მტრებს მეხი ან ყველა მტერი ნელი იყოს ან მტრის ნასროლი ტყვიები იყოს ნელი ან რამე მსგავსი)
/// </summary>
public enum MagicType
{
    Default = 0,
    Player = 1,
    Bullet = 2,
    Environment = 3,
}
