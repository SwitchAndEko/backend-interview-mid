using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Interview.API.Filters
{
    public class SwaggerExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(AddUserRequest))
            {
                SetExamples(schema, addOnly: true);
            }
            else if (context.Type == typeof(UpdateUserRequest))
            {
                SetExamples(schema, addOnly: false);
            }
        }

        private static void SetExamples(OpenApiSchema schema, bool addOnly)
        {
            var examples = new Dictionary<string, IOpenApiAny>(StringComparer.OrdinalIgnoreCase)
            {
                ["acpd_Cname"] = new OpenApiString("王小明"),
                ["acpd_Ename"] = new OpenApiString("Wang Xiao Ming"),
                ["acpd_Sname"] = new OpenApiString("小明"),
                ["acpd_Email"] = new OpenApiString("xiaoming@example.com"),
                ["acpd_Status"] = new OpenApiInteger(1),
                ["acpd_Stop"] = new OpenApiBoolean(false),
                ["acpd_StopMemo"] = new OpenApiString(""),
                ["acpd_LoginID"] = new OpenApiString("xiaoming"),
                ["acpd_LoginPWD"] = new OpenApiString("P@ssw0rd123"),
                ["acpd_Memo"] = new OpenApiString("測試用帳號"),
            };

            if (!addOnly)
            {
                examples["acpd_UPDDateTime"] = new OpenApiDateTime(DateTimeOffset.Now);
                examples["acpd_UPDID"] = new OpenApiString("admin");
            }

            foreach (var (key, prop) in schema.Properties)
            {
                var match = examples.Keys.FirstOrDefault(k => k.Equals(key, StringComparison.OrdinalIgnoreCase));
                if (match is not null)
                {
                    prop.Example = examples[match];
                }
            }
        }
    }
}
