namespace KittATM.Models;

public class User {
    public int Id { get; set; }
    public string? CardNumber { get; set; }
    public string? PIN { get; set; }
    public double Balance { get; set; }
}