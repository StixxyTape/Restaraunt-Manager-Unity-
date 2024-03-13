using UnityEngine;
using System.Collections;

// The Customer script represents a customer in the game.
// It handles taking orders from the customer, serving orders, and managing the current order.
// The script ensures that only one instance of the Customer exists using the Singleton pattern.

public class Customer : MonoBehaviour
{
    private Recipe _preparedOrder;
    private Recipe _currentOrder; // The current order of the customer
    private Recipes _recipes; // Reference to the Recipes script

    private void Awake()
    {
        // Find and assign the Recipes script
        _recipes = FindObjectOfType<Recipes>();
        _currentOrder = GetRandomRecipe();

        StartCoroutine(Customer_NotOrdered());
    }

    private IEnumerator Customer_NotOrdered()
    {
        while (true)
        {
            yield return null;

            GameObject closestInteractable = PlayerInteraction._instance.ClosestInteractable();

            if (PlayerInteraction._instance.Interact()
                && closestInteractable == gameObject)
            {
                if (_currentOrder == null)
                {
                    Debug.Log("Customer: Gimme a second, I'm still ordering?");
                }
                else
                {
                    yield return null;

                    TakeOrder();
                    break;
                }
            }
        }
    }

    private void TakeOrder()
    {
        Debug.Log($"Customer: I'd like to order {_currentOrder._recipeName}.");

        Order<Recipe> newOrder = new Order<Recipe>()
        {
            CustomerName = "CustomerNameHere", // Set the customer name
            Recipe = _currentOrder // Assign the current order to the order object
        };

        OrdersMenu._instance.AddOrder(newOrder); // Add the new order to the list of orders
        StartCoroutine(Customer_Ordered());
    }

    private IEnumerator Customer_Ordered()
    {
        while (true)
        {
            _preparedOrder = PreparedOrder.Instance.preparedOrder;

            GameObject closestInteractable = PlayerInteraction._instance.ClosestInteractable();

            if (PlayerInteraction._instance.Interact()
                && closestInteractable == gameObject)
            {
                if (_preparedOrder == null)
                {
                    Debug.Log($"Customer: Where the crap is my {_currentOrder._recipeName}?!?!?!");
                }
                else if (_preparedOrder._id == _currentOrder._id)
                {
                    Debug.Log($"Customer: Thank you for the {_currentOrder._recipeName}!");
                    PreparedOrder.Instance.preparedOrder = null;
                    OrdersMenu._instance._selectedRecipe = null;
                    Destroy(gameObject);
                    break;
                }
                else if (_preparedOrder._id != _currentOrder._id)
                {
                    Debug.Log($"Customer: I didn't order that {_preparedOrder._recipeName}?!?!?!");
                }

                yield return null;
            }

            yield return null;

        }
    }

    private Recipe GetRandomRecipe()
    {
        Recipe[] availableRecipes = _recipes.GetAvailableRecipes(); // Get the array of available recipes from the Recipes script
        int randomIndex = Random.Range(0, availableRecipes.Length); // Generate a random index within the range of available recipes

        Debug.Log(availableRecipes[randomIndex]); // Log the randomly chosen recipe
        return availableRecipes[randomIndex]; // Return the randomly chosen recipe
    }

    

}
