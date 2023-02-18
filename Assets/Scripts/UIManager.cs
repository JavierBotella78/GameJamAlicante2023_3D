using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject diedMenu;
    [SerializeField] private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseMenuChangeState()
    {
        diedMenu.SetActive(!diedMenu.activeSelf);
    }

    private void diedMenuActivation()
    {
        diedMenu.SetActive(true);
    }

    public void goToMenu()
    {
        changeScene("Inicio");
    }

    public void replayScene()
    {
        changeScene(SceneManager.GetActiveScene().name);
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
