// Arguments
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// Define directories
var buildDir = Directory("./bin") + Directory(configuration);
#tool "nuget:?package=NUnit.ConsoleRunner"

// Tasks
Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-Nuget-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./src/CISample.sln");
});

Task("Build")
    .IsDependentOn("Restore-Nuget-Packages")
    .Does(() =>
{
    if (IsRunningOnWindows())
    {
        MSBuild("./src/CISample.sln", settings =>
            settings.SetConfiguration(configuration));
    }
    else
    {
        XBuild("./src/Example.sln", settings =>
            settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/*.UnitTests.dll", new NUnit3Settings {NoResults = true});
});

Task("NetCore-PackPackage")
    .Does(() =>
{
    // var settings = new DotNetCorePackSettings
    // {
    //     Frameworks = new[] { "dnx451", "dnxcore50" },
    //     Configurations = new[] { "Debug", "Release" },
    //     OutputDirectory = "./artifacts/"
    // };
            
    //DotNetCorePack("./src/*");
});

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);