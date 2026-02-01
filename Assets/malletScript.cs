using UnityEngine;

public class malletScript : MonoBehaviour
{
    public hammerScript hammerScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Insect") && hammerScript.swinging)
        {
            print("GameWon");
        }
    }
}
