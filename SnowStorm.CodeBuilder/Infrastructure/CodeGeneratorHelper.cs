using SnowStorm.CodeBuilder.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
//using System.Data.Entity.Design.PluralizationServices;

namespace SnowStorm.CodeBuilder.Infrastructure
{
    public interface ICodeGeneratorHelper
    {
        string Schema { get; set; }
        string TableName { get; set; }
        string DefaultNameSpace { get; set; }
        string NameSpacePrefix { get; set; }
        string TableDomainName { get; set; }
        List<ColumnDefinition> Columns { get; set; }
        List<string> PrimaryKeys { get; set; }
        List<string> ForeignKeys { get; set; }

        void Setup(string schema, string tableName, string tableDomainName, string defaultNameSpace, string nameSpacePrefix, List<ColumnDefinition> columns, List<string> primaryKeys, List<string> foreignKeys);

        void Clear();
    }

    public class CodeGeneratorHelper : ICodeGeneratorHelper //IDisposable
    {
        #region initializer

        public string Schema { get; set; }
        public string TableName { get; set; }
        public string DefaultNameSpace { get; set; }
        public string NameSpacePrefix { get; set; }
        public string TableDomainName { get; set; }
        public List<ColumnDefinition> Columns { get; set; }
        public List<string> PrimaryKeys { get; set; }
        public List<string> ForeignKeys { get; set; } //TODO:  Info.ForeignKeys should change to TblColName, ParentObjectName

        public CodeGeneratorHelper() { }

        public void Setup(string schema, string tableName, string tableDomainName, string defaultNameSpace, string nameSpacePrefix, List<ColumnDefinition> columns, List<string> primaryKeys, List<string> foreignKeys)
        {
            Schema = schema;
            TableName = tableName;
            TableDomainName = tableDomainName;
            DefaultNameSpace = defaultNameSpace;
            NameSpacePrefix = nameSpacePrefix;
            Columns = columns;
            PrimaryKeys = primaryKeys;
            ForeignKeys = foreignKeys;


        }

        #endregion initializer

        public readonly string GetSet = " { get; set; }";
        public readonly string GetSetPrivate = " { get; private set; }";
        public readonly string BracketsOnly = " { }";

        public List<string> AuditColumnNames = new List<string>()
            {
                "create_user_id",
                "create_date",
                "modify_user_id",
                "modify_date",
                "create_date_time",
                "modify_date_time",
                "CreateUserID",
                "CreateDateTime",
                "ModifyUserID",
                "ModifyDateTime",
                "CreatedBy",
                "CreatedOn",
                "ModifiedBy",
                "ModifiedOn"
            };

        //public PluralizationService Plural => PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));

        public string NameSpaceDomain => $"{DefaultNameSpace}.Domain{PrefixNamspace}";
        public string NameSpaceQuery => $"{DefaultNameSpace}.Queries{PrefixNamspace}";
        public string NameSpaceController => $"{DefaultNameSpace}.Api{PrefixNamspace}";
        public string NameSpaceClientDto => $"{DefaultNameSpace}.Client.Api{PrefixNamspace}";
        public string NameSpaceClientApiQuery => $"{DefaultNameSpace}.Client.Api{PrefixNamspace}.Queries";
        public string NameSpaceIntegrationTests => $"{DefaultNameSpace}.Tests.IntegrationTests";
        public string PrefixNamspace => string.IsNullOrEmpty(NameSpacePrefix) ? "" : $".{NameSpacePrefix}";

        public bool IsPrimaryKeyColumn(string columnName) => PrimaryKeys.Any(a => a.ToLower() == columnName.ToLower());
        public bool IsForeignKeyColumn(string columnName) => ForeignKeys.Any(a => a.ToLower() == columnName.ToLower());
        public bool IsAuditColumn(string columnName) => AuditColumnNames.Any(a => a.ToLower() == columnName.ToLower());

        public bool HasPrimaryKeys => PrimaryKeys != null && PrimaryKeys.Count > 0 ? true : false;
        public bool HasPrimaryKeySingle => PrimaryKeys != null && PrimaryKeys.Count == 1 ? true : false;
        public bool HasPrimaryKeysMany => PrimaryKeys != null && PrimaryKeys.Count > 1 ? true : false;

        public ColumnDefinition Column(string columnName) => Columns.SingleOrDefault(s => s.COLUMN_NAME.ToLower() == columnName.ToLower());

        public string PkPrivateProperties()
        {
            var value = new StringBuilder();
            foreach (var key in PrimaryKeys)
            {
                value.AppendLine($"        private readonly {Column(key).ApiDataTypeNullable} _{DefinitionBaseClass.ToCamelCaseStartLower(key)};");
            }
            return value.ToString();
        }

        public string PkPrivatePropertiesAssignments()
        {
            var keyName = "";
            var value = new StringBuilder();
            foreach (var key in PrimaryKeys)
            {
                keyName = DefinitionBaseClass.ToCamelCaseStartLower(key);
                value.AppendLine($"            _{keyName} = {keyName};");
            }
            return value.ToString();
        }

        public string PkMethodVariables(bool withDataTypes)
        {
            var dt = "";
            var value = new StringBuilder();
            foreach (var key in PrimaryKeys)
            {
                dt = withDataTypes ? $"{Column(key).ApiDataType} " : "";
                value.Append($"{dt}{DefinitionBaseClass.ToCamelCaseStartLower(key)}, ");
            }
            if (value.Length > 0)
                value.Remove(value.Length - 2, 2); //remove last ', "'
            return value.ToString();
        }

        public string PkWhereStatement(bool isBaseQuery)
        {
            var colName = "";
            var value = new StringBuilder();
            foreach (var key in PrimaryKeys)
            {
                var pk = Column(key);
                if (pk.ApiDataType == "string")
                    value.AppendLine($"            if (!string.IsNullOrEmpty(_{ToCamelCaseStartLower(pk.ApiColumnName)}))");
                else
                    value.AppendLine($"            if (_{ToCamelCaseStartLower(pk.ApiColumnName)}.HasValue)");

                if (isBaseQuery)
                {
                    colName = pk.ApiDataType == "int" ? "Id" : pk.ApiColumnName;
                    value.AppendLine($"                baseQuery = baseQuery.Where(w => w.{colName} == _{ToCamelCaseStartLower(key)});");
                }
                else
                    value.AppendLine($"                url = $\"{{url}}/{{_{ToCamelCaseStartLower(key)}}}\";");
            }
            return value.ToString();
        }

        public string ToCamelCaseStartLower(string value) => DefinitionBaseClass.ToCamelCaseStartLower(value);

        public string AuditColumnName(string columnName)
        {
            var auditColName = "";

            if (columnName == "CreatedBy" || columnName == "CreateUserID") //TODO:  Provide list of col names for comparision
                auditColName = "CreatedBy";

            if (columnName == "CreatedOn" || columnName == "create_date_time" || columnName == "CreateDateTime") //TODO:  Provide list of col names for comparision
                auditColName = "CreatedOn";

            if (columnName == "ModifiedBy" || columnName == "ModifyUserID" || columnName== "LastUpdatedBy") //TODO:  Provide list of col names for comparision
                auditColName = "ModifiedBy";

            if (columnName == "ModifiedOn" || columnName == "modify_date_time" || columnName == "ModifyDateTime" || columnName == "LastUpdatedOn") //TODO:  Provide list of col names for comparision
                auditColName = "ModifiedOn";

            return auditColName;
        }

        public string IncludesForQuery()
        {
            var value = new StringBuilder();
            if (ForeignKeys.Count > 0)
                value.AppendLine();

            foreach (var fkey in ForeignKeys)
            {   //TODO:  Info.ForeignKeys should change to TblColName, ParentObjectName
                var fk = Column(fkey);
                if (fk.COLUMN_NAME.ToLower() == "company_code" || fk.COLUMN_NAME.ToLower() == "companyid")
                    value.AppendLine($"                 .Include(i => i.Company)");
                else
                    value.AppendLine($"                 .Include(i => i.{fk.ApiColumnName})");
            }
            return value.ToString();
        }

        public void Clear()
        {
            Schema = string.Empty;
            TableName = string.Empty;
            TableDomainName = string.Empty;
            Columns = null;
            PrimaryKeys = null;
            ForeignKeys = null;
            GC.Collect();
        }
    }
}
