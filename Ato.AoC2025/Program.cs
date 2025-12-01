using Microsoft.Extensions.DependencyInjection;
using Ato.AoC2025;
using System.Reflection;

using var cancellationSource = new CancellationTokenSource();

await using var serviceProvider = BuildServiceProvider(args);

var problemTarget = args.FirstOrDefault();

var problems = serviceProvider
    .GetServices<IProblem>()
    .ToDictionary(
        x => $"{x.GetType().Namespace} {x.GetType().Name}",
        x => x);

var items = problems.Keys.ToArray();
int selectedIndex = 0;

if (problemTarget is not null &&
    problems.FirstOrDefault(x => x.Key.Contains(problemTarget, StringComparison.OrdinalIgnoreCase)).Value is { } targetProblem)
{
    await SolveProblem(targetProblem);
    return;
}

while (!cancellationSource.Token.IsCancellationRequested)
{
    Console.Clear();
    for (int i = 0; i < items.Length; i++)
    {
        if (i == selectedIndex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"> {items[i]}");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"  {items[i]}");
        }
    }

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            selectedIndex = (selectedIndex == 0) ? items.Length - 1 : selectedIndex - 1;
            break;

        case ConsoleKey.DownArrow:
            selectedIndex = (selectedIndex == items.Length - 1) ? 0 : selectedIndex + 1;
            break;

        case ConsoleKey.Enter:
            var problem = problems[items[selectedIndex]];
            await SolveProblem(problem);

            goto case ConsoleKey.Escape;

        case ConsoleKey.Escape:
            cancellationSource.Cancel();
            break;
    }
}

static ServiceProvider BuildServiceProvider(string[] args)
{
    IServiceCollection services = new ServiceCollection();

    foreach (var problem in typeof(Program).Assembly.GetTypes().Where(x => !x.IsAbstract && x.IsClass && typeof(IProblem).IsAssignableFrom(x)))
    {
        services.AddSingleton(typeof(IProblem), problem);
    }

    return services.BuildServiceProvider();
}

static async Task SolveProblem(IProblem targetProblem)
{
    var input = string.Empty;

    var inputStream = Assembly
        .GetExecutingAssembly()
        .GetManifestResourceStream($"{targetProblem.GetType().Namespace}.input.txt");

    if (inputStream is not null)
    {
        using var reader = new StreamReader(inputStream);
        input = await reader.ReadToEndAsync();
    }

    var soultion1 = targetProblem.Solve1(input);
    Console.WriteLine(soultion1);

    var soultion2 = targetProblem.Solve2(input);
    Console.WriteLine(soultion2);
}