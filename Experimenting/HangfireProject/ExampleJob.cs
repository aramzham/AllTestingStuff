namespace HangfireProject;

public class ExampleJob(ILogger<ExampleJob> logger)
{
    private readonly ILogger<ExampleJob> _logger = logger;

    public void Execute()
    {
        _logger.LogInformation("ExampleJob executed");
    }
}