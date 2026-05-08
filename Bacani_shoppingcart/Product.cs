using System;

class Product
{

    public int ID;
    public string Name;
    public double Price;
    public int RemainingStock;
    public string Category;

    public void DisplayProduct()
    {
        Console.WriteLine($"{ID}. {Name} - ${Price} - (Stock: {RemainingStock}) - Category: {Category}");

    }
    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return RemainingStock >= quantity;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}
