using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Lextatico.Api.Configurations
{
    public class UrlPatterner : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            if (value is null) return null;

            var replacement=  Regex.Replace(value.ToString() ?? string.Empty,
                "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100)).ToLowerInvariant();

            return replacement;
        }
    }
}
