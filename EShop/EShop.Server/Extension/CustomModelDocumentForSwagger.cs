using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace EShop.Server.Extension
{
    public class CustomModelDocumentForSwagger<T> : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter where T : class
    {

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(T), context.SchemaRepository);
        }
      
      
    }

}
