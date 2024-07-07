using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public GameObject deathEffect;
    public bool canRespawn;
    public float respawnDelay = 0f;

    public void destroy()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);

        if (canRespawn)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().respawnObject(respawnDelay, gameObject);
            gameObject.SetActive(false);
        }
        else Destroy(gameObject);
    }
}
