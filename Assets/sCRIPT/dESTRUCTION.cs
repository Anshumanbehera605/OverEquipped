using UnityEngine;

public class PropDestruction : MonoBehaviour
{
    [Header("Assign the Broken Prefab Here")]
    public GameObject brokenPrefab;

    [Header("Settings")]
    public float breakForce = 10f; // How hard pieces fly apart
    public float collisionThreshold = 5f; // How hard you have to hit it to break

    // This detects when something hits the object
    void OnCollisionEnter(Collision collision)
    {
        // 1. Check if the hit was hard enough (so it doesn't break if you just gently touch it)
        // OR check if the object hitting it has a specific tag like "Bullet"
        if ( collision.gameObject.CompareTag("Projectile"))
        {
            //collision.relativeVelocity.magnitude > collisionThreshold ||
            BreakTheObject();
        }
    }

    public void BreakTheObject()
    {
        // 2. Spawn the broken version at the same position and rotation
        GameObject brokenInstance = Instantiate(brokenPrefab, transform.position, transform.rotation);

        // 3. Find all Rigidbodies in the new broken object and explode them
        Rigidbody[] rbs = brokenInstance.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            // Add force outwards + a little bit upwards for drama
            rb.AddExplosionForce(breakForce, transform.position, 2f);
        }

        // 4. Delete this unbroken object
        Destroy(gameObject);

        // Optional: Delete the broken pieces after 5 seconds to save lag
        Destroy(brokenInstance, 5f);
    }
}