using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Demo.ApiDesign.Cache
{

    /// <content>
    /// Provides the implementation for ASP.NET Core.
    /// </content>
    public class CustomMediaTypeApiVersionReader: IApiVersionReader
    {

        /// <summary>
        /// Reads the service API version value from a request.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest">HTTP request</see> to read the API version from.</param>
        /// <returns>The raw, unparsed service API version value read from the request or <c>null</c> if request does not contain an API version.</returns>
        /// <exception cref="AmbiguousApiVersionException">Multiple, different API versions were requested.</exception>
        public string Read(HttpRequest request)
        {

            string versionNumber = "1.0";
            // Comment the code that gets the version number from Query String
            // var versionQueryString =
            //         HttpUtility.ParseQueryString(request.RequestUri.Query);
            //if (versionQueryString["v"] != null)
            //{
            //    versionNumber = versionQueryString["v"];
            //}

            // Get the version number from Custom version header

            //string customHeader = "X-StudentService-Version";
            //if (request.Headers.Contains(customHeader))
            //{
            //    // If X-StudentService-Version:1 is specified twice in the request
            //    // then in versionNumber variable will get a value of "1,1"
            //    versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();
            //    // Check if versionNumber string contains a comma, and take only
            //    // the first number from the comma separated list of version numbers
            //    if (versionNumber.Contains(","))
            //    {
            //        versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(","));
            //    }
            //}

            // Get the version number from the Accept header

            // Users can include multiple Accept headers in the request
            // Check if any of the Accept headers has a parameter with name version
            //var acceptHeader = request.Headers.Accept.Where(a => a.Parameters
            //                    .Count(p => p.Name.ToLower() == "version") > 0);

            //// If there is atleast one header with a "version" parameter
            //if (acceptHeader.Any())
            //{
            //    // Get the version parameter value from the Accept header
            //    versionNumber = acceptHeader.First().Parameters
            //                    .First(p => p.Name.ToLower() == "version").Value;
            //}

            // Get the version number from the Custom media type

            // Use regular expression for mataching the pattern of the media
            // type. We have given a name for the matched group that contains
            // the version number. This enables us to retrieve the version number 
            // using the group name("version") instead of ZERO based index
            //string regex = @"application\/vnd\.([a-z]+)\.v(?<version>[0-9]+)\+([a-z]+)";
            string regex = @"application\/vnd\.([a-z]+)\.v(?<version>[0-9]+)";

            // Users can include multiple Accept headers in the request.
            // Check if any of the Accept headers has our custom media type by
            // checking if there is a match with regular expression specified
            var acceptHeader = request.Headers["Accept"].Where(a => Regex.IsMatch(a, regex, RegexOptions.IgnoreCase));
            // If there is atleast one Accept header with our custom media type
            if (acceptHeader.Any())
            {
                // Retrieve the first custom media type
                var match = Regex.Match(acceptHeader.First(), regex, RegexOptions.IgnoreCase);
                // From the version group, get the version number
                versionNumber = match.Groups["version"].Value;
            }

            if (versionNumber == "1")
            {
                return "1.0";
            }
            else
            {
                return "2.0";
            }

        }

        /// <summary>
        /// Provides API version parameter descriptions supported by the current reader using the supplied provider.
        /// </summary>
        /// <param name="context">The <see cref="IApiVersionParameterDescriptionContext">context</see> used to add API version parameter descriptions.</param>
        public virtual void AddParameters(IApiVersionParameterDescriptionContext context)
        {
            //Arg.NotNull(context, nameof(context));
            context.AddParameter("v", ApiVersionParameterLocation.MediaTypeParameter);
        }
    }
}
