using S2Pkg;
using System.CommandLine;

var rootCommand = new RootCommand("The Sims 2 .s2pk Package Manager");

var sourceOption = new Option<string>(
    aliases: ["--source", "-s"],
    description: "Source directory containing .package files")
{ IsRequired = true };

var outputOption = new Option<string>(
    aliases: ["--output", "-o"],
    description: "Destination .s2pk archive file")
{ IsRequired = true };

// Pack command
var packCommand = new Command("pack", "Packs .package files into a .s2pk archive")
{
    sourceOption,
    outputOption
};

packCommand.SetHandler((source, output) =>
{
    PackageManager.PackPackages(source, output);
}, sourceOption, outputOption);

var packageOption = new Option<string>(
    aliases: ["--package", "-p"],
    description: "Input .s2pk archive")
{ IsRequired = true };

var destinationOption = new Option<string>(
    aliases: ["--destination", "-d"],
    description: "Destination directory (e.g., The Sims 2 Downloads folder)")
{ IsRequired = true };

// Unpack command
var unpackCommand = new Command("unpack", "Unpacks a .s2pk archive to the Downloads folder")
{
    packageOption,
    destinationOption
};

unpackCommand.SetHandler((package, destination) =>
{
    PackageManager.UnpackPackages(package, destination);
}, packageOption, destinationOption);

// Register commands
rootCommand.AddCommand(packCommand);
rootCommand.AddCommand(unpackCommand);

// Start CLI
return rootCommand.Invoke(args);
