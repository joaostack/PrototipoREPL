# PrototipoREPL

Um **REPL** (*Read-Eval-Print Loop*) simples e extensível para console/CLI, feito em **.NET 10 (C#)**. Permite executar comandos interativamente e carregar novos comandos em tempo de execução via DLLs externas (plugins).

---

## 📁 Estrutura da Solução

A solução possui três projetos:

1. **`PrototipoREPL.Contracts`** (Class Library)
   - Define a interface `ICommand` com `Name`, `Description` e `ExecuteAsync(string[] args)`.
   - É a referência usada por quem quiser criar plugins.

2. **`PrototipoREPL.Engine`** (Class Library)
   - Contém o `CommandRegistry`, que registra comandos e carrega DLLs externas usando `AssemblyLoadContext`.
   - Inclui os comandos integrados:
     - `help`: lista os comandos disponíveis.
     - `load <caminho-da-dll>`: carrega comandos de uma DLL externa.

3. **`PrototipoREPL`** (Console Application)
   - Ponto de entrada (`Program.cs`).
   - Registra os comandos padrão, mostra a interface de boas-vindas e mantém o loop interativo.
   - Usa a biblioteca **Spectre.Console** para exibir um banner e mensagens formatadas.
   - Sai da aplicação com o comando `exit`.

---

## 🚀 Como Executar

### Pré-requisitos
- [.NET 10.0 SDK](https://dotnet.microsoft.com/)

### Rodando o REPL
No diretório raiz da solução:

```bash
dotnet run --project PrototipoREPL/PrototipoREPL.csproj
```

Você verá um banner em ASCII "REPL" e o prompt:

```text
> 
```

---

## 🕹️ Comandos Disponíveis

| Comando | Descrição |
| :--- | :--- |
| `help` | Lista todos os comandos registrados. |
| `load <caminho>` | Carrega comandos de uma DLL externa. |
| `exit` | Encerra o REPL. |

---

## 🧩 Criando e Carregando Plugins

1. Crie uma biblioteca de classes:
   ```bash
   dotnet new classlib -n MeusComandos
   ```
2. Adicione a referência ao `PrototipoREPL.Contracts`:
   ```bash
   dotnet add MeusComandos reference PrototipoREPL.Contracts/PrototipoREPL.Contracts.csproj
   ```
3. Implemente `ICommand`:
   ```csharp
   using PrototipoREPL.Contracts;

   namespace MeusComandos;

   public class OlaMundoCommand : ICommand
   {
       public string Name => "ola";
       public string Description => "Exibe uma saudação personalizada.";

       public Task ExecuteAsync(string[] args)
       {
           string nome = args.Length > 0 ? string.Join(" ", args) : "Mundo";
           Console.WriteLine($"Olá, {nome}!");
           return Task.CompletedTask;
       }
   }
   ```
4. Compile:
   ```bash
   dotnet build MeusComandos
   ```
5. No REPL, carregue a DLL:
   ```text
   > load ./MeusComandos/bin/Debug/net10.0/MeusComandos.dll
   > ola Fulano
   Olá, Fulano!
   ```

---

*Documentação gerada pelo modelo de IA **DeepSeek**.*
