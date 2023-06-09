using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public bool updates = true;
        
        private Vector2 targetLoc;
        private int laps = 0;

        private List<Transform> targets = new();
        private float _movementSpeed = 5f;
        private void Start()
        {
            
        }

        private void Update()
        {
            if (!updates) return;
            // Debug.Log("in update");
            if (targetLoc == (Vector2)transform.position)
            {
                targetLoc = MathUtil.GetRandomElement(targets).position;
                GetComponent<SpriteRenderer>().color = MathUtil.GetRandomColor();
                _movementSpeed = Random.Range(2f, 8f);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetLoc, _movementSpeed * Time.deltaTime);
            }
        }

        public void SetTargets(List<Transform> spawnPoints)
        {
            targets.AddRange(spawnPoints);
            targetLoc = MathUtil.GetRandomElement(targets).position;
        }
    }
}