using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeScript : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float maxDistanceToNotExplode = 3f;
    public float timeToExplode = 2f;
    public float explosionRadius = 3f;
    public float explosionForce = 500f;

    [Header("Effects")]
    public GameObject explosionEffect;
    public CinemachineImpulseSource impulseSource;

    [Header("Game Logic")]
    public string winTag = "WinTarget";   // Instant win if hit (e.g. Spider)
    public string loseTag = "LoseTarget"; // Game Over if hit (e.g. Cat)
    public float damageToDeal = 50f;      // How much damage to deal if target has Health

    float distanceTravelled = 0f;
    bool shouldExplode = false;
    bool collided = false;
    bool exploded = false;
    public bool isRocket = false;
    AudioSource source;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        // Hide visual child if it exists (so we see the grenade flying, but maybe hide a dummy model)
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (!shouldExplode)
        {
            // Unity 6 uses 'linearVelocity'. 
            // If you are on an older version and get an error, change this to: rb.velocity.magnitude
            distanceTravelled += rb.linearVelocity.magnitude * Time.deltaTime;
        }

        if (shouldExplode)
        {
            timeToExplode -= Time.deltaTime;

            if (timeToExplode <= 0f)
            {
                Explode();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collided) return;
        collided = true;

        if (isRocket) Explode();

        // --- GAME LOGIC START ---

        // 1. Check for Health Script (Damage Accumulation)
        // If the object has HP, we deal damage instead of instantly destroying it
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageToDeal);
            // We force an explosion because we hit a valid enemy
            shouldExplode = true;
            Explode();
            return;
        }

        // 2. Check Tags (Instant Win/Loss)
        // Only run this if there was no Health script
        if (collision.gameObject.CompareTag(winTag))
        {
            Debug.Log("GAME WIN! You hit the target: " + collision.gameObject.name);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(loseTag))
        {
            Debug.Log("GAME OVER! You hit the wrong target: " + collision.gameObject.name);
        }
        // --- GAME LOGIC END ---


        // 3. Safety Distance Check
        // If we travelled far enough, arm the grenade
        if (distanceTravelled > maxDistanceToNotExplode)
        {
            shouldExplode = true;
            Debug.Log("Grenade armed, will explode");
        }
        else
        {
            // If we hit a wall too close to the player, ignore it (unless it was an enemy handled above)
            Debug.Log("Grenade collision ignored (too close)");

            // Fallback: Check for "Insect" tag specifically if you still use that
            if (collision.gameObject.CompareTag("Insect"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        if (source != null) source.Play();

        if (impulseSource != null) impulseSource.GenerateImpulse();

        Debug.Log("Exploding");

        // Spawn explosion effect
        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(
              explosionEffect,
              transform.position,
              Quaternion.identity
            );
            Destroy(effect, 2f);
        }

        // Physics explosion (Push objects)
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hits)
        {
            Rigidbody hitRb = hit.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                hitRb.AddExplosionForce(
                  explosionForce,
                  transform.position,
                  explosionRadius
                );
            }
        }

        // Break props (Toaster, etc.)
        Collider[] hits1 = Physics.OverlapSphere(transform.position, explosionRadius / 2);
        foreach (Collider hit in hits1)
        {
            PropDestruction hitbody = hit.gameObject.GetComponent<PropDestruction>();
            if (hitbody != null)
            {
                hitbody.BreakTheObject();
            }
        }

        // Stop grenade physics
        rb.isKinematic = true;
        gameObject.tag = "Projectile";

        // Optional: Destroy the grenade object after sound finishes
        Destroy(gameObject, 2f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}