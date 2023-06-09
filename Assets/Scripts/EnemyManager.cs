using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
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
            foreach (var enemy in _enemies)
            {
                var spawnPoint = MathUtil.GetRandomElement(spawnPoints);
                var instance = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                instance.SetTargets(spawnPoints);
                _enemyInstances.Add(instance);
                yield return new WaitForSeconds(Random.Range(0f, 1.5f));
            }
        }
    }

    private void ContinueCombat()
    {
        
    }

    public void StopEnemies()
    {
        
    }
}