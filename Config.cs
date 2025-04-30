namespace S2PK;

public class Config
{
    public string Destination { get; set; } = string.Empty;

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
