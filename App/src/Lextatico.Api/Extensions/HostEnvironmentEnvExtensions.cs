using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Lextatico.Api.Extensions
{
    public static class HostEnvironmentEnvExtensions
    {
        public static bool IsLocalDevelopment(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
                throw new ArgumentNullException(nameof(hostEnvironment));

            return hostEnvironment.IsEnvironment("LocalDevelopment");
        }

        public static bool IsDocker()
        {
            var result = bool.TryParse(Environment.GetEnvironmentVariable("IS_DOCKER"), out var isDocker);

            return result && isDocker;
        }
    }
}
