using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    public float timeLimit;
    bool gameWon = false;
    bool gameLost = false;
    
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
        }
    }

    public void GameWon()
    {
        if (gameLost || gameWon) return;
        gameWon = true;
        print("Game Won");
    }

    public void GameLost()
    {
        if (gameLost || gameWon) return;
        gameLost = true;
        print("Game Lost");
    }
}
