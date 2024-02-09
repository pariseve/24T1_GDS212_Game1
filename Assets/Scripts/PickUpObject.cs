using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private GameObject carriedObject;

    void Update()
    {
        // Check if the numpad 0 key is held down
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If not already carrying an object, try to pick one up
            if (carriedObject == null)
            {
                TryPickUpObject();
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            // If the key is released, drop the carried object if any
            if (carriedObject != null)
            {
                DropObject();
            }
        }
    }

    void TryPickUpObject()
    {
        Vector2 raycastOrigin = transform.position;
        Vector2 raycastDirection = Vector2.down;

        // Define the layer mask for the "Interactable" layer
        LayerMask layerMask = LayerMask.GetMask("Interactable");

        // Use a raycast to check if there is an object with the "Interactable" tag in front of the bird
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, 10f, layerMask);

        // Log raycast information
        Debug.DrawRay(raycastOrigin, raycastDirection * 10f, Color.red);
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

        // Check if the object has a Collider component
        Collider2D objectCollider = carriedObject.GetComponent<Collider2D>();
        if (objectCollider != null)
        {
            // Disable the object's collider and set it as a child of the bird for carrying
            objectCollider.enabled = false;
            carriedObject.transform.SetParent(transform);

            Debug.Log($"Picked up {carriedObject.name}.");
        }
        else
        {
            Debug.LogWarning($"Cannot pick up {carriedObject.name} because it doesn't have a Collider component.");
            // Optionally handle the case where the object doesn't have a Collider
            carriedObject = null; // Reset the carriedObject variable
        }
    }


    void DropObject()
    {
        // Enable the object's collider and remove it from being a child of the bird
        carriedObject.GetComponent<Collider2D>().enabled = true;
        carriedObject.transform.SetParent(null);

        Debug.Log($"Dropped {carriedObject.name}.");

        // Reset the carriedObject variable
        carriedObject = null;
    }
}

