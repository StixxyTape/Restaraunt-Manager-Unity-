using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersMenu : MonoBehaviour
{
    public static OrdersMenu _instance { get; private set; }

    public Recipe _selectedRecipe;

    [SerializeField]
    private GameObject _orderButtonPrefab;
    [SerializeField]
    private GameObject _orderPanel;
    [SerializeField]
    private float _buttonSpacing = 10f;
    [SerializeField]
    private float _buttonWidth = 200f;
    [SerializeField]
    private float _buttonHeight = 50f;
    private int _buttonsPerRow = 0;
    private float _currentX = 0f;
    private float _currentY = 0f;

    private List<Order<Recipe>> _orders = new List<Order<Recipe>>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // Set this as the singleton instance
        }
        else
        {
            Destroy(gameObject);
        }

        _selectedRecipe = null;
    }

    private void Start()
    {
        PopulateOrdersMenu();
    }

    public void AddOrder(Order<Recipe> order)
    {
        _orders.Add(order);
        PopulateOrdersMenu();
    }

    private void PopulateOrdersMenu()
    {
        foreach (Transform child in _orderPanel.GetComponentsInChildren<Transform>())
        {
            if (child != _orderPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // Calculate the number of buttons per row based on panel width and button size
        RectTransform panelRectTransform = _orderPanel.GetComponent<RectTransform>();
        float panelWidth = panelRectTransform.rect.width;
        _buttonsPerRow = Mathf.FloorToInt(panelWidth / (_buttonWidth + _buttonSpacing));

        // Calculate the starting position for the first button
        _currentX = -panelWidth / 2f + _buttonWidth / 2f;
        _currentY = panelRectTransform.rect.height / 2f - _buttonHeight / 2f;

        // Create new order buttons for each order in the list

        foreach (Order<Recipe> order in _orders)
        {
            CreateOrderButton(order);
        }
    }

    private void CreateOrderButton(Order<Recipe> order)
    {
        GameObject orderButton = Instantiate(_orderButtonPrefab, _orderPanel.transform);
        orderButton.transform.localPosition = new Vector3(_currentX, _currentY, 0f);
        TextMeshProUGUI buttonText = orderButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = order.Recipe._recipeName;

        // Attach a click event handler to the order button
        Button button = orderButton.GetComponent<Button>();
        button.onClick.AddListener(() => HandleOrderClick(order));

        // Update the current position for the next button
        _currentX += _buttonWidth + _buttonSpacing;

        // Check if we have reached the end of the row
        if ((_currentX + _buttonWidth / 2f) > _orderPanel.GetComponent<RectTransform>().rect.width / 2f)
        {
            _currentX = -_orderPanel.GetComponent<RectTransform>().rect.width / 2f + _buttonWidth / 2f;
            _currentY -= _buttonHeight + _buttonSpacing;
        }
    }

    private void HandleOrderClick(Order<Recipe> clickedOrder)
    {
        if (_selectedRecipe == null)
        {
            _selectedRecipe = clickedOrder.Recipe;
            Debug.Log($"Selected order: {_selectedRecipe._recipeName}");

            // Pass the order information to the preparation phase or begin the preparation process directly
            _selectedRecipe.GetPreparation().PreparationCheck();

            _orders.Remove(clickedOrder);
            PopulateOrdersMenu();
        }
    }

    public List<Order<Recipe>> GetOrders()
    {
        return _orders;
    }
}

public class Order<T> where T : Recipe
{
    public string CustomerName { get; set; }
    public T Recipe { get; set; }
}
