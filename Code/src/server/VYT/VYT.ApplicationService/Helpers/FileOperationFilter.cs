using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace VYT.ApplicationService.Helpers
{
    public class FileOperationFilter : IOperationFilter
    {      
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.operationId.ToLower() == "job_createjobfromfile")
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>(2);
                else
                    operation.parameters.Clear();
                operation.parameters.Add(new Parameter
                {
                    name = "jobFile",
                    @in = "formData",
                    description = "Upload File",
                    required = true,
                    type = "file"
                });
                
                operation.parameters.Add(new Parameter
                {
                    name = "languages",
                    @in = "formData",
                    description = "OCR Languages",
                    required = true,
                    type = "string"
                });
                
                operation.consumes.Add("application/form-data");
            }
            else if (operation.operationId.ToLower() == "job_addjobfile")
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>(3);
                else
                    operation.parameters.Clear();
                operation.parameters.Add(new Parameter
                {
                    name = "jobFile",
                    @in = "formData",
                    description = "Upload File",
                    required = true,
                    type = "file"
                });
                
                operation.parameters.Add(new Parameter
                {
                    name = "jobId",
                    @in = "formData",
                    description = "Job Id",
                    required = true,
                    type = "string"
                });

                operation.parameters.Add(new Parameter
                {
                    name = "jobFileType",
                    @in = "formData",
                    description = "Job File Type",
                    required = true,
                    type = "string"
                });
               
                operation.consumes.Add("application/form-data");
            }
        }
    }
}