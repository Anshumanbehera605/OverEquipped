using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    bool finished = false;
    public UIManager uIManager;
    public PropDestruction tree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tree.isBroken) GameLost();
    }

    public void GameWon()
    {
        if (finished) return;
        finished = true;
        uIManager.GameWon();
    }

    public void GameLost()
    {
        if (finished) return;
        finished = true;
        uIManager.GameLost();
    }
}
