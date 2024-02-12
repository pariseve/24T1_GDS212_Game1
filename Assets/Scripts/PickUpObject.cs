using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform pickupPosition; // Reference to the empty GameObject for picking up objects
    private GameObject carriedObject;
    private bool isCarryingObject = false;

    void Update()
    {
        // Check if the E key is held down
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isCarryingObject)
            {
                // If already carrying an object, drop it
                DropObject();
            }
            else
            {
                // If not already carrying an object, try to pick one up
                TryPickUpObject();
            }
        }

        // If carrying an object, update its position to follow the pickup position
        if (isCarryingObject)
        {
            carriedObject.transform.position = pickupPosition.position;
        }
    }

    void TryPickUpObject()
    {
        Vector2 raycastOrigin = pickupPosition.position;
        Vector2 raycastDirection = Vector2.down;

        // Define the layer mask for the "Interactable" layer
        LayerMask layerMask = LayerMask.GetMask("Interactable");

        // Use a raycast to check if there is an object with the "Interactable" tag in front of the pickup position
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, 10f, layerMask);

        // Log raycast information
        Debug.DrawRay(raycastOrigin, raycastDirection * 5f, Color.red);
        Debug.Log($"Raycast origin: {raycastOrigin}, Raycast direction: {raycastDirection}");

        if (hit.collider != null)
        {
            Debug.Log($"Raycast hit: {hit.collider.gameObject.name}, Tag: {hit.collider.tag}");

            // Ensure the collider is of type Collider2D
            Collider2D objectCollider = hit.collider as Collider2D;
            if (objectCollider != null && objectCollider.CompareTag("Interactable"))
            {
                Debug.Log("Hit Interactable!");
                PickUp(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("Object found, but it doesn't have the 'Interactable' tag or is not a Collider2D.");
            }
        }
        else
        {
            Debug.Log("No object found in the raycast.");
        }
    }

    void PickUp(GameObject objToPickUp)
    {
        // Set the carried object
        carriedObject = objToPickUp;
        isCarryingObject = true;

        // Disable the object's Rigidbody component
        Rigidbody2D rb = carriedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        // Set the object's position to the pickup position
        carriedObject.transform.position = pickupPosition.position;

        Debug.Log($"Picked up {carriedObject.name}.");
    }

    void DropObject()
    {
        // Enable the object's Rigidbody component
        Rigidbody2D rb = carriedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
        }

        isCarryingObject = false;

        Debug.Log($"Dropped {carriedObject.name}.");

        // Reset the carriedObject variable
        carriedObject = null;
    }
}
