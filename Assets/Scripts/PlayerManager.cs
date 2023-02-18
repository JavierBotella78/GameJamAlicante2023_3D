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
    private float wheelSpeed = 4.0f;
    private float bodySpeed = 1.0f;
    private float bodyRotForce = 50.0f;
    private float gravityRot = 30.0f;

    [SerializeField]
    private float MAX_SPEED = 10.0f;


    private Rigidbody rb;
    private float direction;
    private GameObject wheel;
    private GameObject bodyParent;

    public Transform center;

    public Action OnRestartLevel;
    public Action OnNextLevel;
    public Action OnDeathPlaySound;
    public Action OnChocarPlaySound;
    public Action OnIrAlMenu;
    public Action OnMeMuero;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(!wheel)
            wheel = gameObject.transform.Find("Wheel").gameObject;
        
        if(!bodyParent)
            bodyParent = gameObject.transform.Find("BodyParent").gameObject;

        if(!center)
            center = GameObject.Find("Centro").transform;

        //transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        inputPlayer();
    }

    private void FixedUpdate()
    {
        playerRotation();
        playerMovement();
        bodyRotation();
    }





    private void inputPlayer()
    {
        checkRestartButtonPressed();
        checkNextLevelButtonPressed();
    }

    private void playerRotation()
    {
        transform.LookAt(center);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
        /*
        Vector2 offset = new Vector2(center.position.x - transform.position.x, center.position.z - transform.position.z);

        float rad = 0f;

        if(offset.x != 0f)
        {
            rad = Mathf.Atan(offset.y / offset.x);
            if(offset.x < 0f)
            {
                rad += Mathf.PI;
            }
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rad * Mathf.Rad2Deg, transform.eulerAngles.z);

        Debug.DrawLine(center.position, transform.position, Color.red);
        */
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
        bodyParent.transform.Rotate(0.0f, 0.0f, rb.velocity.sqrMagnitude * bodySpeed * Time.deltaTime * direction, Space.Self);
    }

    private void bodyRotation()
    {
        if (Input.GetKey(right))
        {
            bodyParent.transform.Rotate(0.0f, 0.0f, bodyRotForce * Time.deltaTime, Space.Self);

        }

        if (Input.GetKey(left))
        {
            bodyParent.transform.Rotate(0.0f, 0.0f, -bodyRotForce * Time.deltaTime, Space.Self);
        }

        float rotz = bodyParent.transform.rotation.z;

        bodyParent.transform.Rotate(0.0f, 0.0f, rotz * 100.0f * Time.deltaTime, Space.Self);

        float roty = bodyParent.transform.rotation.eulerAngles.y;
        rotz = bodyParent.transform.rotation.eulerAngles.z;





        if (rotz >= 90.0f && rotz <= 100.0f)
        {
            bodyParent.transform.rotation = Quaternion.Euler(0.0f, roty, 90);
            return;
        }

        if (rotz <= 270.0f && rotz >= 260.0f)
        {
            bodyParent.transform.rotation = Quaternion.Euler(0.0f, roty, -90);

        }


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
        if (collision.gameObject.layer == 9)
        {
            OnDeathPlaySound?.Invoke();
            OnMeMuero?.Invoke();
        }
    }

}
