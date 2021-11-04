using MediatR;
using SnowStorm.CodeBuilder.Infrastructure;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowStorm.CodeAccelerator.Services.Commands
{
    public class CreateDtoObjectCommand : IRequest<string>
    {
        public CodeGeneratorHelper Helper { get; set; }

        public CreateDtoObjectCommand(CodeGeneratorHelper helper)
        {
            Helper = helper;
        }
    }

    public class CreateDtoObjectCommandHandler : IRequestHandler<CreateDtoObjectCommand, string>
    {
        private readonly AppDbConnection _dbInfo;
        private CodeGeneratorHelper _helper;

        public CreateDtoObjectCommandHandler(IAppDbConnection dbInfo)
        {
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public async Task<string> Handle(CreateDtoObjectCommand request, CancellationToken cancellationToken)
        {
            _helper = request.Helper;

            var value = new StringBuilder();

            value.AppendLine($"namespace {_helper.NameSpaceDomain}");
            value.AppendLine("{");
            value.AppendLine($"    public class {_helper.TableDomainName}Dto");
            value.AppendLine("    {");
            value.AppendLine(AllColumns());
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