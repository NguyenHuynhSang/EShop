using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
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

    public static class SwaggerExtension
    {
        public static string DescriptionFromFile(string fileName)
        {
            var path = "/Doc/" + fileName;
            var description = File.ReadAllText(path);
            return description;

        }    

    }




    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SwaggerOperationCustomAttribute: SwaggerOperationAttribute
    {

        public string FileName
        {
            get { return FileName; }   // get method
            set {
                var description = File.ReadAllText(@"./Doc/"+value);
                Description = description; }  // set method
        }
        public SwaggerOperationCustomAttribute(string summary = null, string descriptionpath = null) 
        {

           

        }

    }

}
