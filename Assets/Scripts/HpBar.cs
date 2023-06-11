using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public Transform foreground;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetHealth(float health, float maxHealth)
    {
        foreground.localScale = new Vector2(health/maxHealth, foreground.localScale.y);
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }
}
