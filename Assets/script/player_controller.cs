using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7;
    Rigidbody rb;
    GameManager gameManager;
    public int collectedCoins;
    public int maxHealth = 1 ;
    [SerializeField] int currentHealth ;

    private Camera mainCamera;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentHealth = maxHealth;
        gameManager.updatHealthText(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        jump();

    }

    public void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 moveDirection = cameraForward.normalized * verticalInput + cameraRight.normalized * horizontalInput;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

        }
    }

    public void jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("damge"))
        {
            Vector3 damgeDirection = other.transform.position - transform.position;
            damgeDirection.Normalize();
            rb.AddForce(-damgeDirection * 2, ForceMode.Impulse); 
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                gameManager.Restart();
            }
            gameManager.updatHealthText(currentHealth, maxHealth);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            collectedCoins += 1;
            gameManager.UpdatedCoinText(collectedCoins);

            Destroy(other.gameObject);
        }
    }

}
