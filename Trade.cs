namespace App;
enum TradeStatus {
    Pending,
    Accepted,
    Denied,
    Completed }

class Trade
{
    public int Id { get; set; }
    public User FromUser { get; set; }        // who sent the request
    public User ToUser { get; set; }          // who decides (receiver)
    public Item OfferedItem { get; set; }     // item from FromUser
    public Item RequestedItem { get; set; }   // item from ToUser
    public TradeStatus Status { get; set; } = TradeStatus.Pending;

    public override string ToString()
    {
        return $"#{Id} {FromUser.Username} → {ToUser.Username} | " +
               $"Offer: {OfferedItem.ItemName} for {RequestedItem.ItemName} | {Status}";
    }
}