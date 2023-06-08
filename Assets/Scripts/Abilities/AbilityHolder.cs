using System.Linq;
using UnityEngine;

public class AbilityHolder : MonoBehaviour, IObserver
{
    public AbilityBase[] Abilities;
    public Subject<bool> Subject;

    // Start is called before the first frame update
    void Start()
    {
        Subject.AddObserver(this);

        RestartAbilities();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ActivateRandomAbilities()
    {
        var randomMagicCount = Random.Range(1, Abilities.Length + 1);

        for (int i = 0; i < randomMagicCount; i++)
        {
            Abilities[Random.Range(0, Abilities.Length)].SetActive(true);
        }
    }

    void DisableAllAbilities()
    {
        foreach (var ability in Abilities)
        {
            ability.SetActive(false);
        }
    }

    public void RestartAbilities()
    {
        DisableAllAbilities();
        ActivateRandomAbilities();
    }

    public void Notify<T>(T data)
    {
        bool? boolValue = data as bool?;

        if (boolValue.HasValue)
        {
            if (!boolValue.Value)
            {
                DisableAllAbilities();
            }
            else
            {
                ActivateRandomAbilities();
            }
        }
    }
}
