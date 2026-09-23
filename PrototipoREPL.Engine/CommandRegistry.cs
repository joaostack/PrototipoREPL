using System.Reflection;
using System.Runtime.Loader;
using PrototipoREPL.Contracts;

namespace PrototipoREPL.Engine;

public class CommandRegistry
{
    private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);

    public void Register(ICommand command)
    {
        _commands[command.Name] = command;
    }

    public int LoadFromAssembly(string assemblyPath)
    {
        string fullPath = Path.GetFullPath(assemblyPath);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Assembly nõa encontrado => {fullPath}");

        var loadContext = new AssemblyLoadContext(Path.GetFileNameWithoutExtension(fullPath), isCollectible: true);
        Assembly assembly = loadContext.LoadFromAssemblyPath(fullPath);
        var commandTypes = assembly.GetTypes()
            .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        int loadedCount = 0;

        foreach (var type in commandTypes)
        {
            if (Activator.CreateInstance(type) is ICommand command)
            {
                Register(command);
                loadedCount++;
            }
        }

        return loadedCount;
    }
    
    public bool TryGet(string name, out ICommand? command) => _commands.TryGetValue(name, out command);
    
    public IEnumerable<ICommand> GetAll() => _commands.Values;
}