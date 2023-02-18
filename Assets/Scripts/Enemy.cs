using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Transform Center;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(moveEnemy),5f, Time.deltaTime);
    }

    private void moveEnemy()
    {
        transform.RotateAround(Center.position, Vector3.up, velocity * Time.deltaTime);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
