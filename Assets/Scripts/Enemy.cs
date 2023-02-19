using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private Transform Center;
    [SerializeField] private List<Material> frames;
    private int actualFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(moveEnemy),5f, Time.deltaTime);
        InvokeRepeating(nameof(changeMaterial), 5f, 0.2f);

    }

    private void changeMaterial()
    {
        myRenderer.material = frames[actualFrame];
        actualFrame++;
        if (actualFrame > frames.Count - 1)
            actualFrame = 0;
    }

    private void moveEnemy()
    {
        Center.Rotate(Vector3.up, velocity * Time.deltaTime);

    }
}
