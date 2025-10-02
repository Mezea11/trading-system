namespace App;

class Shop
{
    List<Item> Items { get; set; }

    public Shop()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public void DisplayAllItems(Item item)
    {
        foreach(var it in Items)
        {
            Console.WriteLine(it.ToString());
        }
    }

    public override string ToString()
    {
        return $"Total of items: {Items.Count}.";
    }
}