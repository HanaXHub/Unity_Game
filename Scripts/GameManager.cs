using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public int healthMax;
    public int healthCurrent;

    public float oxygenMax;
    public float oxygenCurrent;
    public bool losingOxygen;

    public int passengers;
    public int moneyCurrent;

    private int savedMoney;
    private int savedPassengers;

    public int level;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI oxygenText;
    [SerializeField] private TextMeshProUGUI passengerText;
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject hitEffect;

    public GameObject retryMenu;

    public Vector2 respawnPoint { get; set; }

    private void Singleton()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        Singleton();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        oxygenUpdate();
        updateUI();
    }

    //---------------------------------------------------------
    //Player stats functions
    private void oxygenUpdate()
    {
        if (losingOxygen) oxygenCurrent -= Time.deltaTime * 0.5f;
        if (oxygenCurrent < 0) playerDeath();
    }

    //Save details and set respawn
    public void respawnSet(Vector2 respawn)
    {
        respawnPoint = respawn;
        savedMoney = moneyCurrent;
        savedPassengers = passengers;
    }

    //Restart Level
    public void retry()
    {
        healthCurrent = healthMax;
        oxygenCurrent = oxygenMax;
        moneyCurrent = savedMoney;
        passengers = savedPassengers;
    }

    //Decrease health, activate knockback, kill if health = 0
    public void takeDamage(int damage)
    {
        healthCurrent -= damage;
        if (healthCurrent == 0) playerDeath();
        else Instantiate(hitEffect, Player.transform.position, Player.transform.rotation);
        Debug.Log("Hit by Enemy");
    }

    //Disable camera, player, open retry menu
    public void playerDeath()
    {
        GameObject.FindGameObjectWithTag("Cinemachine").SetActive(false);
        Player.GetComponent<Movement>().enabled = false;
        Player.GetComponent<Weapon>().enabled = false;
        Player.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(deathEffect, Player.transform.position, Player.transform.rotation);
        Time.timeScale = 0.2f;
        retryMenu.SetActive(true);
        StopAllCoroutines();
        Debug.Log("Game over");
    }

    //---------------------------------------------------------
    //Pickup functions
    public void oxygenUp()
    {
        oxygenCurrent = oxygenMax;
    }

    public void healthUp()
    {
        if (healthCurrent != healthMax) healthCurrent++;
    }

    public void moneyUp()
    {
        moneyCurrent += 500;
    }

    public void passengerUp() 
    {
        passengers++;
    }

    //---------------------------------------------------------
    //City functions
    public bool makePurchase(int purchase)
    {
        if (moneyCurrent > purchase)
        {
            moneyCurrent -= purchase;
            return true;
        }
        return false;
    }

    public void dropPassengers()
    {
        moneyCurrent += passengers * 1000;
        passengers = 0;
    }

    //---------------------------------------------------------
    //Change UI Text
    private void updateUI()
    {
        healthText.text = "Health: " + healthCurrent + "/" + healthMax;
        oxygenText.text = "Oxygen: " + Mathf.CeilToInt(oxygenCurrent * 10);
        passengerText.text = "Passengers: " + passengers;
        moneyText.text = "Money: " + moneyCurrent;
    }

    //---------------------------------------------------------
    //Make object active again after certain time
    public void respawnObject(float time, GameObject obj)
    {
        StartCoroutine(respObj(time, obj));
    }

    IEnumerator respObj(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(true);
    }
}

