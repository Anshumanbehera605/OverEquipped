using UnityEngine;

public class outOfBounds : MonoBehaviour
{
    public Level4Manager level4Manager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            level4Manager.GameLost();
        }
    }
}
