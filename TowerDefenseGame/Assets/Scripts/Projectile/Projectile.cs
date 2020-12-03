using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public Transform target;
    private Vector3 targetDirection;

    public void SetStaticDefaults()
    {
        target = gameObject.AddComponent<Transform>();
    }

    public void Create(Vector3 targetPosition)
    {
        targetDirection = targetPosition;
    }

    void Update()
    {
        Vector3 dir = targetDirection - transform.position;
        GetComponent<Rigidbody>().velocity = dir.normalized * speed * Time.deltaTime;

        //if (target)
        //{
        //    Vector3 dir = target.position - transform.position;
        //    GetComponent<Rigidbody>().velocity = dir.normalized * speed * Time.deltaTime;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}
