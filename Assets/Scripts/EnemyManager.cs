using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private bool _isBossRoom;

    private List<Enemy> _enemies = new List<Enemy>();

    [SerializeField]
    private BossEnemy _enemy = null;
}