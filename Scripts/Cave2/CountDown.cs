using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CountDown : MonoBehaviour
{
    public float timer = 120f;
    public TextMeshProUGUI textForCountDown;
    public TextMeshProUGUI lostText;
    public GameObject specialBombEffect;
    public GameObject bomb;
    public float specialExplosionDuration = 3f;
    public float loadTime;
    
    void Start()
    {
        lostText.enabled = false;
        specialBombEffect.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            displayTimer();
            if (timer <= 10)
            {
                textForCountDown.color = Color.red;
            }
        }
        else
        {
            explosionAtEndOfCountDown();
        }
    }

    private void explosionAtEndOfCountDown()
    {
        Debug.Log("Explosion");
        if (specialBombEffect != null) { specialBombEffect.SetActive(true); }
        bomb.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GameObject.FindWithTag("Passenger"));
        Destroy(specialBombEffect, specialExplosionDuration);
        Destroy(bomb, specialExplosionDuration);
        loadTime -= Time.deltaTime;
        lostText.enabled = true;
        if (loadTime > 4)
        {
            SceneManager.LoadScene("Room");
        }
    }
    private void displayTimer()
    {
        float toMinutes = timer / 60;
        float toSeconds = timer % 60;
        textForCountDown.text = string.Format("{0:00}:{1:00}", toMinutes, toSeconds);
    }
}
