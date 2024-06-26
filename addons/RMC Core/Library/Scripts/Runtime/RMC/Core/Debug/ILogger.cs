namespace RMC.Core.Debug
{
    public interface ILogger
    {
        bool IsEnabled { get; set; }
        string Prefix { get; set; }
        string Suffix { get; set; }
        void Print(string message);
        void PrintErr(string message);
        void PrintHeader(string message);
        void PrintDivider();

        void PrintEmptyLine();
    }
}