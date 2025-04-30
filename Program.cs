using System.CommandLine;

var rootCommand = new RootCommand("The Sims 2 .s2pk Package Manager");

var sims3option = new Option<bool>(
    aliases: ["--ts3"],
    description: "Switch to The Sims 3 mode.",
    getDefaultValue: () => false);

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

packCommand.SetHandler((source, output, sims3) =>
{
    PackageManager.PackPackages(source, output, sims3);
}, sourceOption, outputOption, sims3option);

var packageOption = new Option<string>(
    aliases: ["--package", "-p"],
    description: "Input .s2pk archive")
{ IsRequired = true };

var destinationOption = new Option<string>(
    aliases: ["--destination", "-d"],
    description: "Destination directory (e.g., The Sims 2 Downloads folder)");

// Unpack command
var unpackCommand = new Command("unpack", "Unpacks a .s2pk archive to the Downloads folder")
{
    packageOption,
    destinationOption
};

unpackCommand.SetHandler((package, destination, sims3) =>
{
    PackageManager.UnpackPackages(package, destination, sims3);
}, packageOption, destinationOption, sims3option);

// Register commands
rootCommand.AddCommand(packCommand);
rootCommand.AddCommand(unpackCommand);

// Start CLI
return rootCommand.Invoke(args);
