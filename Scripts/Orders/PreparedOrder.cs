using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedOrder
{
    private static PreparedOrder _instance;
    private Recipe _recipe;

    public static PreparedOrder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PreparedOrder();
            }
            return _instance;
        }
    }
    public Recipe preparedOrder
    {
        get { return _recipe; }
        set { _recipe = value; }
    }
}
