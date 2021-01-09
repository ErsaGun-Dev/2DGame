using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpPlatform : MonoBehaviour
{
    [Header("Collider")]
    PolygonCollider2D collider2d;

    [Header("Bump State")]
    public bool Outing;

    private void Awake()
    {
        collider2d = GetComponent<PolygonCollider2D>();
    }

    public Vector3 returnEndPoint()
    {
        Vector3 EndPoint;
      
        if (Outing)
        {
            EndPoint.x = (collider2d.bounds.size.x  + this.transform.position.x) +1;
            EndPoint.y = this.transform.position.y;
        }
        else
        {
            EndPoint.x = (collider2d.bounds.size.x * 2) + this.transform.position.x;
            EndPoint.y = this.transform.position.y  - 1;
        }
        
        EndPoint.z = 0;
        return EndPoint;
    }
}
