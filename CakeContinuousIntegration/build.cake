#tool "nuget:?package=NUnit.ConsoleRunner"
// Arguments
var target = Argument("target", "Default");
var buildId = Argument("buildId","0");
var zipDir = Argument("zipDir","./");
var configuration = Argument("configuration", "Release");

// Define directories
var buildDir = Directory("./bin") + Directory(configuration);
var appName = "CISample";
var version = "1.0.4";
var informationalVersion = string.Concat(version + "-dev" + buildId);

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

Task("Create-AssemblyInfo")
    .Does(() =>
{
    var file = "./src/CISample/Properties/AssemblyInfo.cs";
    
    CreateAssemblyInfo(file, new AssemblyInfoSettings {
        Title = "CISample",
        Description = "Universal Windows Platform App",
        Product = "CISample",
        Version = version,
        FileVersion = version,
        InformationalVersion = informationalVersion,
        Copyright = string.Format("Copyright (c) Dachi Gogotchuri - {0}", DateTime.Now.Year)
    });
});

Task("Build")
    .IsDependentOn("Create-AssemblyInfo")
    .Does(() =>
{
    if (IsRunningOnWindows())
    {
        DotNetBuild("./src/CISample.sln");
    }
    else
    {
        XBuild("./src/Example.sln", settings =>
            settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/*.UnitTests.dll", new NUnit3Settings {NoResults = true});
});

Task("Zip-Package")
    .Does(() =>
{
    if (DirectoryExists(string.Concat(zipDir+"AppPackages")))
    {
        Zip(string.Concat(zipDir+"AppPackages"), string.Concat(zipDir+"drop.zip"));
    }
    else
    {
        throw new Exception(string.Concat("AppPackages not exists in the current context --> "+zipDir));
    }
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);