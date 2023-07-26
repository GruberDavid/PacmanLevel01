using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Portal : MonoBehaviour
{
    public Transform connection;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = connection.position;
        position.z = collision.transform.position.z;
        collision.transform.position = position;
    }
}
