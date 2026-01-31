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

    void Update()
    {
        bool isShooting;

        zOffset = Mathf.Lerp(zOffset, 0, 0.03f);
        pitchOffset = Mathf.Lerp(pitchOffset, 0, 0.03f);

        if (shootingMode == ShootingMode.Automatic)
            isShooting = Input.GetMouseButton(0);
        else
            isShooting = Input.GetMouseButtonDown(0);

        if (readyToShoot && isShooting)
        {
            bulletsLeftInBurst = bulletsPerBurst;
            Shoot();
        }

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
        zOffset = -0.2f;
        pitchOffset = -10;

        if (smoke != null) smoke.Play();

        Vector3 direction = GetShootDirection();

        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            Quaternion.identity
        );

        bullet.transform.forward = direction.normalized;

        bullet.GetComponent<Rigidbody>().AddForce(
            direction.normalized * bulletSpeed,
            ForceMode.Impulse
        );

        StartCoroutine(DestroyBullet(bullet));

        Invoke(nameof(ResetShot), shootDelay);

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

        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100f);

        Vector3 direction = targetPoint - bulletSpawnPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        return direction + new Vector3(x, y, 0);
    }

    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(bullet);
    }
}
