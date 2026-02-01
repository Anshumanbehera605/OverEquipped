using UnityEngine;

public class hammerScript : MonoBehaviour
{
    float initialPitch;
    public float finalPitch = -60f;

    public float swingSpeed = 300f;
    public float returnSpeed = 450f;
    public GameObject child;
    public bool isDestroktor = true;

    [Header("Audio Settings")]
    public AudioClip swingSound; // Drag "Whoosh" sound here
    public AudioClip hitSound;   // Drag "Bang/Thud" sound here
    private AudioSource audioSource;

    public bool swinging;

    void Start()
    {
        initialPitch = transform.localEulerAngles.x;

        // Setup AudioSource automatically
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        Vector3 angles = transform.localEulerAngles;

        // 1. INPUT DETECTED
        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            swinging = true;

            // PLAY SWING SOUND
            if (audioSource != null && swingSound != null)
            {
                audioSource.PlayOneShot(swingSound);
            }
        }

        if (swinging)
        {
            angles.x = Mathf.MoveTowardsAngle(
                angles.x,
                finalPitch,
                swingSpeed * Time.deltaTime
            );

            if (Mathf.Abs(Mathf.DeltaAngle(angles.x, finalPitch)) < 0.5f)
            {
                swinging = false;
            }
        }
        else
        {
            angles.x = Mathf.MoveTowardsAngle(
                angles.x,
                initialPitch,
                returnSpeed * Time.deltaTime
            );
        }

        if (isDestroktor) child.gameObject.tag = swinging ? "Projectile" : "Untagged";

        transform.localEulerAngles = angles;
    }

    // 2. IMPACT DETECTED
    // This runs when the hammer physically hits a wall or enemy
    void OnCollisionEnter(Collision collision)
    {
        // Only play impact sound if we are currently swinging
        if (swinging)
        {
            // Optional: Avoid playing sound if hitting the player itself
            if (collision.gameObject.tag == "Player") return;

            // PLAY HIT SOUND
            if (audioSource != null && hitSound != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}