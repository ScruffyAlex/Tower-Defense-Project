using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    public Transform target;
    void Update()
    {
        if (target)
        {
            Vector3 dir = target.position - transform.position;
            GetComponent<Rigidbody>().velocity = dir.normalized * speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
