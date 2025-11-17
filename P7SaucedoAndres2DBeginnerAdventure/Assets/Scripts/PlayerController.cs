using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f; 
    public int maxHealth = 5;
    public float timeInvincible = 2;
    public int health { get { return currentHealth; } }
    int currentHealth; 

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public InputAction MoveAction;
  
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
        //QualitySettings,vSyncCount = 0; 
        //Application.targetFrameRate = 10;
        //Can monkeys do backfilps?

        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
        transform.position = position;

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false; 
            }

        }
    }
    void FixedUpdate()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
        transform.position = position;

        Vector2 postion = rigidbody2d.position;
        rigidbody2d.MovePosition(position);

    }
    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            if(isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
