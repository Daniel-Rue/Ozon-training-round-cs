using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

int t = Convert.ToInt32(input.ReadLine());
Dictionary<char, int> cardValues = new Dictionary<char, int>
{
    { '2', 2 },
    { '3', 3 },
    { '4', 4 },
    { '5', 5 },
    { '6', 6 },
    { '7', 7 },
    { '8', 8 },
    { '9', 9 },
    { 'T', 10 },
    { 'J', 11 },
    { 'Q', 12 },
    { 'K', 13 },
    { 'A', 14 }
};

for (int i = 0; i < t; i++)
{
    int n = Convert.ToInt32(input.ReadLine());
    Dictionary<char, List<string>> deck = new Dictionary<char, List<string>>
    {
        { '2', new List<string> { "2C", "2D", "2H", "2S" } },
        { '3', new List<string> { "3C", "3D", "3H", "3S" } },
        { '4', new List<string> { "4C", "4D", "4H", "4S" } },
        { '5', new List<string> { "5C", "5D", "5H", "5S" } },
        { '6', new List<string> { "6C", "6D", "6H", "6S" } },
        { '7', new List<string> { "7C", "7D", "7H", "7S" } },
        { '8', new List<string> { "8C", "8D", "8H", "8S" } },
        { '9', new List<string> { "9C", "9D", "9H", "9S" } },
        { 'T', new List<string> { "TC", "TD", "TH", "TS" } },
        { 'J', new List<string> { "JC", "JD", "JH", "JS" } },
        { 'Q', new List<string> { "QC", "QD", "QH", "QS" } },
        { 'K', new List<string> { "KC", "KD", "KH", "KS" } },
        { 'A', new List<string> { "AC", "AD", "AH", "AS" } }
    };


    List<Player> players = new List<Player>();
    for (int j = 0; j < n; j++)
    {
        var pair = input.ReadLine().Split(" ");
        players.Add(new Player(pair[0], pair[1]));
        deck[pair[0][0]].Remove(pair[0]);
        deck[pair[1][0]].Remove(pair[1]);

        if (!deck[pair[0][0]].Any())
        {
            deck.Remove(pair[0][0]);
        }

        if (deck.ContainsKey(pair[1][0]) && !deck[pair[1][0]].Any())
        {
            deck.Remove(pair[1][0]);
        }
    }

    HashSet<string> result = new HashSet<string>();
    foreach (var cardNum in deck)
    {
        if (players[0].FirstCard[0] == players[0].SecondCard[0])
        {
            players[0].High = cardValues[players[0].FirstCard[0]];
            if (players[0].FirstCard[0] == cardNum.Key)
            {
                players[0].Combination = 3;
            }
            else
            {
                players[0].Combination = 2;
            }
        }
        else
        {
            if (players[0].FirstCard[0] == cardNum.Key)
            {
                players[0].High = cardValues[players[0].FirstCard[0]];
                players[0].Combination = 2;
            }
            else if (players[0].SecondCard[0] == cardNum.Key)
            {
                players[0].High = cardValues[players[0].SecondCard[0]];
                players[0].Combination = 2;
            }
            else
            {
                players[0].High = cardValues[players[0].FirstCard[0]] > cardValues[players[0].SecondCard[0]]
                    ? cardValues[players[0].FirstCard[0]]
                    : cardValues[players[0].SecondCard[0]];

                if (players[0].High >= cardValues[cardNum.Key])
                {
                    players[0].Combination = 1;
                }
                else
                {
                    players[0].Combination = 0;
                    players[0].High = 0;
                }
            }
        }


        bool isWon = true;
        for (int j = 1; j < players.Count; j++)
        {
            if (players[j].FirstCard[0] == players[j].SecondCard[0])
            {
                players[j].High = cardValues[players[j].FirstCard[0]];
                players[j].Combination = players[j].FirstCard[0] == cardNum.Key ? 3 : 2;
            }
            else
            {
                if (players[j].FirstCard[0] == cardNum.Key)
                {
                    players[j].High = cardValues[players[j].FirstCard[0]];
                    players[j].Combination = 2;
                }
                else if (players[j].SecondCard[0] == cardNum.Key)
                {
                    players[j].High = cardValues[players[j].SecondCard[0]];
                    players[j].Combination = 2;
                }
                else
                {
                    players[j].High = cardValues[players[j].FirstCard[0]] > cardValues[players[j].SecondCard[0]]
                        ? cardValues[players[j].FirstCard[0]]
                        : cardValues[players[j].SecondCard[0]];

                    if (players[j].High >= cardValues[cardNum.Key])
                    {
                        players[j].Combination = 1;
                    }
                    else
                    {
                        players[j].Combination = 0;
                        players[j].High = 0;
                    }
                }
            }

            if (players[0].Combination < players[j].Combination ||
                (players[0].Combination == players[j].Combination && players[0].High < players[j].High))
            {
                isWon = false;
                break;
            }
        }

        if (isWon)
        {
            foreach (var card in cardNum.Value)
            {
                result.Add(card);
            }
        }
    }

    output.WriteLine(result.Count);
    foreach (var res in result)
    {
        output.WriteLine(res);
    }
}


class Player
{
    public string FirstCard { get; }
    public string SecondCard { get; }

    public int Combination { get; set; }
    public int High { get; set; }

    public Player(string firstCard, string secondCard)
    {
        FirstCard = firstCard;
        SecondCard = secondCard;
        Combination = 0;
    }
}