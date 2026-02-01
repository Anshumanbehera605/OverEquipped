using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject buttonParent;
    public GameObject levelParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            levelParent.SetActive(false);
            buttonParent.SetActive(true);
        }
    }

    public void StartGame()
    {
        levelParent.SetActive(true);
        buttonParent.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadSceneAsync("Level" + level.ToString());
    }
}
