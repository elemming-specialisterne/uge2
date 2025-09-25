class Player(string name, bool isBot)
{
    private readonly string name = name;
    private readonly bool isBot = isBot;
    private readonly List<Hand> hands = [new Hand(isBot)];
    private bool isOut = false;
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
    public bool IsOut()
    {
        return isOut;
    }
    public void SetOut(bool status)
    {
        isOut = status;
    }
    public void Clear()
    {
        hands.Clear();
        hands.Add(new Hand(isBot));
    }
}