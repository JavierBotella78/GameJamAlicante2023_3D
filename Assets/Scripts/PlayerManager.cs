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
    public KeyCode jump;

    private bool floorSound = false;
    private bool isGrounded = false;

    [SerializeField]
    private float jumpForce = 10f;


    [SerializeField]
    private float initZ = -48f;
    [SerializeField]
    private float initX = 0f;

    [SerializeField]
    private float wheelSpeed = 20.0f;
    [SerializeField]
    private float bodySpeed = 4.0f;
    [SerializeField]
    private float bodyRotForce = 100.0f;
    [SerializeField]
    private float gravityRot = 140.0f;

    [SerializeField]
    private float accel = 5f;
    private float actualVelocity = 0f;

    [SerializeField]
    private float friction = 10f;
    [SerializeField]
    private float Offsetfriction = 20f;

    [SerializeField]
    private float MAX_SPEED = 20.0f;


    private Rigidbody rb;
    private float direction;
    private GameObject wheel;

    private GameObject bodyParent;

    public Transform center;
    public Transform parent;

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

        if (!wheel)
            wheel = gameObject.transform.Find("Wheel").gameObject;

        if (!bodyParent)
            bodyParent = gameObject.transform.Find("BodyParent").gameObject;

        if (!center)
            center = GameObject.Find("Centro").transform;

        if (!parent)
            parent = gameObject.transform.parent;

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
        checkGoToMenu();
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
        //transform.localPosition = new Vector3(initX, transform.position.y, initZ);

        if (isGrounded && MathF.Abs(actualVelocity) != 0)
            actualVelocity -= (friction + Offsetfriction) * Math.Sign(actualVelocity) * Time.deltaTime;

        if (MathF.Abs(actualVelocity) >= -0.2f && MathF.Abs(actualVelocity) <= 0.2f)
            actualVelocity = 0f;


        if (isGrounded && Input.GetKey(backward))
            actualVelocity += accel;

        if (isGrounded && Input.GetKey(forward))
            actualVelocity -= accel;

        if (isGrounded && Input.GetKey(jump))
        {
            rb.AddForce(transform.up * jumpForce);
            isGrounded = false;
        }



        if (Mathf.Abs(actualVelocity) > MAX_SPEED)
            actualVelocity = MAX_SPEED * Math.Sign(actualVelocity);


        parent.Rotate(Vector3.up * actualVelocity * Time.deltaTime, Space.Self);

        if (isGrounded)
        {
            wheel.transform.Rotate(0.0f, 0.0f, actualVelocity * wheelSpeed * Time.deltaTime, Space.Self);
            bodyParent.transform.Rotate(0.0f, 0.0f, -actualVelocity * bodySpeed * Time.deltaTime, Space.Self);
        }


        direction = Math.Sign(actualVelocity);

    }

    private void bodyRotation()
    {


        if (Input.GetKey(right))
        {
            bodyParent.transform.Rotate(0.0f, 0.0f, bodyRotForce * Time.deltaTime, Space.Self);
            //actualVelocity += accel / 4f;

        }

        if (Input.GetKey(left))
        {
            bodyParent.transform.Rotate(0.0f, 0.0f, -bodyRotForce * Time.deltaTime, Space.Self);
            //actualVelocity -= accel / 4f;
        }

        float rotzEuler = bodyParent.transform.rotation.eulerAngles.z;
        float rotz = bodyParent.transform.rotation.z;

        float gravityDirection = 0f;
        if (Math.Abs(rotzEuler) <= 90f && Math.Abs(rotzEuler) > 0f)
        {
            gravityDirection = 1f;
        }
        else if (Math.Abs(rotzEuler) < 360f && Math.Abs(rotzEuler) >= 270f)
        {
            gravityDirection = -1f;
        }

        bodyParent.transform.Rotate(0.0f, 0.0f, Math.Abs(rotz) * gravityDirection * gravityRot * Time.deltaTime, Space.Self);

        float roty = bodyParent.transform.rotation.eulerAngles.y;
        rotz = bodyParent.transform.rotation.eulerAngles.z;


        if (rotz >= 90.0f && rotz <= 100.0f)
        {
            bodyParent.transform.rotation = Quaternion.Euler(0.0f, roty, 90);

            if (floorSound)
            {
                OnChocarPlaySound?.Invoke();
                Offsetfriction = 100f;

                floorSound = false;
            }
            return;
        }

        if (rotz <= 270.0f && rotz >= 260.0f)
        {
            bodyParent.transform.rotation = Quaternion.Euler(0.0f, roty, -90);
            if (floorSound)
            {
                OnChocarPlaySound?.Invoke();
                Offsetfriction = 100f;

                floorSound = false;
            }
        }

        if ((rotz <= 80f && rotz >= 0f) || (rotz <= 360f && rotz >= 280f))
        {
            floorSound = true;
            Offsetfriction = 0f;
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
       /* if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.M))
        {
            GoToMenu();
        }*/
        if (Input.GetKey(KeyCode.I))
        {
            PlayerPrefs.SetFloat("Y", 0f);
        }
    }

    private void GoToMenu()
    {
        OnIrAlMenu?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 12)
        {
            OnDeathPlaySound?.Invoke();
            OnMeMuero?.Invoke();
        }

    }

}
