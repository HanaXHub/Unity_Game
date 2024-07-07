using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    public Transform[] targets;
    public int currentTarget;
    public float speed;

    private bool orientation;

    private void Start()
    {
        orientation = GetComponent<SpriteRenderer>().flipX;
    }

    private void Update()
    {
        checkTarget();
        transform.position = Vector3.MoveTowards(transform.position, targets[currentTarget].position, speed * Time.deltaTime);
    }

    private void checkTarget()
    {
        if (transform.position == targets[currentTarget].position)
        {
            if (currentTarget == targets.Length - 1) currentTarget = 0;
            else currentTarget++;

            GetComponent<SpriteRenderer>().flipX = !orientation;
            orientation = !orientation;
        }
    }
}
