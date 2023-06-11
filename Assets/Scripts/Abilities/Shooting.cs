using System;
using Enemies;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float Speed;

    public float damage = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * (Speed * Time.deltaTime));
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ReceiveDamage(damage);
        }
        Destroy(gameObject);
    }
}
