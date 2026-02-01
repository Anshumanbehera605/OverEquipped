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
    public AudioClip swingSound; // Sound when you click (Whoosh)
    public AudioClip hitSound;   // Sound when you hit something (Clank/Thud)
    private AudioSource audioSource;

    bool swinging;

    void Start()
    {
        initialPitch = transform.localEulerAngles.x;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        Vector3 angles = transform.localEulerAngles;

        // 1. Swing Input & Sound
        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            swinging = true;
            if (audioSource != null && swingSound != null)
            {
                audioSource.PlayOneShot(swingSound);
            }
        }

        // 2. Swing Movement
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

    // ---------------------------------------------------------
    // NEW: IMPACT LOGIC
    // ---------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        // Only play the hit sound if we are actually swinging
        if (swinging)
        {
            // Optional: Don't play sound if hitting the Player himself
            if (collision.gameObject.tag == "Player") return;

            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}