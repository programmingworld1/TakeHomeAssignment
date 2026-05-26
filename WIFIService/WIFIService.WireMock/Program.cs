using WireMock.Handlers;
using WireMock.Server;
using WireMock.Settings;

var stubsPath = Path.Combine(AppContext.BaseDirectory, "Stubs");

var networkInfrastructureServer = WireMockServer.Start(new WireMockServerSettings
{
    Port = 9001,
    ReadStaticMappings = true,
    FileSystemHandler = new LocalFileSystemHandler(Path.Combine(stubsPath, "NetworkInfrastructure"))
});

var networkControllerServer = WireMockServer.Start(new WireMockServerSettings
{
    Port = 9002,
    ReadStaticMappings = true,
    FileSystemHandler = new LocalFileSystemHandler(Path.Combine(stubsPath, "NetworkController"))
});

Console.WriteLine("Network Infrastructure API mock running on http://localhost:9001");
Console.WriteLine("Network Controller API mock running on http://localhost:9002");
Console.WriteLine("Press Ctrl+C to stop...");

await Task.Delay(Timeout.Infinite);
