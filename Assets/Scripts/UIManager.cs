using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject diedMenu;
    [SerializeField] private GameObject pauseMenu;

    public void pauseMenuChangeState()
    {
        pauseMenu.SetActive(!diedMenu.activeSelf);
    }

    public void diedMenuActivation()
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

    public void nextLevel()
    {
        string[] parts = SceneManager.GetActiveScene().name.Split(" ");

        changeScene(parts[0] + (int.Parse(parts[1])+1));

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
