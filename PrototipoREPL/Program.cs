using PrototipoREPL.Contracts;
using PrototipoREPL.Engine;
using PrototipoREPL.Engine.Commands;
using Spectre.Console;

var registry = new CommandRegistry();

registry.Register(new HelpCommand(registry));
registry.Register(new LoadAssemblyCommand(registry));

AnsiConsole.Write(new FigletText("REPL").Color(Color.Magenta));

AnsiConsole.Write(
    new Rule("[grey]Interactive Shell[/]")
        .RuleStyle("magenta")
);

AnsiConsole.MarkupLine(
    "[grey]Digite[/] [bold]help[/] [grey]para listar os comandos ou[/] [bold]exit[/] [grey]para sair.[/]"
);

while (true)
{
    AnsiConsole.Markup("\n[bold white]>[/] ");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
    {
        AnsiConsole.MarkupLine("[grey]Encerrando REPL...[/]");
        break;
    }

    string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string commandName = parts[0];
    string[] commndArgs = parts.Skip(1).ToArray();

    if (registry.TryGet(commandName, out ICommand? command))
    {
        try
        {
            await command!.ExecuteAsync(commndArgs);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Erro durante execução]: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Comando desconhecido!");
    }
}
