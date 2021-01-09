using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [Header("Platform Prefabs")]
    public GameObject platformPrefab;
    public GameObject outingPlatfromPrefab;
    public GameObject landingPlatfromPrefab;
    public GameObject soilBumpPrefab;
    public GameObject soilPlatfromPrefab;

    [Header("Plant Spawner")]
    PlantSpawner plantspawner;

    [Header("Platforms Value")]
    public int platformCount;
     bool isOuting = false;

    [Header("Points Value")]
    public Vector3 lastEndPoint;
    public Vector3 spawnStartPoint;
    Vector3 lastStartPoint;

    public void Start()
    {
        plantspawner = GetComponent<PlantSpawner>();
        SpawnPlatforms();
    }

    public void SpawnPlatforms()
    {
        for (int i = 0; i < platformCount; i++)
        {
            GameObject platform = GameObject.Instantiate(platformPrefab);

            Platform platformScript = platform.GetComponent<Platform>();
            if (i == 0)
            {
                platform.transform.position = spawnStartPoint;
                int platformHeight = (int)platform.transform.position.y;
                for (int j = 0; j < platformHeight; j++)
                {

                    GameObject soilPlatform = GameObject.Instantiate(soilPlatfromPrefab);
                    soilPlatform.transform.position = new Vector3(platform.transform.position.x + 1, j, 0);
                }
            }
            else
            {
                lastStartPoint = lastEndPoint;
                platform.transform.position = lastEndPoint;
                int platformHeight = (int)platform.transform.position.y;
                
                for (int j = 0; j < platformHeight; j++)
                {

                    GameObject soilPlatform = GameObject.Instantiate(soilPlatfromPrefab);
                    soilPlatform.transform.position = new Vector3(platform.transform.position.x + 1, j, 0);
                }

            }

            lastEndPoint = platformScript.returnEndPoint();

            if (i != platformCount-1 && i != 0)
            {
                if ((i % 3) == 0)
                {
                    bool bumpPlatfrom = returnBumpPlatfrom();

                    if (bumpPlatfrom)
                    {
                        GameObject bumpPlatformObj;

                        if (isOuting)
                        {
                            bumpPlatformObj = GameObject.Instantiate(outingPlatfromPrefab);
                        }
                        else
                        {
                            bumpPlatformObj = GameObject.Instantiate(landingPlatfromPrefab);
                        }
                        BumpPlatform bumpScript = bumpPlatformObj.GetComponent<BumpPlatform>();
                        if (isOuting)
                        {
                            bumpPlatformObj.transform.position = new Vector3(lastEndPoint.x - 1, lastEndPoint.y + 1, lastEndPoint.z);
                            int bumpheight = (int)bumpPlatformObj.transform.position.y - 1;
                            for (int k = 0; k < bumpheight; k++)
                            {
                                GameObject soilbump = GameObject.Instantiate(soilBumpPrefab);
                                soilbump.transform.position = new Vector3(bumpPlatformObj.transform.position.x, k, 0);
                            }
                        }
                        else
                        {
                            bumpPlatformObj.transform.position = new Vector3(lastEndPoint.x - 2, lastEndPoint.y, lastEndPoint.z);
                            int bumpheight = (int)bumpPlatformObj.transform.position.y - 1;
                            for (int k = 0; k < bumpheight; k++)
                            {
                                GameObject soilbump = GameObject.Instantiate(soilBumpPrefab);
                                soilbump.transform.position = new Vector3(bumpPlatformObj.transform.position.x + 1, k, 0);
                            }
                        }

                        lastEndPoint = bumpScript.returnEndPoint();
                    }
                }
                else
                {
                    plantspawner.SpawnPlants(lastStartPoint, lastEndPoint);
                }
            }
        }

    }


    public bool returnBumpPlatfrom()
    {
        bool spawnBump = false;
        int RandomRange = Random.Range(1, 4);

        if (RandomRange == 2)
        {
            int boolRandomRange = Random.Range(1, 3);
            if (boolRandomRange == 1)
            {
                isOuting = false;
            }
            else
            {
                isOuting = true;
            }

            spawnBump = true;
        }

        return spawnBump;
    }

   
}
