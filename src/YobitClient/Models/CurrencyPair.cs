namespace YobitClient.Models
{
    public class CurrencyPair
    {
        public CurrencyPair(string baseSymbol, string counterSymbol)
        {
            BaseSymbol = baseSymbol;
            CounterSymbol = counterSymbol;
        }

        public string BaseSymbol { get; }
        public string CounterSymbol { get; }

        public override string ToString()
        {
            return $"{BaseSymbol}_{CounterSymbol}".ToLower();
        }
    }
}