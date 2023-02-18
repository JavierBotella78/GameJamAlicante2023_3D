using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject diedMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void diedMenuActivation()
    {
        diedMenu.SetActive(true);
    }

    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
