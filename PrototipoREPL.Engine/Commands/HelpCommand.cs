using PrototipoREPL.Contracts;

namespace PrototipoREPL.Engine.Commands;

public class HelpCommand(CommandRegistry registry) : ICommand
{
    public string Name => "help";
    public string Description => "Lista todos os comandos disponíveis.";

    public Task ExecuteAsync(string[] ars)
    {
        Console.WriteLine("\nComandos disponíveis:");
        foreach (var cmd in registry.GetAll())
        {
            Console.WriteLine($"  {cmd.Name,-12} - {cmd.Description}");
        }
        Console.WriteLine();
        return Task.CompletedTask;
    }
}