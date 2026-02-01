using TMPro;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    public float timeLimit;
    bool gameWon = false;
    bool gameLost = false;
    public UIManager uIManager;
    public TextMeshProUGUI timerText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0)
        {
            GameLost();
            timeLimit = 0;
        }

        timerText.text = ((int)(timeLimit*10)/10).ToString()+"s";
    }

    public void GameWon()
    {
        if (gameLost || gameWon) return;
        gameWon = true;
        uIManager.GameWon();
        print("Game Won");
    }

    public void GameLost()
    {
        if (gameLost || gameWon) return;
        gameLost = true;
        uIManager.GameLost();
        print("Game Lost");
    }
}
