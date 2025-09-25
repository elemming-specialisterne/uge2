class Player(string name, bool isBot)
{
    private readonly string name = name;
    private readonly bool isBot = isBot;
    private readonly List<Hand> hands = [new Hand(isBot)];
    public List<Hand> GetHands()
    {
        return hands;
    }
    public void AddHand(Hand hand)
    {
        hands.Add(hand);
    }
    public string GetName()
    {
        return name;
    }
    public bool IsBot()
    {
        return isBot;
    }   
    public void clear()
    {
        hands.Clear();
        hands.Add(new Hand(isBot));
    }
}