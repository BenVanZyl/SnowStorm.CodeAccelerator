using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Dto
{
    public class ExportDefinition
    {
        public enum ExportTypeDefinition
        {
            ApiDomain,
            ApiDomainMapping,
            ApiDomainReferences,
            ApiDomainMethods,
            ClientDto,
            ApiGlobalAsax,
            ApiQuerySingle,
            ApiQueryList,
            ApiController,
            ClientApiSingle,
            ClientApiList,
            TestsApi
        }

        public bool IsSelected { get; set; }
        public ExportTypeDefinition ExportType { get; set; }
        public string ClassGroup { get; set; }
        public string ClassName { get; set; }
        public string Code { get; set; }
        public string FilePathRoot { get; set; }
        public string DirectoryPath
        {
            get
            {
                var path = "";

                FilePathRoot = FilePathRoot.EndsWith("\\") ? FilePathRoot : $"{FilePathRoot}\\";

                switch (ExportType)
                {
                    case ExportTypeDefinition.ApiDomain:
                        path = $"Domain\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ApiDomainMapping:
                        path = $"Domain\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ApiDomainReferences:
                        path = $"Domain\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ApiDomainMethods:
                        path = $"Domain\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ClientDto:
                        path = $"Api\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ApiGlobalAsax:
                        path = $"";
                        break;
                    case ExportTypeDefinition.ApiQuerySingle:
                        path = $"Queries\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ApiQueryList:
                        path = $"Queries\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ApiController:
                        path = $"Api\\{ClassGroup}\\";
                        break;
                    case ExportTypeDefinition.ClientApiSingle:
                        path = $"Api\\{ClassGroup}\\Queries\\";
                        break;
                    case ExportTypeDefinition.ClientApiList:
                        path = $"Api\\{ClassGroup}\\Queries\\";
                        break;
                    case ExportTypeDefinition.TestsApi:
                        path = $"IntegrationTests\\";
                        break;
                    default:
                        break;
                }

                return $"{FilePathRoot}{path}";
            }
        }

        public string CodeFileName
        {
            get
            {
                var codeFile = "";

                switch (ExportType)
                {
                    case ExportTypeDefinition.ApiDomain:
                        codeFile = $"{ClassName}.cs";
                        break;
                    case ExportTypeDefinition.ApiDomainMapping:
                        codeFile = $"{ClassName}Mapping.cs";
                        break;
                    case ExportTypeDefinition.ApiDomainReferences:
                        codeFile = $"{ClassName}References.cs";
                        break;
                    case ExportTypeDefinition.ApiDomainMethods:
                        codeFile = $"{ClassName}Methods.cs";
                        break;
                    case ExportTypeDefinition.ClientDto:
                        codeFile = $"{ClassName}.cs";
                        break;
                    case ExportTypeDefinition.ApiGlobalAsax:
                        //codeFile = $"Global.asax";
                        break;
                    case ExportTypeDefinition.ApiQuerySingle:
                        codeFile = $"{ClassName}.cs";
                        break;
                    case ExportTypeDefinition.ApiQueryList:
                        codeFile = $"{ClassName}.cs";
                        break;
                    case ExportTypeDefinition.ApiController:
                        codeFile = $"{ClassName}.cs";
                        break;
                    case ExportTypeDefinition.ClientApiSingle:
                        codeFile = $"{ClassName}.cs";
                        break;
                    case ExportTypeDefinition.ClientApiList:
                        codeFile = $"{ClassName}.cs";
                        break;
                    case ExportTypeDefinition.TestsApi:
                        codeFile = $"{ClassGroup}ApiTest.cs";
                        break;
                    default:
                        break;
                }

                return codeFile;
            }
        }

        public string FilePath => $"{DirectoryPath}{CodeFileName}";

        public string VsProjectPath => FilePath.Replace(FilePathRoot, "");
    }
}
