namespace PrototipoREPL.Contracts;

public interface ICommand
{
    string Name { get; }
    string Description { get; }
    Task ExecuteAsync(string[] ars);
}