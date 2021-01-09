using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    [Header("Plant Prefabs")]
    public GameObject rockPrefab;
    public GameObject bushPrefab;
    public GameObject treePrefab;
    GameObject Plant;
    GameObject emptyPlant;

    [Header("Plant Generation Count")]
    public int rockCount;
    public int bushCount;
    public int treeCount;

    [Header("Platform Spawner")]
    PlatformSpawner platformSpawner;

    private void Awake()
    {
        platformSpawner = GetComponent<PlatformSpawner>();
    }

    public void SpawnPlants(Vector3 platformStartPosition, Vector3 platformEndPosition)
    {
        PlantCountControl();
        float plantX = Random.Range(platformStartPosition.x, platformEndPosition.x);
        float plantY = platformStartPosition.y;
        float plantZ = 0f;

        GameObject Prefab = randomPrefab();
   
       if(Prefab == bushPrefab)
        {
            GameObject plantObj = GameObject.Instantiate(Prefab);
            plantObj.transform.position = new Vector3(plantX, plantY + 1.2f, plantZ);
        }
        else if(Prefab != null)
        {
            GameObject plantObj = GameObject.Instantiate(Prefab);
            plantObj.transform.position = new Vector3(plantX, plantY + 0.5f, plantZ);
        }
        else{
            
        }
        
    }

    public GameObject randomPrefab()
    {
        int randomPlantValue = Random.Range(1, 4);
        if(treeCount == 0 && rockCount == 0 && bushCount == 0)
        {
            Plant = null;
        }else if (treeCount == 0)
        {
            if(rockCount != 0)
            {
                if(bushCount != 0)
                {
                    randomPlantValue = Random.Range(1, 3);
                    if(randomPlantValue == 1)
                    {
                        Plant = bushPrefab;
                        bushCount -= 1;
                    }
                    else {
                        Plant = rockPrefab;
                        rockCount -= 1;
                    }
                }
                else
                {
                    Plant = rockPrefab;
                    rockCount -= 1;
                }
            }
            else
            {
                Plant = bushPrefab;
                bushCount -= 1;
            }
        }
        else if (bushCount == 0)
        {
            if (rockCount != 0)
            {
                if (treeCount != 0)
                {
                    randomPlantValue = Random.Range(1, 3);
                    if (randomPlantValue == 1)
                    {
                        Plant = treePrefab;
                        treeCount -= 1;
                    }
                    else
                    {
                        Plant = rockPrefab;
                        rockCount -= 1;
                    }
                }
                else
                {
                    Plant = rockPrefab;
                    rockCount -= 1;
                }
            }
            else
            {
                Plant = treePrefab;
                treeCount -= 1;
            }
        }
        else if (rockCount == 0)
        {
            if (treeCount != 0)
            {
                if (bushCount != 0)
                {
                    randomPlantValue = Random.Range(1, 3);
                    if (randomPlantValue == 1)
                    {
                        Plant = treePrefab;
                        treeCount -= 1;
                    }
                    else
                    {
                        Plant = bushPrefab;
                        bushCount -= 1;
                    }
                }
                else
                {
                    Plant = treePrefab;
                    treeCount -= 1;
                }
            }
            else
            {
                Plant = bushPrefab;
                bushCount -= 1;
            }
        }
        else
        {
            if(randomPlantValue == 1)
            {
                Plant = bushPrefab;
                bushCount -= 1;
            }
            else if(randomPlantValue == 2)
            {
                Plant = treePrefab;
                treeCount -= 1;
            }
            else if (randomPlantValue == 3)
            {
                Plant = rockPrefab;
                rockCount -= 1;
            }
        }

        return Plant;
    }
    public void PlantCountControl()
    {
        int plantCount = bushCount + rockCount + treeCount;
        while(plantCount > platformSpawner.platformCount)
        {
            bushCount -= 1;
            treeCount -= 1;
            rockCount -= 1;
            plantCount = bushCount + rockCount + treeCount;
        }
    }

}
