using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject respawnLight;
    
    public Vector2 setRespawn()
    {
        respawnLight.SetActive(true);
        return gameObject.transform.position;
    }
}
