var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage").RunAsEmulator();
var clusteringTable = storage.AddTables("clustering");
var grainStorage = storage.AddBlobs("grain-state");

var orleans = builder.AddOrleans("default")
    .WithClustering(clusteringTable)
    .WithGrainStorage("Default", grainStorage);

builder.AddProject<Projects.Silo>("silo")
    .WithReference(orleans)
    .WithReplicas(1);

builder.AddProject<Projects.Client>("frontend")
    .WithReference(orleans.AsClient())
    .WithExternalHttpEndpoints()
    .WithReplicas(1);

builder.Build().Run();