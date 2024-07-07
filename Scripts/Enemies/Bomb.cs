using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : CollisionDetection
{
    public GameObject bombEffect;
    public float ExplosionDuration = 3f;
    public float radius = 2f;
    void Start()
    {
        bombEffect.SetActive(false);
    }
    public override void ProcessCollision(GameObject collision)
    {
        if(collision.transform.tag == "Player")
        {
            bombEffect.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(bombEffect, ExplosionDuration);
            Destroy(gameObject, ExplosionDuration);
            GameObject[] passengers = GameObject.FindGameObjectsWithTag("Passenger");
            foreach (GameObject pass in passengers)
            {
                if (pass.transform.position.y <= gameObject.transform.position.y + radius && pass.transform.position.y >= gameObject.transform.position.y - radius)
                {
                    if (pass.transform.position.x <= gameObject.transform.position.x + radius && pass.transform.position.x >= gameObject.transform.position.x - radius)
                        Destroy(pass);
                }

            }
        }
    }


}
