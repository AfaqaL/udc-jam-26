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
          _manager.StopEnemies();
          _firstTime = false;
     }

     public void PrepareProcesses()
     {
          
     }
}