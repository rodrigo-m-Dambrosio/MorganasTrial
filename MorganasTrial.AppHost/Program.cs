var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MorganasTrialAPI>("morganastrialapi");

builder.AddProject<Projects.UmbracoProject>("umbracoproject");

builder.Build().Run();
