using UnityEngine;

public class malletScript : MonoBehaviour
{
    public hammerScript hammerScript;
    public Level2Manager level2Manager;
    
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
            collision.gameObject.GetComponent<toasterGoBooom>().Boom();
            level2Manager.GameWon();
        }
    }
}
