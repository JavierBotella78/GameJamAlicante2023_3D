using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject diedMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;

    void Update()
    {
        inputPlayer();
    }

    private void inputPlayer()
    {
        checkGoToMenu();
    }

    private void checkGoToMenu()
    {
        if (!pauseMenu) { return; }
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.M))
        {
            GoToMenu();
        }
    }

    private void GoToMenu()
    {
        pauseMenuChangeState();
    }

    public void pauseMenuChangeState()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        int cosa = System.Convert.ToInt32(!pauseMenu.activeInHierarchy);
        Time.timeScale = (float)cosa;
    }

    public void controlsMenuChangeState()
    {
        controlsMenu.SetActive(!controlsMenu.activeSelf);
        
    }

    public void diedMenuActivation()
    {
        Time.timeScale = 0f;
        diedMenu.SetActive(true);
    }

    public void goToMenu()
    {
        Time.timeScale = 1f;
        changeScene("Inicio");
    }

    public void replayScene()
    {
        Time.timeScale = 1f;
        changeScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevel()
    {
        string[] parts = SceneManager.GetActiveScene().name.Split(" ");

        changeScene(parts[0] + " " + (int.Parse(parts[1])+1));

    }

    public void changeScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
