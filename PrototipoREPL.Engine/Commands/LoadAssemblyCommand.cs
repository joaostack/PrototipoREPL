using PrototipoREPL.Contracts;

namespace PrototipoREPL.Engine.Commands;

public class LoadAssemblyCommand(CommandRegistry registry) : ICommand
{
    public string Name => "load";
    public string Description => "Carrega comandos de um assembly externo. Uso: load <caminho-da-dll>";
    
    public Task ExecuteAsync(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Erro: Informe o caminho do arquivo DLL.");
            return Task.CompletedTask;
        }
    
        try
        {
            int count = registry.LoadFromAssembly(args[0]);
            Console.WriteLine($"Sucesso: {count} comando(s) carregado(s).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Falha ao carregar assembly: {ex.Message}");
        }
    
        return Task.CompletedTask;
    }

}