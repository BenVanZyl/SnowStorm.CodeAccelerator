using MediatR;
using SnowStorm.CodeBuilder.Infrastructure;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowStorm.CodeAccelerator.Services.Commands
{
    public class CreateSaveCmdObjectCommand : IRequest<string>
    {
        public CodeGeneratorHelper Helper { get; set; }

        public CreateSaveCmdObjectCommand(CodeGeneratorHelper helper)
        {
            Helper = helper;
        }
    }

    public class CreateSaveCmdObjectCommandHandler : IRequestHandler<CreateSaveCmdObjectCommand, string>
    {
        private readonly AppDbConnection _dbInfo;
        private CodeGeneratorHelper _helper;

        public CreateSaveCmdObjectCommandHandler(IAppDbConnection dbInfo)
        {
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public async Task<string> Handle(CreateSaveCmdObjectCommand request, CancellationToken cancellationToken)
        {
            _helper = request.Helper;

            var value = new StringBuilder();

            value.AppendLine($"namespace {_helper.NameSpaceDomain}");
            value.AppendLine("{");
            value.AppendLine($"    public class Save{_helper.TableDomainName}Command : IRequest<{_helper.TableDomainName}>");
            value.AppendLine("    {");
            value.AppendLine($"     public {_helper.TableDomainName}Dto Data {_helper.GetSet}");
            value.AppendLine();
            value.AppendLine($"     public Save{_helper.TableDomainName}Command({_helper.TableDomainName}Dto data)");
            value.AppendLine("      {");
            value.AppendLine($"      Data = data;");
            value.AppendLine("      }");
            value.AppendLine("    }");
            value.AppendLine();
            value.AppendLine($"    public class Save{_helper.TableDomainName}CommandHandler : IRequestHandler<Save{_helper.TableDomainName}Command, bool>");
            value.AppendLine("    {");
            value.AppendLine($"     private readonly IQueryExecutor _executor;");
            value.AppendLine();
            value.AppendLine($"     public Save{_helper.TableDomainName}CommandHandler(IQueryExecutor executor)");
            value.AppendLine("      {");
            value.AppendLine($"         _executor = executor;");
            value.AppendLine("      }");
            value.AppendLine();
            value.AppendLine($"     public async Task<{_helper.TableDomainName}> Handle(Save{_helper.TableDomainName}Command request, CancellationToken cancellationToken)");
            value.AppendLine("      {");
            value.AppendLine($"         var value = await _executor.Get(new Get{_helper.TableDomainName}Query(request.Data.Id));");
            value.AppendLine($"         if (value == null)");
            value.AppendLine($"             value = await {_helper.TableDomainName}.Create(_executor, request.Data);");
            value.AppendLine($"         else");
            value.AppendLine($"             value.Save(request.Data);");
            value.AppendLine();
            value.AppendLine($"         await _executor.Save();");
            value.AppendLine();
            value.AppendLine($"         return value;");
            value.AppendLine($"         ");
            value.AppendLine($"                 //throw new ThisAppExecption(StatusCodes.Status400BadRequest, \"Record not found.\"");
            value.AppendLine();
            value.AppendLine($"         ");
            value.AppendLine($"         ");
            value.AppendLine("      }");
            value.AppendLine("    }");

            value.AppendLine("}");

            return value.ToString();
        }

        private string AllColumns()
        {
            var v = new StringBuilder();

            foreach (var col in _helper.Columns)
            {
                if (col.ScriptSkipColumn)
                    continue;

                if (_helper.PrimaryKeys != null && _helper.PrimaryKeys.Contains(col.COLUMN_NAME))
                    continue;

                v.AppendLine($"        public {col.ApiDataTypeNullable} {col.ApiColumnName}{_helper.GetSet}");
            }

            return v.ToString();
        }
    }
}
