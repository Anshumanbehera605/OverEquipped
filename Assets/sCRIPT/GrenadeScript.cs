using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeScript : MonoBehaviour
{
    public float maxDistanceToNotExplode = 3f;
    public float timeToExplode = 2f;
    float distanceTravelled = 0;
    bool shouldExplode = false;
    bool collided = false;
    bool exploded = false;
    Rigidbody rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldExplode) distanceTravelled += rigidbody.linearVelocity.magnitude * Time.deltaTime;
        timeToExplode -= Time.deltaTime;

        if (timeToExplode <= 0 && shouldExplode)
        {
            explode();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collided) return;
        if (distanceTravelled > maxDistanceToNotExplode) {
            shouldExplode = true;
            collided = true;
            print("This will explode");
        }
    }

    void explode()
    {
        if (exploded) return;
        transform.GetChild(0).gameObject.SetActive(true);
        gameObject.tag = "Projectile";
        rigidbody.isKinematic = true;
        exploded = true;
        print("Exploding");
    }
}
