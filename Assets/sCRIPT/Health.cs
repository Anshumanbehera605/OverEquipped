using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Reaction Settings")]
    public string tagToMove = "LoseTarget"; // Set this to "Cat" or "Player" (whatever tag the object has)
    public float moveForce = 500f;          // How hard to push it
    public Vector3 pushDirection = new Vector3(0, 1, 1); // Direction: Up and Forward

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        // Debug.Log(gameObject.name + " took damage! Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 1. Find the object you want to move (The Cat/Player)
        GameObject objectToMove = GameObject.FindGameObjectWithTag(tagToMove);

        if (objectToMove != null)
        {
            Rigidbody targetRb = objectToMove.GetComponent<Rigidbody>();

            if (targetRb != null)
            {
                // Ensure physics is on (unfreeze it if it was stuck in a tree)
                targetRb.isKinematic = false;

                // Add the force
                targetRb.AddForce(pushDirection.normalized * moveForce);

                Debug.Log("Pushing the " + objectToMove.name);
            }
        }
        else
        {
            Debug.LogWarning("Could not find an object with tag: " + tagToMove);
        }

        Debug.Log("GAME WIN! Target Destroyed.");
        Destroy(gameObject); // Destroy this enemy
    }
}