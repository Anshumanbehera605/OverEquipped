using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : MonoBehaviour
{
    public UIManager uIManager;
    public List<PropDestruction> vehicles;
    bool finished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PropDestruction prop in vehicles)
        if (prop.isBroken)
        {
            GameLost();
            break;
        }
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
