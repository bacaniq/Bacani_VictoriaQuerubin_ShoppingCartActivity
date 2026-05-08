using System;

class Product
{

    // Private fields
    private int ID;
    private string Name;
    private double Price;
    private int RemainingStock;
    private string Category;

    // Getters
    public int GetID() { return ID; }
    public string GetName() { return Name; }
    public double GetPrice() { return Price; }
    public int GetRemainingStock() { return RemainingStock; }
    public string GetCategory() { return Category; }

    // Setters
    public void SetID(int id) { this.ID = id; }
    public void SetName(string name) { this.Name = name; }
    public void SetPrice(double price) { if (price >= 0) this.Price = price; }
    public void SetRemainingStock(int stock) { if (stock >= 0) this.RemainingStock = stock; }
    public void SetCategory(string category) { this.Category = category; }


    public void DisplayProduct()
    {
        Console.WriteLine($"{GetID()}. {GetName()} - P{GetPrice()} - (Stock: {GetRemainingStock()}) - Category: {GetCategory()}");
    }
    public double GetItemTotal(int quantity)
    {
        return GetPrice() * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return GetRemainingStock() >= quantity;
    }

    public void DeductStock(int quantity)
    {
        this.RemainingStock -= quantity;
    }
}
