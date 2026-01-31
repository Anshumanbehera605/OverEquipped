using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            particles.Play();
        } else {
            particles.Stop();
        }
    }
}
