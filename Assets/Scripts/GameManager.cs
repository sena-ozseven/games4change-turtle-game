using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject turtlePrefab;
    
    [Header("Spawn Settings")]
    public Transform[] spawnPoints; //nest empty objects will be there
    public float spawnInterval;


    void Start()
    {
        StartCoroutine(SpawnTurtlesRoutine());
    }

    IEnumerator SpawnTurtlesRoutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(spawnInterval); 
            SpawnTurtle();
        }
    }

    void SpawnTurtle()
    {
        if (turtlePrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Turtle Prefab veya Spawn Points atanmamış! Lütfen GameManager objesini kontrol et.");
            return;
        }

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        Instantiate(turtlePrefab, spawnPoint.position, spawnPoint.rotation);
        
        Debug.Log(spawnPoint.name + " isimli yuvada yeni bir kaplumbağa yaratıldı.");
    }
}