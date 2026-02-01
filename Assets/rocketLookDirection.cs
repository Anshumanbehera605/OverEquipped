using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RocketLookDirection : MonoBehaviour
{
    Rigidbody rb;

    public float rotationSpeed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // If rocket is moving
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(-rb.linearVelocity.normalized);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }
    }
}
