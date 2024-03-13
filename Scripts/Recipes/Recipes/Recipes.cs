using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// The Recipes script manages the collection of available recipes in the game.
// It dynamically adds recipes from subclasses of the Recipe class to the list of available recipes.
// Each recipe consists of a list of required ingredients, a preparation method, a recipe name, duration, and an ID.
// The Recipes script provides a method to retrieve the available recipes

public class Recipes : MonoBehaviour
{
    public List<Recipe> _availableRecipes = new List<Recipe>(); // List of available recipes

    private void Awake()
    {
        AddRecipesFromSubclasses(); // Add recipes from subclasses when the object is awakened
    }

    private void AddRecipesFromSubclasses()
    {
        Assembly assembly = Assembly.GetExecutingAssembly(); // Get the assembly containing the recipes
        Type recipeType = typeof(Recipe); // Get the base type of Recipe

        foreach (Type type in assembly.GetTypes()) // Iterate through all types in the assembly
        {
            if (recipeType.IsAssignableFrom(type) && type != recipeType) // Check if the type is a subclass of Recipe
            {
                Recipe recipe = Activator.CreateInstance(type) as Recipe; // Create an instance of the recipe subclass
                if (recipe != null)
                {
                    _availableRecipes.Add(recipe); // Add the recipe to the list of available recipes
                }
            }
        }
    }

    public Recipe[] GetAvailableRecipes()
    {
        return _availableRecipes.ToArray(); // Return an array of available recipes
    }
}

[System.Serializable]
public class Recipe
{
    public List<Ingredient> _requiredIngredients = new List<Ingredient>(); // List of required ingredients for the recipe
    public PreparationMethod _preparation; // The preparation method for the recipe
    public string _recipeName; // The name of the recipe
    public float _duration; // The duration of the recipe
    public int _id; // The ID of the recipe

    public Recipe(List<Ingredient> ingredients, string name, PreparationMethod prep, float duration, int id)
    {
        _requiredIngredients = ingredients; // Initialize the required ingredients list
        _recipeName = name; // Set the recipe name
        _preparation = prep; // Set the preparation method
        _duration = duration; // Set the duration
        _id = id; // Set the ID
    }

    public virtual List<Ingredient> GetIngredients()
    {
        return _requiredIngredients; // Return the required ingredients for the recipe
    }

    public virtual PreparationMethod GetPreparation()
    {
        return _preparation; // Return the preparation method for the recipe
    }
}

public class FullSandwich : Recipe
{
    public FullSandwich() : base(null, "Full Sandwich", null, 5f, 1)
    {

    }

    public override List<Ingredient> GetIngredients()
    {
        return new List<Ingredient>
        {
            new Tomato(),
            new Bread(),
            new Cheese()
        }; // Return the specific ingredients for the Full Sandwich recipe
    }

    public override PreparationMethod GetPreparation()
    {
        return new SimplePreparation(5f, new FullSandwich()); // Return the specific preparation method for the Full Sandwich recipe
    }

}

public class TomatoSandwich : Recipe
{
    public TomatoSandwich() : base(null, "Tomato Sandwich", null, 5f, 2)
    {

    }

    public override List<Ingredient> GetIngredients()
    {
        return new List<Ingredient>
        {
            new Tomato(),
            new Bread(),
        }; // Return the specific ingredients for the Tomato Sandwich recipe
    }

    public override PreparationMethod GetPreparation()
    {
        return new SimplePreparation(3.5f, new TomatoSandwich()); // Return the specific preparation method for the Tomato Sandwich recipe
    }
}



