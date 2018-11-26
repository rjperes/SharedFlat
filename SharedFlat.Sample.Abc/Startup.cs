using Microsoft.AspNetCore.Hosting;
using System;

[assembly: HostingStartup(typeof(SharedFlat.Sample.Abc.Startup))]

namespace SharedFlat.Sample.Abc
{
    public class Startup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ToString();
        }
    }
}
