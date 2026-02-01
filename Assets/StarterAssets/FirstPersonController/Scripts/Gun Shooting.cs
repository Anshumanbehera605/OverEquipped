using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // Shooting types
    public enum ShootingMode
    {
        Single,
        Burst,
        Automatic
    }

    public ShootingMode shootingMode;

    [Header("Audio Settings")]
    public AudioClip fireSoundClip; // Drag your gunshot sound here
    AudioSource shootSound;

    // References
    public Camera playerCamera;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    // Bullet settings
    public float bulletSpeed = 100f;
    public float bulletLifeTime = 3f;
    public float spread = 0.02f;

    // Fire rate
    public float shootDelay = 0.2f;
    bool readyToShoot = true;

    // Burst
    public int bulletsPerBurst = 3;
    int bulletsLeftInBurst;

    // Recoil / Visuals
    float initialZ = 0;
    float initialPitch = 0;
    float zOffset = 0;
    float pitchOffset = 0;

    public ParticleSystem smoke;

    void Awake()
    {
        bulletsLeftInBurst = bulletsPerBurst;
        initialZ = transform.localPosition.z;
    }

    void Start()
    {
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool isShooting;

        // Visual Recoil Recovery
        zOffset = Mathf.Lerp(zOffset, 0, 0.03f);
        pitchOffset = Mathf.Lerp(pitchOffset, 0, 0.03f);

        // Input Check
        if (shootingMode == ShootingMode.Automatic)
            isShooting = Input.GetMouseButton(0);
        else
            isShooting = Input.GetMouseButtonDown(0);

        // Shooting Logic
        if (readyToShoot && isShooting)
        {
            bulletsLeftInBurst = bulletsPerBurst;
            Shoot();
        }

        // Apply Visual Recoil
        Vector3 localPos = transform.localPosition;
        localPos.z = initialZ + zOffset;
        transform.localPosition = localPos;

        Vector3 rot = transform.localRotation.eulerAngles;
        rot.x = initialPitch + pitchOffset;
        transform.localEulerAngles = rot;
    }

    void Shoot()
    {
        readyToShoot = false;

        // Recoil Kick
        zOffset = -0.2f;
        pitchOffset = -10;

        // Effects
        if (smoke != null) smoke.Play();

        // SOUND LOGIC ADDED HERE
        if (shootSound != null && fireSoundClip != null)
        {
            shootSound.PlayOneShot(fireSoundClip);
        }

        Vector3 direction = GetShootDirection();

        // Instantiate Bullet
        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            Quaternion.identity
        );

        // Rotate bullet to face direction
        bullet.transform.forward = direction.normalized;

        // Add Force
        bullet.GetComponent<Rigidbody>().AddForce(
            direction.normalized * bulletSpeed,
            ForceMode.Impulse
        );

        // Cleanup
        StartCoroutine(DestroyBullet(bullet));

        // Delay next shot
        Invoke(nameof(ResetShot), shootDelay);

        // Burst Logic
        if (shootingMode == ShootingMode.Burst && bulletsLeftInBurst > 1)
        {
            bulletsLeftInBurst--;
            Invoke(nameof(Shoot), shootDelay);
        }
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

    Vector3 GetShootDirection()
    {
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0)
        );

        RaycastHit hit;
        Vector3 targetPoint;

        // Check where the camera is looking
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100f);

        Vector3 direction = targetPoint - bulletSpawnPoint.position;

        // Calculate Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        return direction + new Vector3(x, y, 0);
    }

    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifeTime);
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
}