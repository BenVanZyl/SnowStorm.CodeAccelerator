using MediatR;
using SnowStorm.CodeBuilder.Infrastructure;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowStorm.CodeAccelerator.Services.Commands
{
    public  class CreateQueryCommand : IRequest<string>
    {
        public CodeGeneratorHelper Helper { get; set; }

        public CreateQueryCommand(CodeGeneratorHelper helper)
        {
            Helper = helper;
        }
    }

    public class CreateQueryCommandHandler : IRequestHandler<CreateQueryCommand, string>
    {
        private readonly AppDbConnection _dbInfo;
        private CodeGeneratorHelper _helper;

        public CreateQueryCommandHandler(IAppDbConnection dbInfo)
        {
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public async Task<string> Handle(CreateQueryCommand request, CancellationToken cancellationToken)
        {
            _helper = request.Helper;

            var value = new StringBuilder();

            value.AppendLine($"namespace {_helper.NameSpaceDomain}");
            value.AppendLine("{");
            value.AppendLine($"    public class Get{_helper.TableDomainName}Query : IQueryResultSingle<{_helper.TableDomainName}>");
            value.AppendLine("    {");
            value.AppendLine("      private readonly long _id;");
            value.AppendLine("");
            value.AppendLine($"      public Get{_helper.TableDomainName}Query(long id)");
            value.AppendLine("      {");
            value.AppendLine($"          _id = id;");
            value.AppendLine("      }");
            value.AppendLine("");
            value.AppendLine($"      public IQueryable<{_helper.TableDomainName}> Execute(IQueryableProvider queryableProvider)");
            value.AppendLine("      {");
            value.AppendLine($"          return queryableProvider.Query<{_helper.TableDomainName}>()");
            value.AppendLine($"             .Where(w => w.Id == _id);");
            value.AppendLine("      }");
            value.AppendLine("    }");
            value.AppendLine("}");
            value.AppendLine("");
            value.AppendLine("");
            value.AppendLine("**************************************************");
            value.AppendLine("");
            value.AppendLine("");
            value.AppendLine($"namespace {_helper.NameSpaceDomain}");
            value.AppendLine("{");
            value.AppendLine($"    public class Get{_helper.TableDomainName}sQuery : IQueryResultList<{_helper.TableDomainName}>");
            value.AppendLine("    {");
            value.AppendLine("      private readonly long _id;");
            value.AppendLine("");
            value.AppendLine($"      public IQueryable<{_helper.TableDomainName}> Execute(IQueryableProvider queryableProvider)");
            value.AppendLine("      {");
            value.AppendLine($"          return queryableProvider.Query<{_helper.TableDomainName}>()");
            value.AppendLine($"             .OrderBy(o => o.)");
            value.AppendLine($"             .AsQueryable();");
            value.AppendLine("      }");
            value.AppendLine("    }");
            value.AppendLine("}");

            return value.ToString();
        }


    }
}