using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public GameObject ShootingElement;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject GetShootingElement()
    {
        return ShootingElement;
    }
}
