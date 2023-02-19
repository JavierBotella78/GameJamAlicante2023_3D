using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]  private GameObject player1;
    [SerializeField]  private GameObject player2;
    [SerializeField] private GameObject enemy;



    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat("Y") == 0f)
        {
            PlayerPrefs.SetFloat("Y", 90f);
        }
        player1.transform.rotation = Quaternion.Euler(0, PlayerPrefs.GetFloat("Y"), 0);
        player2.transform.rotation = Quaternion.Euler(0, PlayerPrefs.GetFloat("Y") - 5f, 0);
        enemy.transform.rotation = Quaternion.Euler(0, PlayerPrefs.GetFloat("Y") + 12f, 0);

    }

}
