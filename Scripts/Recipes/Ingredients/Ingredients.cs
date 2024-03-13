// Ok, so this script isn't a monobehaviour, which means it should work fine as long as you don't attach it to any game objects.
// Now, what it does is basically establish what an ingredient is (seen in the Ingredient class), and all universal values and shiz
// you can just throw in there, for every ingredient (such as value, name, etc.)
// However, each ingredient is also its own class incase you want there to be something special about it, such as chili's having
// a spicy level or something like that.
// So any ingredients, just make a class and anything special, and you'll be grand, should be able to access them from other scripts :D.

// Alright, this script is pretty clean for the most part. I don't think it needs any changes apart from adding ingredients. Maybe
// give ingredients values for buying in a shop, or ID's for comparing when makin recipes (although prob won't do that).

[System.Serializable]
public class Ingredient
{
    public string _ingredientName;

    public Ingredient(string name)
    {
        _ingredientName = name;
    }
}

public class Tomato : Ingredient
{
    public Tomato() : base("Tomato")
    {
        
    }
}
public class Cheese : Ingredient
{
    public Cheese() : base("Cheese")
    {

    }
}
public class Bread : Ingredient
{
    public Bread() : base("Bread")
    {

    }
}
