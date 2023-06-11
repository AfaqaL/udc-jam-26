using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public GameObject ShootingElement;
    public float Health = 150f;

    public HpBar hpBar;
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

    public void ReceiveDamage(float damage)
    {
        Health -= damage;
        hpBar.SetHealth(Health, 150);
        if (Health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
