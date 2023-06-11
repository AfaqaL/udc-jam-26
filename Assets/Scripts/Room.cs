using System;
using UnityEngine;

public class Room : MonoBehaviour
{
     private bool _firstTime = true;

     [SerializeField]
     public Transform _spawnLocation;

     private EnemyManager _manager;
     
     private void Start()
     {
          _manager = GetComponent<EnemyManager>();
     }

     public void StopProcesses()
     {
          if (_manager == null) return;
          _manager.StopEnemies();
          _firstTime = false;
          _manager.enabled = false;
     }

     public void PrepareProcesses()
     {
          if (_manager == null) return;
          _manager.enabled = true;
          _manager.Continue();
     }
}