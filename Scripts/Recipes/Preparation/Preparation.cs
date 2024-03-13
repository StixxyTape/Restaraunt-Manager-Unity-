using System.Collections;
using UnityEngine;

// Ok, right now this preparation script basically only does one thing with the simple prep method.
// I will see if it is able to handle other methods aswell. Might add some things. Dunno.

public class Preparation : MonoBehaviour
{
    private bool _activePreparation; // Flag indicating if a preparation is currently active

    // BeginPreparation method is called to start a preparation process
    // It starts a coroutine to perform the preparation over a specified duration
    public void BeginPreparation(float duration, Recipe recipe, GameObject prepObject)
    {
        StartCoroutine(PerformPreparation(duration, recipe, prepObject));
    }

    // PerformPreparation is a coroutine that handles the actual preparation process
    // It runs for the specified duration and checks for player interaction to complete the preparation
    private IEnumerator PerformPreparation(float duration, Recipe recipe, GameObject prepObject)
    {
        _activePreparation = true;

        Debug.Log("Started Prep");

        float timer = 0f;
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;

            // Check if the interact button is pressed and the player is in range of the preparation object
            GameObject closestInteractable = PlayerInteraction._instance.ClosestInteractable();

            if (PlayerInteraction._instance.Interact()
                && closestInteractable == prepObject)
            {
                Debug.Log("Done Preparation");
                PreparedOrder.Instance.preparedOrder = recipe; // Serve the prepared order to the customer
                Debug.Log("The prepared order is: " + PreparedOrder.Instance.preparedOrder);
                _activePreparation = false;
                break;
            }
        }

        if (_activePreparation)
        {
            Debug.Log("Preparation timed out.");
        }
    }
}
