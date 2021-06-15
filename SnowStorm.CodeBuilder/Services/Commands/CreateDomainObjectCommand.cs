using MediatR;
using SnowStorm.CodeBuilder.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Services.Commands
{
    public class CreateDomainObjectCommand : IRequest<string>
    {
        public  CodeGeneratorHelper Helper { get; set; }

        public CreateDomainObjectCommand(CodeGeneratorHelper helper)
        {
            Helper = helper;
        }
    }

    public class CreateDomainObjectCommandHandler : IRequestHandler<CreateDomainObjectCommand, string>
    {
        private readonly AppDbConnection _dbInfo;
        private CodeGeneratorHelper _helper;

        public CreateDomainObjectCommandHandler(IAppDbConnection dbInfo)
        {
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public async Task<string> Handle(CreateDomainObjectCommand request, CancellationToken cancellationToken)
        {
            _helper = request.Helper;

            var isPk = "";
            var isFk = "";
            var isPkId = "";
            var doAuditColumns = false;
            var value = new StringBuilder();


            value.AppendLine("using Microsoft.EntityFrameworkCore;");
            value.AppendLine("using Microsoft.EntityFrameworkCore.Metadata.Builders;");
            value.AppendLine("using SnowBird.Web.Client.Dto.Shopify;");
            value.AppendLine("using SnowStorm.Infrastructure.Domain;");
            value.AppendLine("using SnowStorm.Infrastructure.QueryExecutors;");
            value.AppendLine("using System;");
            value.AppendLine("using System.Threading.Tasks;");
            value.AppendLine("");
            value.AppendLine($"namespace {_helper.NameSpaceDomain}");
            value.AppendLine("{");
            value.AppendLine($"    public partial class {_helper.TableDomainName} : IDomainEntity");
            value.AppendLine("    {");
            value.AppendLine($"        protected {_helper.TableDomainName}() {_helper.BracketsOnly}");

            //foreach (var col in _helper.Columns)
            //{
            //    isPkId = "";
            //    isPk = "";
            //    isFk = "";

            //    if (_helper.IsPrimaryKeyColumn(col.COLUMN_NAME))
            //    {
            //        if (_helper.PrimaryKeys != null
            //            && _helper.PrimaryKeys.Count == 1
            //            && col.ApiDataType == "long")
            //        {
            //            isPkId = "//";
            //            isPk = " // PK is Mapped to DomainEnitiy.Id";
            //            value = value.Replace("IDomainEntity", "DomainEntity");
            //            doAuditColumns = true;
            //        }
            //        else
            //        {
            //            isPkId = "";
            //            isPk = " // PK.";
            //        }
            //    }

            //    isFk = _helper.IsForeignKeyColumn(col.COLUMN_NAME) ? " // TODO: Foreign Key Column requires config." : "";

            //    if (doAuditColumns)
            //    {
            //        if (!_helper.IsAuditColumn(col.COLUMN_NAME))
            //            value.AppendLine($"        {isPkId}public {col.ApiDataTypeNullable} {col.ApiColumnName}{_helper.GetSetPrivate} {isPk}{isFk}");
            //    }
            //    else
            //        value.AppendLine($"        {isPkId}public {col.ApiDataTypeNullable} {col.ApiColumnName}{_helper.GetSetPrivate} {isPk}{isFk}");
            //}

            value.AppendLine();
            value.AppendLine(AllColumns());
            value.AppendLine();

            // TODO: Setup relationship objects
            value.AppendLine();
            value.AppendLine(ForeignKeys());
            value.AppendLine();

            value.AppendLine();
            value.AppendLine(Methods());
            value.AppendLine();

            value.AppendLine("    }");

            value.AppendLine();
            value.AppendLine(EntityConfig());
            value.AppendLine();

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

                v.AppendLine($"        public {col.ApiDataTypeNullable} {col.ApiColumnName}{_helper.GetSetPrivate}");
            }

            return v.ToString();
        }

        private string ForeignKeys()
        {
            if (_helper.ForeignKeys != null || _helper.ForeignKeys.Count == 0)
                return "";

            var v = new StringBuilder();

            foreach (var fk in _helper.ForeignKeys)
            {
                v.AppendLine($"        public OBJECT_{fk} FK_OBJECT_{fk}{_helper.GetSetPrivate}");
            }

            return v.ToString();
        }

        private string Methods()
        {
            var v = new StringBuilder();
            v.AppendLine("        #region Methods");
            v.AppendLine();
            v.AppendLine($"        internal static async Task<{_helper.TableDomainName}> Create(IQueryExecutor executor, {_helper.TableDomainName}Dto data, bool autoSave = true)");
            v.AppendLine("        {");
            v.AppendLine($"            if (data == null)");
            v.AppendLine($"                throw new Exceptioin(\"Create Failed due to missing data!: {_helper.TableDomainName}\")");
            v.AppendLine();
            v.AppendLine($"            var v = new {_helper.TableDomainName}(data);");
            v.AppendLine($"            await executor.Add<Member>(value);");
            v.AppendLine();
            v.AppendLine($"            if (autoSave)");
            v.AppendLine($"                await executor.Save();");
            v.AppendLine("        }");
            v.AppendLine();
            v.AppendLine($"        private {_helper.TableDomainName}({_helper.TableDomainName}Dto data)");
            v.AppendLine("        {");
            v.AppendLine($"            Save(data);");
            v.AppendLine("        }");
            v.AppendLine();
            v.AppendLine($"        public void Save({_helper.TableDomainName}Dto data)");
            v.AppendLine("        {");
            foreach (var col in _helper.Columns)
            {
                if (col.ScriptSkipColumn)
                    continue;
                if (_helper.PrimaryKeys != null && _helper.PrimaryKeys.Contains(col.COLUMN_NAME))
                    continue;
                v.AppendLine($"       SetSet{col.ApiColumnName}(data.{col.ApiColumnName});");
            }
            v.AppendLine();
            v.AppendLine("        }");
            v.AppendLine();
            foreach (var col in _helper.Columns)
            {
                if (col.ScriptSkipColumn)
                    continue;
                if (_helper.PrimaryKeys != null && _helper.PrimaryKeys.Contains(col.COLUMN_NAME))
                    continue;
                v.AppendLine($"        public void Set{col.ApiColumnName}({col.ApiDataTypeNullable} v)");
                v.AppendLine("        {");
                v.AppendLine($"            if ({col.ApiColumnName} != v)");
                v.AppendLine($"                {col.ApiColumnName} = v;");
                v.AppendLine("        }");
                v.AppendLine();
            }
            v.AppendLine();
            v.AppendLine("        #endregion Methods");
            return v.ToString();
        }

        private string EntityConfig()
        {
            var v = new StringBuilder();

            v.AppendLine($"    #region Configuration");
            v.AppendLine($"    internal class Mapping : IEntityTypeConfiguration<{_helper.TableDomainName}>");
            v.AppendLine("    {");
            v.AppendLine($"        public void Configure(EntityTypeBuilder<{_helper.TableDomainName}> builder)");
            v.AppendLine("        {");
            //table connection
            v.AppendLine($"            builder.ToTable(\"{_helper.TableName}\", \"{_helper.Schema}\");");
            //pk setup
            if (_helper.PrimaryKeys != null && _helper.PrimaryKeys.Count == 1)
            {
                v.AppendLine($"            builder.HasKey(u => u.Id);  // PK.");
                v.AppendLine($"            builder.Property(p => p.Id).HasColumnName(\"{_helper.PrimaryKeys[0]}\");");
            }
            v.AppendLine();

            //max col size & isRequired
            foreach (var col in _helper.Columns)
            {
                string hasLength = "";
                string isRequired = "";

                if (col.ApiDataType == "string")
                    hasLength = $".HasMaxLength({col.CHARACTER_MAXIMUM_LENGTH})";

                if (col.ColumnIsString)
                    isRequired = ".IsRequired()";

                if (!string.IsNullOrWhiteSpace(hasLength) || !string.IsNullOrWhiteSpace(isRequired))
                    v.AppendLine($"            builder.Property(p => p.{col.ApiColumnName}){hasLength}{isRequired};");
            }

            v.AppendLine("          ");
            v.AppendLine("        }");
            v.AppendLine("    }");
            v.AppendLine($"    #endregion //config");


            return v.ToString();
        }
}
}
