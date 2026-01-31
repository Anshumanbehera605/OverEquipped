using UnityEngine;

public class hammerScript : MonoBehaviour
{
    float initialPitch;
    public float finalPitch = -60f;

    public float swingSpeed = 300f;
    public float returnSpeed = 450f;
    public GameObject child;
    public bool isDestroktor = true;

    bool swinging;

    void Start()
    {
        initialPitch = transform.localEulerAngles.x;
    }

    void Update()
    {
        Vector3 angles = transform.localEulerAngles;

        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            swinging = true;
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
}
