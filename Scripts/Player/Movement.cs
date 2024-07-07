using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    
    public float speed = 10.0f;
    public float dashSpeed = 20.0f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1.0f;

    private float dashTime;
    private float dashCooldownTime;
    public bool isDashing = false;

    public float knockDuration = 0.5f;

    private float knockTime;
    public bool isKnocked = false;

    public bool facingRight = true;
    private Vector3 direction = Vector3.zero; 

    public PostProcessVolume volum;
    private ChromaticAberration cr;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public GameObject attach;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Player = gameObject;

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        volum.profile.TryGetSettings<ChromaticAberration>(out cr);

        spawnPlayer(SceneManager.GetActiveScene().name);
        Debug.Log("Character Ready ");
    }

    // Update is called once per frame
    void Update()
    {
        //Determine speed based on if player is dashing or getting knocked
        //If Dashing or knocked decrease the timer until 0 then return to normal
        //Else take player's directional input and check for dash input
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0) Dash(false);
        }
        else if (isKnocked)
        {
            knockTime -= Time.deltaTime;
            if (knockTime <= 0) Knockback(false);
        }
        else
        {
            //Set Player's direction
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            direction = new Vector3(horizontal, vertical, 0);

            //Check if sprite needs to be flipped
            if (horizontal < 0 && facingRight
                || horizontal > 0 && !facingRight) flipChar();

            if (cr.intensity.value > 0.2f) cr.intensity.value -= Time.deltaTime;

            if (Input.GetButtonDown("Jump") && dashCooldownTime <= 0) Dash(true);

            // Decrease the dash cooldown time
            if (dashCooldownTime > 0) dashCooldownTime -= Time.deltaTime;
        }
        
    }

    void FixedUpdate()
    {
        if (isDashing) Move(dashSpeed);
        else Move(speed);     
    }

    private void spawnPlayer(string name)
    {
        if (name == "MainLevel") transform.position = GameManager.Instance.respawnPoint;
        else transform.position = Vector2.zero;
    }

    private void flipChar()
    {
        facingRight = !facingRight;
        attach.transform.Rotate(0, 180f, 0);
        sprite.flipX = !sprite.flipX;
    }

    private void Move(float s)
    {
        Vector2 position = rb.position;
        position.x = position.x + s * direction.x * Time.deltaTime;
        position.y = position.y + s * direction.y * Time.deltaTime;

        rb.MovePosition(position);
    }

    private void Dash(bool dash)
    {
        if(dash)
        {
            isDashing = true;
            dashTime = dashDuration;
            cr.intensity.value = 1.0f;
            transform.localScale = new Vector3(2.2f, 1.7f, 1f);
            if (facingRight) direction = new Vector3Int(1, 0, 0);
            else direction = new Vector3Int(-1, 0, 0);
        }
        else
        {
            isDashing = false;
            dashCooldownTime = dashCooldown;
            transform.localScale = new Vector3(2f, 2f, 1f);
        }
    }

    public void Knockback(bool knock)
    {
        if (knock)
        {
            isKnocked = true;
            knockTime = knockDuration;
            cr.intensity.value = 1.0f;
            sprite.color = new Color(1f, 1f, 1f, 0.5f);
            if (facingRight) direction = new Vector3Int(-1, 0, 0);
            else direction = new Vector3Int(1, 0, 0);
        }
        else
        {
            isKnocked = false;
            sprite.color = new Color(1f,1f,1f, 1f);
        }
    }
}
