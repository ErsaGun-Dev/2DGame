using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject PlatformSpawner;
    public GameObject character;
    GameObject Character;

    [Header("Platform Spawner Value")]
    public Vector3 platfromStartPosition;
    public Vector3 platfromEndPosition;
    PlatformSpawner platformSpawner;

    void Start()
    {
        platformSpawner = PlatformSpawner.GetComponent<PlatformSpawner>();
        platfromStartPosition = platformSpawner.spawnStartPoint;
        platfromEndPosition = platformSpawner.lastEndPoint;
        StartCoroutine(CharacterSpawnTimer());
    }
     public IEnumerator CharacterSpawnTimer()
    {
        float SpawX = Random.Range(platfromStartPosition.x, platfromEndPosition.x);
        float SpawY = Random.Range(platfromStartPosition.y, platfromEndPosition.y);
        float SpawZ = Random.Range(platfromStartPosition.z, platfromEndPosition.z);

        yield return new WaitForSeconds(2.0f);

        GameObject Character = GameObject.Instantiate(character);
        Character.transform.position = new Vector3(SpawX, SpawY + 5, SpawZ);
        Debug.Log("Character Spawned");
    }

    
}
