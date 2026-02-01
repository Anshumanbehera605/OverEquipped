using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public PropDestruction tvProp;
    bool gameOver = false;
    bool gameWon = false;
    public UIManager uIManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tvProp == null && !gameOver) GameOver();
    }

    void GameOver()
    {
        if (gameWon) return;
        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        uIManager.GameLost();
        print("Gameover");
    }

    public void GameWon()
    {
        if (gameOver) return;
        gameWon = true;
        Cursor.lockState = CursorLockMode.None;
        uIManager.GameWon();
        print("gamewon");
    }
}
