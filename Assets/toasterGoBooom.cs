using UnityEngine;

public class toasterGoBooom : MonoBehaviour
{
    public Rigidbody flyBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Boom()
    {
        flyBody.isKinematic = false;
        flyBody.AddForce(Vector3.up * 1, ForceMode.Impulse);
    }
}
