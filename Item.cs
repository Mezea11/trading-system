namespace App;

class Item
{
    public string ItemName { get; set; }
    public double Price { get; set; }

    public string Information {get; set;}

    public Item(string itemName, double price, string information)
    {
        ItemName = itemName;
        Price = price;
        Information = information;
    }

    public override string ToString()
    {
        return $"Item: {ItemName}, Price: {Price}, Info: {Information}";
    }

    public void DisplayAllItems(Item[] items)
    {
        foreach (var item in items)
        {
            System.Console.WriteLine(item.ToString());
        }
    }
}