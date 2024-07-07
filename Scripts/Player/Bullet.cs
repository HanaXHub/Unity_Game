using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float decay = 1f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        StartCoroutine(bulletDecay());
    }

    //Destroys the bullet after some time
    IEnumerator bulletDecay()
    {
        yield return new WaitForSeconds(decay);
        Destroy(gameObject);
    }
}
