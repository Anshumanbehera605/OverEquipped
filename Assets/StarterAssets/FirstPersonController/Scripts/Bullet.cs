using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Hit: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
