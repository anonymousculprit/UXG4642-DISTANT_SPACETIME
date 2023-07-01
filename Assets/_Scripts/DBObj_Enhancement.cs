public struct DBObj_Enhancement
{
    public static int fieldCount = 8;

    public DBObj_Enhancement(string name, string abbreviation, int cost, int cap, string effect, string type, string[] subtype, string category)
    {
        this.name = name;
        this.abbreviation = abbreviation;
        this.cost = cost;
        this.cap = cap;
        this.effect = effect;
        this.type = type;
        this.subtype = subtype;
        this.category = category;

        if (category == "Equipment")
            DefaultEmptyOrNullValues();
    }

    public DBObj_Enhancement(string[] input, char delimiter)
    {
        this.name = input[0]; 
        this.abbreviation = input[1]; 
        this.cost = int.Parse(input[2]); 
        this.cap = int.Parse(input[3]); 
        this.effect = input[4];
        this.category = input[5];
        this.type = input[6]; 
        this.subtype = Utility.ParseForStringArray(input[7], delimiter);
    }

    public string name { get; private set; }
    public string abbreviation { get; private set; }
    public int cost { get; private set; }
    public int cap { get; private set; }
    public string effect { get; private set; }
    public string category { get; private set; }
    public string type { get; private set; }
    public string[] subtype { get; private set; }

    void DefaultEmptyOrNullValues()
    {
        if (string.IsNullOrEmpty(type)) type = "All";
        if (subtype == null || subtype.Length == 0) subtype = new string[] { "All" };
    }
}