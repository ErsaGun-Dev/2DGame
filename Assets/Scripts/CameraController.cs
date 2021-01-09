using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    [Header("Character Component")]
    Character character;
    Transform Character;

    [Header("Character")]
    public Transform target;

    [Header("Camera Values")]
    public Vector3 offset;
    public float damping;

    [Header("Camera State")]
    public bool isFindCharacter = false;
    public bool isDirection = true;


    private void Start()
    {
        StartCoroutine(FindCharacter());
    }

    private void FixedUpdate()
    {
        if (isFindCharacter)
        {
            if (isDirection)
            {
                if (character.isFlipX)
                {
                    Vector3 offsetNegatif = new Vector3(-(offset.x), offset.y, offset.z);
                    Vector3 newOffset = Vector3.Lerp(offset, offsetNegatif, 1f);
                    transform.position = Vector3.Lerp(transform.position, target.position, damping) + newOffset;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, target.position, damping) + offset;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.position, damping) + offset;
            }
            
        }
        
    }

    IEnumerator FindCharacter()
    {
        yield return new WaitForSeconds(2.1f);
        Character = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("Find Character");
        target = Character;
        isFindCharacter = true;
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

    }
}
