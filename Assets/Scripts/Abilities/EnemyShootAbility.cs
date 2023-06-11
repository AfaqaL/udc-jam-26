using System;
using Enemies;
using UnityEngine;

namespace Abilities
{
    public class EnemyShootAbility : MonoBehaviour
    {
        public bool updates = true;

        public Transform playerTransform;
        public float speed;
        public float duration;
        public float expansionRate;

        protected float timeExisting = 0f;

        private RangedEnemy.AbilityRotation currRotation;

        private Rigidbody2D rb;

        private Vector2 direction;

        private Vector3 targetScale;

        public float damage;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (currRotation == RangedEnemy.AbilityRotation.SingleFire)
            {
                var dir = playerTransform.position - transform.position;
                rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;
            }
            else if (currRotation == RangedEnemy.AbilityRotation.CircularFire)
            {
                rb.velocity = direction * speed;
            }

            targetScale = transform.localScale * 3.2f;
        }

        public void Configure(Transform playerRefPosition, RangedEnemy.AbilityRotation currRotation,
            Vector2 dir = default)
        {
            playerTransform = playerRefPosition;
            this.currRotation = currRotation;
            switch (currRotation)
            {
                case RangedEnemy.AbilityRotation.SingleFire:
                    speed = 5f;
                    duration = 12f;
                    expansionRate = 1f;
                    damage = 15f;
                    break;
                case RangedEnemy.AbilityRotation.CircularFire:
                    speed = 3.6f;
                    duration = 7f;
                    expansionRate = 1f;
                    direction = dir;
                    damage = 12f;
                    break;
                case RangedEnemy.AbilityRotation.FollowFire:
                    speed = 3.2f;
                    duration = 6f;
                    expansionRate = 1.001f;
                    damage = 10f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currRotation), currRotation, null);
            }
        }

        private void Update()
        {
            if (!updates)
            {
                return;
            }


            Actions(
                (() => { }),
                (() =>
                {
                    if (playerTransform != null)
                    {
                        var dir = playerTransform.position - transform.position;
                        rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;
                        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 0.5f);
                    }
                }),
                (() => { })
            );

            timeExisting += Time.deltaTime;

            if (timeExisting >= duration)
            {
                Destroy(gameObject);
            }
        }

        public delegate void BehaviorAction();

        public void Actions(BehaviorAction single, BehaviorAction follow, BehaviorAction circular)
        {
            switch (currRotation)
            {
                case RangedEnemy.AbilityRotation.SingleFire:
                    single();
                    break;
                case RangedEnemy.AbilityRotation.CircularFire:
                    circular();
                    break;
                case RangedEnemy.AbilityRotation.FollowFire:
                    follow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currRotation), currRotation, null);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.ReceiveDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}