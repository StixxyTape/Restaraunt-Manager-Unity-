using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for ingredients that you own. Different from the regular ingredient class, in that those are just every ingredient in the game,
// whereas thses are the ones you have, so they come with a quantity counter aswell. Might change name later, or framework. See how you get on >:).

[System.Serializable]
public class InventoryIngredient
{
    public Ingredient _ingredient;
    public int _quantity;

    public InventoryIngredient(Ingredient ingredient, int quantity)
    {
        _ingredient = ingredient;
        _quantity = quantity;
    }
}

// An inventory class, although really just for ingredients. Basically just keeps track of what you have, and how many.
public class Inventory : MonoBehaviour
{
    // Singleton instance
    public static Inventory _instance { get; private set; }

    private List<InventoryIngredient> _inventory = new List<InventoryIngredient>();

    private void Awake()
    {
        // Adding Ingredients to the players Inventory. The ingredients come from the "Ingredients" script.
        AddToInventory(new Tomato(), 4);
        AddToInventory(new Bread(), 3);
        AddToInventory(new Cheese(), 5);

        // Ensure only one instance exists
        if (_instance == null)
        {
            _instance = this; // Set the singleton instance to this object
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances of this script
            return;
        }
    }

    public void AddToInventory(Ingredient ingredient, int quantity)
    {
        InventoryIngredient existingIngredient = _inventory.Find(i => i._ingredient._ingredientName == ingredient._ingredientName);
        if (existingIngredient != null)
        {
            existingIngredient._quantity += quantity;
        }
        else
        {
            InventoryIngredient newIngredient = new InventoryIngredient(ingredient, quantity);
            _inventory.Add(newIngredient);
        }
    }

    public List<InventoryIngredient> GetInventory()
    {
        return _inventory;
    }
}
