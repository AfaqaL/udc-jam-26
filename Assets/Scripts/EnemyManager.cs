using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Enemies;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private bool _isBossRoom;

    [SerializeField]
    private List<Enemy> _enemies;

    [SerializeField]
    private BossEnemy _enemy = null;

    private bool _isInitial = true;

    [SerializeField] private List<Transform> spawnPoints;

    private List<Enemy> _enemyInstances = new();

    private GameObject player;

    public Transform projectileHolder;

    public int spawnCount = 9;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        if (_isInitial)
        {
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            ContinueCombat();
        }
    }

    private void OnDisable()
    {
        if (_isBossRoom)
        {
            
        }
        else
        {
            foreach (var enemy in _enemyInstances)
            {
                enemy.updates = false;
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        if (_isBossRoom)
        {
            // do boss initialization
        }
        else
        {
            while (spawnCount < 60)
            {
                var enemy = MathUtil.GetRandomElement(_enemies);
                var spawnPoint = MathUtil.GetRandomElement(spawnPoints);
                var instance = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                instance.SetTargets(spawnPoints);
                _enemyInstances.Add(instance);
                instance.playerRef = player.transform;
                instance.projectileHolder = projectileHolder;
                spawnCount++;
                float min, max;
                if (spawnCount < 8)
                {
                    min = 0.5f;
                    max = 1.5f;
                } else if (spawnCount < 15)
                {
                    min = 2f;
                    max = 3.5f;
                }
                else
                {
                    min = 5f;
                    max = 8f;
                }
                yield return new WaitForSeconds(Random.Range(min, max));
            }
        }
    }

    private void ContinueCombat()
    {
        
    }

    public void StopEnemies()
    {
        foreach (var enemyInstance in _enemyInstances)
        {
            enemyInstance.updates = false;
            var proj = projectileHolder.GetComponentsInChildren<EnemyShootAbility>();
            foreach (var enemyShootAbility in proj)
            {
                enemyShootAbility.updates = false;
            }
        }
    }

    public void Continue()
    {
        foreach (var enemyInstance in _enemyInstances)
        {
            enemyInstance.updates = true;
            var proj = projectileHolder.GetComponentsInChildren<EnemyShootAbility>();
            foreach (var enemyShootAbility in proj)
            {
                enemyShootAbility.updates = false;
            }
        }
    }
}