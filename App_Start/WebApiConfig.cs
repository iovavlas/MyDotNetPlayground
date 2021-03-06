﻿using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebApplication1.App_Start;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            AutofacConfig.Register();       // DI

            // Web API Versioning configuration
            config.AddApiVersioning(cfg =>
            {
                /* Without any configuration, we must include the version in every request --> ?api-version=1.0 */
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;                     // With that, we don't have to include the version every time...
                cfg.ReportApiVersions = true;                                       // With that, we get a new header "api-supported-versions" with the Response.
                //cfg.ApiVersionReader = new HeaderApiVersionReader("X-Version");   // Use this new custom header to specify the version, instead of using '?api-version' in the URL.
                cfg.ApiVersionReader = ApiVersionReader.Combine(                    // Use either a header or a new query string '?ver' in order to specify the version.
                    new HeaderApiVersionReader("X-Version"),
                    new QueryStringApiVersionReader("ver")
                );

            });

            // Web API routes
            // Enable Attribute routing.
            config.MapHttpAttributeRoutes();

            // Convention-based routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            /* return JSON data to the client, instead of XML. Use camelCase instead of PascalCase. */
            if (config.Formatters != null)
            {
                config.Formatters.Clear();
                config.Formatters.Add(new JsonMediaTypeFormatter());
                config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            }
        }
    }
}
