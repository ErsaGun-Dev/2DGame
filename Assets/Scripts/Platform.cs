using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Platform Collider")]
    BoxCollider2D collider2d;

     void Awake()
    {
        collider2d = GetComponent<BoxCollider2D>();  
    }
    public Vector3 returnEndPoint()
    {
        Vector3 EndPoint;
        EndPoint.x = collider2d.bounds.size.x + this.transform.position.x;
        EndPoint.y = this.transform.position.y;
        EndPoint.z = 0;
        return EndPoint;
    }
}   
