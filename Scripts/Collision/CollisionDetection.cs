using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ProcessExit(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ProcessExit(collision.gameObject);
    }

    public virtual void ProcessCollision(GameObject collider)
    {
        /*Debug.Log(gameObject + " collided with " + collider);*/
    }

    public virtual void ProcessExit(GameObject collider)
    {
        /*Debug.Log(gameObject + " ended collision with " + collider);*/
    }
}
