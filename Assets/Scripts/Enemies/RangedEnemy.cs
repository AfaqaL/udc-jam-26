using System;
using System.Collections.Generic;
using Abilities;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class RangedEnemy : Enemy
    {
        private List<EnemyShootAbility> abilities = new();

        public enum AbilityRotation
        {
            SingleFire,
            FollowFire, // slow projectile that follows for duration
            CircularFire, // shoots projectiles around
        }

        private List<Vector2> directions = new()
        {
            Vector2.down, Vector2.left, Vector2.right, Vector2.up,
            Vector2.down + Vector2.left,
            Vector2.down + Vector2.right,
            Vector2.up + Vector2.left,
            Vector2.up + Vector2.right,
        };

        public EnemyShootAbility abilityPrefab; 


        public float behaviorChangeInterval = 3f;
        public float behaviorChangeRange = 0.1f;
        public float currBehaviorChangeTime;
        public float timeSingeLastBehaviorChange = 0f;

        public AbilityRotation currBehavior;

        public float fireRate = 0.7f;
        public float timeSinceLastFire = 10f;

        // Start is called before the first frame update
        void Start()
        {
            currBehavior = GetRandomRotation();
            // currBehavior = AbilityRotation.SingleFire;
            currBehaviorChangeTime = GetBehaviorChangeTime();

            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            health = MaxHealth;
        }

        public float GetBehaviorChangeTime()
        {
            return Random.Range(behaviorChangeInterval - behaviorChangeRange,
                behaviorChangeInterval + behaviorChangeRange);
        }

        // Update is called once per frame
        void Update()
        {
            if (!updates)
            {
                _agent.SetDestination(transform.position);
                return;
            }

            MoveToNextPoint();
            HandleFire();
        }

        private void HandleFire()
        {
            timeSinceLastFire += Time.deltaTime;
            timeSingeLastBehaviorChange += Time.deltaTime;
            if (timeSingeLastBehaviorChange >= currBehaviorChangeTime)
            {
                currBehavior = GetRandomRotation();
                // currBehavior = AbilityRotation.FollowFire;
                timeSingeLastBehaviorChange = 0f;
                Debug.Log(currBehavior.ToString());
                fireRate = GetFireRate();
            }
            if (timeSinceLastFire < fireRate)
            {
                return;
            }
            timeSinceLastFire = 0f;

            switch (currBehavior)
            {
                case AbilityRotation.SingleFire:
                    SingleFireShot();
                    break;
                    ;
                case AbilityRotation.FollowFire:
                    FollowFireShot();
                    break;
                case AbilityRotation.CircularFire:
                    CircularFireShot();
                    break;
            }
        }

        private float GetFireRate()
        {
            switch (currBehavior)
            {
                case AbilityRotation.FollowFire:
                    return 0.9f;
                case AbilityRotation.SingleFire:
                    return 0.65f;
                case AbilityRotation.CircularFire:
                    return 1.02f;
            }

            return 0.75f;
        }

        protected virtual void CircularFireShot()
        {
            foreach (var direction in directions)
            {
                var instance = Instantiate(abilityPrefab, projectileHolder);
                instance.transform.position = transform.position;
                instance.Configure(playerRef, AbilityRotation.CircularFire, direction.normalized);
            }
        }

        protected virtual void FollowFireShot()
        {
            var instance = Instantiate(abilityPrefab, projectileHolder);
            instance.transform.position = transform.position;
            instance.Configure(playerRef, AbilityRotation.FollowFire);
        }

        protected virtual void SingleFireShot()
        {
            var instance = Instantiate(abilityPrefab, projectileHolder);
            instance.transform.position = transform.position;
            instance.Configure(playerRef, AbilityRotation.SingleFire);
        }


        private AbilityRotation GetRandomRotation()
        {
            int val = Random.Range(0, 12);
            if (val < 6)
            {
                return AbilityRotation.SingleFire;
            }

            if (val < 10)
            {
                return AbilityRotation.FollowFire;
            }

            return AbilityRotation.CircularFire;
        }

        public override void ReceiveDamage(float damage)
        {
            health -= damage;
            hpBar.SetHealth(health, MaxHealth);
            if (health <= 0f)
            {
                _agent.isStopped = true;
                Destroy(gameObject);
            }
        }
    }
}