using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float timeBetweenCoins = 2f;
    private float startDelay = 1f;
    
    [SerializeField] private GameObject[] coinsArray;

    private float spawnRangeX = 8f;
    private float spawnRangeZ = 5f;

    private Player_Controller playerControllerScript;



    void Start()
    {
       playerControllerScript = FindObjectOfType<Player_Controller>();
        
        InvokeRepeating("InstantiateRandomCoin", startDelay, timeBetweenCoins);
    }

 
    void Update()
    {
        if (playerControllerScript.isGameOver) 
        {
            CancelInvoke("InstantiateRandomCoin");
        }
    }

    private void InstantiateRandomCoin() 
    {
        int randomIndex = Random.Range(0, coinsArray.Length);
        Instantiate(coinsArray[randomIndex], RandomSpawnPos(), Quaternion.identity);
    }

    private Vector3 RandomSpawnPos()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        return new Vector3(randomX, 0, randomZ );

    }

}
