using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public bool updates = true;

        private Vector2 targetLoc;
        private int laps = 0;

        public float health = 0f;
        public float MaxHealth = 30;

        protected List<Transform> targets = new();
        protected float _movementSpeed = 3.2f;

        protected NavMeshAgent _agent;

        public HpBar hpBar;

        public Transform playerRef;
        public Transform projectileHolder;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.speed = Random.Range(2.4f, 3.2f);
            health = MaxHealth;

        }

        private void Update()
        {
            if (!updates)
            {
                _agent.SetDestination(transform.position);
                return;
            }

            MoveToNextPoint();
        }

        protected void MoveToNextPoint()
        {
            if (Vector2.Distance(targetLoc, transform.position) < 1f)
            {
                targetLoc = MathUtil.GetRandomElement(targets).position;
            }
            else
            {
                _agent.SetDestination(new Vector3(
                    targetLoc.x, targetLoc.y, transform.position.z
                ));
            }
        }

        public void SetTargets(List<Transform> spawnPoints)
        {
            targets.AddRange(spawnPoints);
            targetLoc = MathUtil.GetRandomElement(targets).position;
        }

        public virtual void ReceiveDamage(float damage)
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