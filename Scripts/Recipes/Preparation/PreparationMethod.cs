using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Implement a Preparation Time.
// So Basically, this script is going to handle all the different types of Preparation you can do withing the game.
// From chopping to frying, you know the drill. Some recipes will require only one method, while others could require multiple.
// So basically, I'm starting off with a simple method where you just click E to make it infront of a station, as long as you have the ingredients.
// Customer walks in -> Orders Recipe -> Recipe contains a Preparation Method -> If you have ingredients, you can do Preparation
// -> When preparation is done, ingredients are gone, but you have the recipe -> Server to customer

public class PreparationMethod
{
    public Preparation _preparation;
    public string _name;

    public PreparationMethod(string name)
    {
        _name = name;
        _preparation = Object.FindObjectOfType<Preparation>();
    }

    public bool CheckIngredients(List<Ingredient> _ingredients)
    {
        Inventory _inventory = Inventory._instance;

        foreach (Ingredient requiredIngredient in _ingredients)
        {
            bool hasIngredient = false;

            foreach(InventoryIngredient inventoryIngredient in _inventory.GetInventory())
            {
                if  (inventoryIngredient._ingredient._ingredientName == requiredIngredient._ingredientName
                    && inventoryIngredient._quantity > 0)
                {
                    hasIngredient = true;
                    break;
                }
            }

            if (!hasIngredient)
            {
                return false;
            }
        }

        return true;
    }

    public virtual void PreparationCheck()
    {
        Debug.Log("Default prep check");
    }

    public virtual void BeginPreparation()
    {
        Debug.Log("Default prep method");
    }    
}

public class SimplePreparation : PreparationMethod
{
    private GameObject _prepStation;
    private Recipe _recipe;
    private float _duration;

    public SimplePreparation(float duration, Recipe recipe) : base("Simple")
    {
        _duration = duration;
        _recipe = recipe;
        _prepStation = GameObject.FindGameObjectWithTag("Stove");
    }

    public override void PreparationCheck()
    {
        if (CheckIngredients(_recipe.GetIngredients()))
        {
            _preparation.BeginPreparation(_recipe._duration, _recipe, _prepStation);
        }
        else
        {
            Debug.Log("Missing Ingredients.");
        }
    }

}
