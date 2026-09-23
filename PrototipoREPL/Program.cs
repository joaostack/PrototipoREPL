using PrototipoREPL.Contracts;
using PrototipoREPL.Engine;
using PrototipoREPL.Engine.Commands;

var registry = new CommandRegistry();

registry.Register(new HelpCommand(registry));
registry.Register(new LoadAssemblyCommand(registry));

Console.WriteLine("=== REPL CLI Inicializado (digite 'help' para comandos ou 'exit' para sair) ===");

while (true)
{
    Console.Write("\n> ");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
        break;

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