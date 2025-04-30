namespace S2PK;

using System.IO.Compression;

public static class PackageManager
{
    const string PackageExtension = ".package";
    const string S2pkExtension = ".s2pk";

    /// <summary>
    /// Packs a directory into a package file.
    /// </summary>
    /// <param name="source">The source directory.</param>
    /// <param name="output">The output package file.</param>
    public static void PackPackages(string source, string output)
    {
        var dir = new DirectoryInfo(source);
        var file = new FileInfo(output);

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
    public static void UnpackPackages(string package, string destination = "")
    {
        if (string.IsNullOrEmpty(destination))
        {
            var config = Config.Load("s2pk.toml"); // TODO: Config file should be in application directory
            destination = config.Destination;
        }

        var file = new FileInfo(package);
        var dir = new DirectoryInfo(destination);

        if (!file.Exists)
        {
            Console.Error.WriteLine("Package file does not exist.");
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
