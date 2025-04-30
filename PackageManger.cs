namespace S2PK;

using System.IO.Compression;

public static class PackageManager
{
    const string PackageExtension = ".package";
    const string S2pkExtension = ".s2pk";
    const string S3pkExtension = ".s3pk";

    /// <summary>
    /// Returns the extension for the package file.
    /// </summary>
    /// <param name="ts3">Whether to use the Sims 3 extension.</param>
    /// <returns>The extension for the package file.</returns>
    static string ExtensionHandler(bool ts3 = false) => ts3 ? S3pkExtension : S2pkExtension;

    /// <summary>
    /// Packs a directory into a package file.
    /// </summary>
    /// <param name="source">The source directory.</param>
    /// <param name="package">The output package file.</param>
    public static void PackPackages(string source, string package, bool ts3 = false)
    {
        var dir = new DirectoryInfo(source);
        var file = new FileInfo(package);
        var extension = ExtensionHandler(ts3);

        if (!dir.Exists)
        {
            Console.Error.WriteLine("Source directory does not exist.");
            return;
        }

        var packageFiles = dir.GetFiles($"*{PackageExtension}", SearchOption.AllDirectories);
        if (packageFiles.Length == 0)
        {
            Console.Error.WriteLine("No .package files found to pack.");
            return;
        }

        using var archive = ZipFile.Open(file.FullName, ZipArchiveMode.Create);
        foreach (var zip in packageFiles)
        {
            var entryPath = Path.GetRelativePath(file.FullName, zip.FullName);
            archive.CreateEntryFromFile(zip.FullName, entryPath);
            Console.WriteLine($"Packed: {entryPath}");
        }

        Console.WriteLine($"Created {file.FullName} with {packageFiles.Length} files.");
    }

    /// <summary>
    /// Unpacks a package file into a destination directory.
    /// </summary>
    /// <param name="package">The path to the package file.</param>
    /// <param name="destination">The destination directory. If not provided, the destination is read from the configuration file.</param>
    /// <param name="ts3">Whether to unpack for The Sims 3.</param>
    public static void UnpackPackages(string package, string destination = "", bool ts3 = false)
    {
        var extension = ExtensionHandler(ts3);

        // If destination is not provided, read from configuration file
        if (string.IsNullOrEmpty(destination))
        {
            var config = Config.Load("s2pk.toml"); // TODO: Config file should be in home directory

            // If The Sims 3 is not specified, use the default destination
            if (!ts3)
                destination = config.Paths.Sims2;
            else
                destination = config.Paths.Sims3;
        }

        // Check if destination directory exists
        if (!Directory.Exists(destination))
        {
            Console.Error.WriteLine("Destination directory does not exist.");
            return;
        }


        var file = new FileInfo(package);
        var dir = new DirectoryInfo(destination);

        if (!file.Exists)
        {
            Console.Error.WriteLine("Package file does not exist.");
            return;
        }

        // Check for extension mismatch
        if (file.FullName.Contains(S3pkExtension) && !ts3)
        {
            Console.Error.WriteLine("Package is for The Sims 3 but unpacking for The Sims 2.");
            return;
        }

        dir.Create(); // Ensures directory exists

        using var archive = ZipFile.OpenRead(file.FullName);
        foreach (var entry in archive.Entries)
        {
            var fullPath = Path.Combine(dir.FullName, entry.FullName);

            // Sanitize path to prevent directory traversal
            if (!fullPath.StartsWith(dir.FullName, StringComparison.OrdinalIgnoreCase))
            {
                Console.Error.WriteLine($"Skipping unsafe path: {entry.FullName}");
                continue;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            entry.ExtractToFile(fullPath, overwrite: true);
            Console.WriteLine($"Unpacked: {entry.FullName}");
        }

        Console.WriteLine($"Unpacked to {file.FullName}");
    }
}
