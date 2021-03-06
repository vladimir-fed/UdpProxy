using Microsoft.Extensions.Options;

using Proxy;

using System.Net;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
	.AddEnvironmentVariables()
	.Build();

builder.Services
	.Configure<UdpOptions>(configuration.GetSection(nameof(UdpOptions)))
	.AddHostedService<UdpHostedService>();

var app = builder.Build();

app.MapGet("/", (IOptions<UdpOptions> options) => string.Join(Environment.NewLine, Dns.GetHostAddresses(options.Value.MyDomain).Select(x => x.ToString())));

app.Run();

