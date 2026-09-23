# PrototipoREPL

Um protótipo de **REPL** (*Read-Eval-Print Loop*) modular e extensível para console/CLI desenvolvido em **.NET 10 (C#)**. O projeto permite a execução interativa de comandos em linha de comando e suporta o carregamento dinâmico de novos comandos em tempo de execução através de assemblies (`.dll`) externos (sistema de plugins).

---

## 📁 Estrutura da Solução

A solução é dividida em três projetos principais:

1. **`PrototipoREPL.Contracts`** (Class Library):
   - Contém os contratos e interfaces compartilhadas do sistema.
   - Define a interface `ICommand`, que padroniza os comandos com as propriedades `Name`, `Description` e o método assíncrono `ExecuteAsync(string[] args)`.
   - Pode ser referenciada por bibliotecas de terceiros para criação de plugins/comandos externos.

2. **`PrototipoREPL.Engine`** (Class Library):
   - Responsável pelo núcleo de gerenciamento e execução dos comandos.
   - `CommandRegistry`: Gerencia o registro em memória dos comandos disponíveis e utiliza reflexão (`AssemblyLoadContext`) para carregar novos tipos que implementam `ICommand` dinamicamente a partir de arquivos `.dll`.
   - Comandos integrados (Built-in):
     - `help`: Lista todos os comandos atualmente registrados e suas descrições.
     - `load <caminho-da-dll>`: Carrega em tempo de execução comandos implementados em um assembly externo.

3. **`PrototipoREPL`** (Console Application):
   - Ponto de entrada da aplicação (`Program.cs`).
   - Inicializa o registro de comandos com os comandos padrão, exibe a interface de boas-vindas e mantém o loop interativo (`while`), processando entradas, argumentos e capturando exceções durante a execução.
   - Suporta a saída da aplicação através do comando `exit`.

---

## 🚀 Como Executar

### Pré-requisitos
- [.NET 10.0 SDK](https://dotnet.microsoft.com/) instalado.

### Executando o REPL
No diretório raiz da solução, execute o comando:

```bash
dotnet run --project PrototipoREPL/PrototipoREPL.csproj
```

Uma vez iniciado, você verá o prompt interativo:
```text
=== REPL CLI Inicializado (digite 'help' para comandos ou 'exit' para sair) ===

> 
```

---

## 🕹️ Comandos Disponíveis

| Comando | Descrição |
| :--- | :--- |
| `help` | Lista todos os comandos atualmente registrados no sistema. |
| `load <caminho>` | Carrega comandos adicionais a partir de uma DLL externa compilada. |
| `exit` | Encerra a aplicação REPL. |

---

## 🧩 Como Criar e Carregar Novos Comandos (Plugins)

1. Crie uma nova biblioteca de classes (.NET 10):
   ```bash
   dotnet new classlib -n MeusComandos
   ```
2. Adicione a referência ao projeto `PrototipoREPL.Contracts`:
   ```bash
   dotnet add MeusComandos reference PrototipoREPL.Contracts/PrototipoREPL.Contracts.csproj
   ```
3. Implemente a interface `ICommand`:
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
4. Compile a biblioteca:
   ```bash
   dotnet build MeusComandos
   ```
5. No REPL interativo, carregue a DLL gerada:
   ```text
   > load ./MeusComandos/bin/Debug/net10.0/MeusComandos.dll
   Sucesso: 1 comando(s) carregado(s).
   > ola Fulano
   Olá, Fulano!
   ```

---

*Esta documentação foi gerada pelo modelo de IA Gemini Flash 3.8.*
