using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs;
    private float _spawnDelay = 2;
    private float _spawnInterval = 1.5f;

    private PlayerControllerX _playerControllerScript;

    private void Start()
    {
        InvokeRepeating("SpawnObjects", _spawnDelay, _spawnInterval);
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }
    
    private void SpawnObjects ()
    {        
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);
        
        if (_playerControllerScript._gameOver == false)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }

    }
}
