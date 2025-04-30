namespace S2PK;

public class Config
{
    public Destination Games { get; set; } = new();

    /// <summary>
    /// Loads the configuration from the specified file path.
    /// </summary>
    /// <param name="path">The path to the configuration file.</param>
    /// <returns>The loaded configuration.</returns>
    public static Config Load(string path)
    {
        var file = File.ReadAllText(path);
        var config = Toml.ToModel<Config>(file);
        return config;
    }
}

public class Destination
{
    public string Sims2 { get; set; } = string.Empty;
    public string Sims3 { get; set; } = string.Empty;
}
