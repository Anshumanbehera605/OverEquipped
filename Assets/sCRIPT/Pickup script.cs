using UnityEngine;

public class HammerPickupSystem : MonoBehaviour
{
    [Header("Hand References")]
    public GameObject hammer1_Hand; // The hammer you start with
    public GameObject hammer2_Hand; // The hammer hidden in your hand

    [Header("Settings")]
    public float pickupRange = 3f;   // How far you can reach
    public Camera playerCamera;      // Drag your Main Camera here

    void Start()
    {
        // Ensure we start with only Hammer 1
        if (hammer1_Hand != null) hammer1_Hand.SetActive(true);
        if (hammer2_Hand != null) hammer2_Hand.SetActive(false);
    }

    void Update()
    {
        // Check if player presses "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        // Create a ray from the center of the camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Shoot the laser
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // Check if we hit an object tagged "Pickup"
            if (hit.collider.CompareTag("Pickup"))
            {
                // 1. Equip the new hammer
                EquipHammer2();

                // 2. Destroy the object on the ground
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void EquipHammer2()
    {
        // Hide the first hammer
        if (hammer1_Hand != null) hammer1_Hand.SetActive(false);

        // Show the second hammer
        if (hammer2_Hand != null) hammer2_Hand.SetActive(true);

        Debug.Log("Picked up and equipped Hammer 2!");
    }
}