namespace App;

class Item
{
    public string ItemName { get; set; }
    public double Price { get; set; }

    public Item(string itemName, double price)
    {
        ItemName = itemName;
        Price = price;
    }

    public override string ToString()
    {
        return $"Item: {ItemName}, Price: {Price}";
    }

    public void DisplayAllItems(Item[] items)
    {
        foreach (var item in items)
        {
            System.Console.WriteLine(item.ToString());
        }
    }
}