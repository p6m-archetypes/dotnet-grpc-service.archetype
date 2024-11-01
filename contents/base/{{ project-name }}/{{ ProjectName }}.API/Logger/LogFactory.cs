namespace {{ ProjectName }}.API.Logger;

public class LogFactory
{
    public static Serilog.ILogger CreateLogger(string name)
    {
        return Serilog.Log.ForContext("SourceContext", name);
    }
}