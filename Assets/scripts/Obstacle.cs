using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Obstacle : MonoBehaviour
{
    public GameObject prefabToSpawn; // Drag your prefab here in the Unity Inspector
    public Vector3 spawnPoint; // Set the spawn point in the Unity Inspector

    private float spawnInterval = 5f;
    GameObject spawnedObject;
    private int spawnCount = 0;
    private List<AIObstacle> allSpawns = new List<AIObstacle>();
    public bool shouldSpawn = true;
    private bool isRunning;
    private float elapsedTime = 0f;

    void OnEnable()
    {
        isRunning = true;
        foreach (AIObstacle obj in allSpawns)
        {
            if (obj != null && obj.shouldUpdate)
                obj.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= spawnInterval)
            {
                SpawnObject();
                elapsedTime -= spawnInterval; // Keep the remaining time
            }
        }
    }
    private void OnDisable()
    {
        isRunning = false;
        foreach (AIObstacle obj in allSpawns)
        {
            if(obj != null)
            obj.gameObject.SetActive(false);
        }
    }

    void SpawnObject()
    {
        if(shouldSpawn && spawnCount < 6)
        {
            if(spawnCount < 6)
            {
                spawnCount++;
                spawnedObject = Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
                spawnedObject.GetComponent<AIObstacle>().AI = GameObject.FindGameObjectWithTag("NPC");
                allSpawns.Add(spawnedObject.ConvertTo<AIObstacle>());
            }
            else
            {
                shouldSpawn = false;
            }
        }

    }

}
