using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem particles;
    public float range = 12f;
    public float radius = 2f;
    public LayerMask hitMask;
    public Color burnColor = Color.black;
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
            FireFlame();
        } else {
            particles.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if (!Input.GetMouseButton(0)) return;
        Renderer mesh = other.gameObject.GetComponent<Renderer>();
        mesh.material.color = Color.black;
        // mesh.material.SetColor("lit", Color.black);
    }

    private Dictionary<Renderer, Color> originalColors =
        new Dictionary<Renderer, Color>();


    void FireFlame()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            radius,
            transform.forward,
            range,
            hitMask,
            QueryTriggerInteraction.Ignore
        );

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend == null) continue;

            // Save original color once
            if (!originalColors.ContainsKey(rend))
            {
                originalColors.Add(rend, rend.material.color);
            }

            rend.material.color = burnColor;
        }
    }

    // Optional reset
    public void ResetBurns()
    {
        foreach (var pair in originalColors)
        {
            if (pair.Key != null)
                pair.Key.material.color = pair.Value;
        }

        originalColors.Clear();
    }
}
