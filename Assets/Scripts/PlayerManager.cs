using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode right;
    public KeyCode left;

    [SerializeField]
    private float speed = 30.0f;
    private float wheelSpeed = 2.0f;

    [SerializeField]
    private float MAX_SPEED = 10.0f;


    private Rigidbody rb;
    private float direction;
    private GameObject wheel;
    private GameObject bodyParent;

    public Action OnRestartLevel;
    public Action OnNextLevel;
    public Action OnDeathPlaySound;
    public Action OnChocarPlaySound;
    public Action OnIrAlMenu;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheel = GameObject.Find("Wheel");
        bodyParent = GameObject.Find("BodyParent");
    }

    // Update is called once per frame
    void Update()
    {
        inputPlayer();
    }

    private void FixedUpdate()
    {
        playerMovement();
    }





    private void inputPlayer()
    {
        checkRestartButtonPressed();
        checkNextLevelButtonPressed();
    }

    private void playerMovement()
    {
        if (Input.GetKey(backward))
        {
            rb.AddForce(-transform.right * speed);
            direction = -1.0f;
        }

        if (Input.GetKey(forward))
        {
            rb.AddForce(transform.right * speed);
            direction = 1.0f;
        }

        if (rb.velocity.magnitude > MAX_SPEED)
            rb.velocity = transform.right * MAX_SPEED * direction;

        wheel.transform.Rotate(0.0f, 0.0f, rb.velocity.sqrMagnitude * wheelSpeed * Time.deltaTime * -direction, Space.Self);
    }

    private void checkNextLevelButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.L))
            OnNextLevel.Invoke();
    }

    private void checkRestartButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.R))
            OnRestartLevel.Invoke();
    }

    private void checkGoToMenu()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.M))
        {
            GoToMenu();
        }
    }

    private void GoToMenu()
    {
        OnIrAlMenu?.Invoke();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Deadly"))
        {
            OnDeathPlaySound?.Invoke();
        }
    }
}
