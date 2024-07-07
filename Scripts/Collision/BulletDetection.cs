using System.Collections;
using UnityEngine;

public class BulletDetection : CollisionDetection
{
    public override void ProcessCollision(GameObject collider)
    {
        //Explode and destroy object it hit
        if (collider.CompareTag("Breakable1") || collider.CompareTag("Breakable2") || collider.CompareTag("Enemy"))
        {
            collider.GetComponent<DestroySelf>().destroy();
            GetComponent<DestroySelf>().destroy();
        }
        //Explode if hit the level
        else if (collider.CompareTag("Terrain"))
        {
            GetComponent<DestroySelf>().destroy();
        }
    }
}
