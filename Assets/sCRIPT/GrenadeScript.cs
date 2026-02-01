using Cinemachine;
using UnityEditor.PackageManager;
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

    float distanceTravelled = 0f;
    bool shouldExplode = false;
    bool collided = false;
    bool exploded = false;
    AudioSource source;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        // Explosion visual (child) OFF at start
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (!shouldExplode)
        {
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

        if (distanceTravelled > maxDistanceToNotExplode)
        {
            shouldExplode = true;
            Debug.Log("Grenade armed, will explode");
        }
        else
        {
            // Optional: bounce or ignore
            Debug.Log("Grenade collision ignored (too close)");
            Collider[] hits = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.CompareTag("Insect"))
                {
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        if (source != null) source.Play();
        
        impulseSource.GenerateImpulse();

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

        // Physics explosion
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

        // Optional: change tag
        gameObject.tag = "Projectile";

        // Destroy grenade object
        //Destroy(gameObject);
    }

    // Debug explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
