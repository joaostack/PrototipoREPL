using PrototipoREPL.Contracts;
using Spectre.Console;

namespace PrototipoREPL.Engine.Commands;

public class HelpCommand(CommandRegistry registry) : ICommand
{
    public string Name => "help";
    public string Description => "Lista todos os comandos disponíveis.";

    public Task ExecuteAsync(string[] ars)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold cyan]Comando[/]")
            .AddColumn("[bold white]Descrição[/]");

        Console.WriteLine("\nComandos disponíveis:");
        foreach (var cmd in registry.GetAll())
        {
            table.AddRow(
               $"[green]{cmd.Name}[/]",
               cmd.Description
           );
        }
        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        return Task.CompletedTask;
    }
}