using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3 (25, 0, 0);
    private float startDelay = 2;
    private float repeatDelay = 2;
    private PlayerController playerControllerScript;
    public List<GameObject> obstaclePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
        playerControllerScript =
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false) 
        {
            int randomObsticalIndex = Random.Range(0, obstaclePrefab.Length);
            Vector3 SpawnManager = spawnPos;
            Instantiate(obstaclePrefabs[randomObsticalIndex], spawnPos, obstaclePrefabs[randomObsticalIndex].transform.rotation);
        }
        
    }
}
