using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : CollisionDetection
{
    Movement movement;
    private void Start()
    {
        movement = GetComponent<Movement>();
    }
    public override void ProcessCollision(GameObject collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (!movement.isDashing && !movement.isKnocked)
            {
                GameManager.Instance.takeDamage(1);
                movement.Knockback(true);
            }
        }

        if (collider.CompareTag("Breakable1"))
        {
            if (movement.isDashing) 
                collider.GetComponent<DestroySelf>().destroy();
        }

        if (collider.CompareTag("PickupH"))
        {
            collider.GetComponent<DestroySelf>().destroy();
            GameManager.Instance.healthUp(); 
        }

        if (collider.CompareTag("PickupO"))
        {
            collider.GetComponent<DestroySelf>().destroy();
            GameManager.Instance.oxygenUp();
        }

        if (collider.CompareTag("PickupM"))
        {
            collider.GetComponent<DestroySelf>().destroy();
            GameManager.Instance.moneyUp();
        }

        if (collider.CompareTag("Passenger"))
        {
            collider.GetComponent<DestroySelf>().destroy();
            GameManager.Instance.passengerUp();
        }

        if (collider.CompareTag("Entrance"))
        {
            if (collider.name != "MainLevel") 
                GameManager.Instance.respawnPoint = collider.transform.position;
            SceneManager.LoadScene(collider.name);
        }

        if (collider.CompareTag("Respawn"))
        {
            GameManager.Instance.respawnSet(collider.GetComponent<Respawn>().setRespawn());
        }
    }
}
