using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameWon;
    public GameObject gameOver;
    public GameObject instruction;
    public GameObject loreScreen;
    public int currentLevel;
    public String hintText;
    public TextMeshProUGUI hintBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameWon.SetActive(false);
        gameOver.SetActive(false);
        instruction.SetActive(false);
        loreScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            hintBox.text = hintText;
        }
        if (Input.GetKey(KeyCode.E))
        {
            loreScreen.SetActive(false);
            instruction.SetActive(true);
        }
    }

    public void GameWon()
    {
        gameWon.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        instruction.SetActive(false);
    }

    public void GameLost()
    {
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        instruction.SetActive(false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadSceneAsync("Level"+currentLevel.ToString());
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("StartScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
