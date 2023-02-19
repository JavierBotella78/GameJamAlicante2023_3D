using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public GameObject player;
    public float direction = 1f;

    public float force = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay(Collider col)
    {
        player.transform.Rotate(Vector3.up * force * Time.deltaTime * direction);
    }
}
