using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Dto
{
    public class ColumnDefinition : DefinitionBaseClass
    {
        public ColumnDefinition()
        {
            if (DataTypeMapping == null || DataTypeMapping.Count() == 0)
                DataTypeMapping = GetDataTypeMapping();

        }

        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string COLUMN_DEFAULT { get; set; }
        public string IS_NULLABLE { get; set; }
        public int? ORDINAL_POSITION { get; set; }
        public int? CHARACTER_MAXIMUM_LENGTH { get; set; }
        public int? NUMERIC_PRECISION { get; set; }
        public int? NUMERIC_PRECISION_RADIX { get; set; }
        public int? NUMERIC_SCALE { get; set; }
        public int? DATETIME_PRECISION { get; set; }

        public string ApiColumnName => ToCamelCase(COLUMN_NAME);

        public string ApiDataType => DataTypeMapping.SingleOrDefault(s => s.Key == DATA_TYPE.ToLower()).Value;

        public string ApiDataTypeNullable => ColumnIsNullable ? $"{ApiDataType}?" : ApiDataType;

        public bool ColumnIsString => ApiDataType == "string"
                                            || ApiDataType == "DateTime"
                                            || ApiDataType == "Guid"
                                            || ApiDataType == "Xml"
                                            ? true : false;

        public bool ScriptSkipColumn => DATA_TYPE.ToLower() == "timestamp" ? true : false;

        public bool ColumnIsNullable => IS_NULLABLE == "YES" && ApiDataType.ToLower() != "string" && ApiDataType.ToLower() != "byte[]" ? true : false;

        public static Dictionary<string, string> DataTypeMapping { get; set; }
        public static Dictionary<string, string> GetDataTypeMapping()
        {
            //Source: https://msdn.microsoft.com/en-us/library/cc716729(v=vs.110).aspx

            var map = new Dictionary<string, string>();
            map.Add("bigint", "long");
            map.Add("binary", "byte[]");
            map.Add("bit", "bool");
            map.Add("char", "string");
            map.Add("date", "DateTime");
            map.Add("datetime", "DateTime");
            map.Add("datetime2", "DateTime");
            map.Add("datetimeoffset", "DateTimeOffset");
            map.Add("decimal", "decimal");
            map.Add("float", "double");
            map.Add("image", "byte[]");
            map.Add("int", "int");
            map.Add("money", "decimal");
            map.Add("nchar", "string");
            map.Add("ntext", "string");
            map.Add("numeric", "decimal");
            map.Add("nvarchar", "string");
            map.Add("real", "Single");
            map.Add("rowversion", "byte[]");
            map.Add("smalldatetime", "DateTime");
            map.Add("smallint", "Int16");
            map.Add("smallmoney", "decimal");
            map.Add("sql_variant", "Object");
            map.Add("text", "string");
            map.Add("time", "DateTime");    //map.Add("time", "TimeSpan");
            map.Add("timestamp", "byte[]");
            map.Add("tinyint", "byte");
            map.Add("uniqueidentifier", "Guid");
            map.Add("varbinary", "Byte[]");
            map.Add("varchar", "string");
            map.Add("xml", "Xml");

            //map.Add("char", "string");
            //map.Add("varchar", "string");
            //map.Add("nvarchar", "string");
            //map.Add("text", "string");
            //map.Add("ntext", "string");

            //map.Add("uniqueidentifier", "string");

            //map.Add("timestamp", "DateTime");
            //map.Add("datetime", "DateTime");
            //map.Add("date", "DateTime");
            //map.Add("datetime2", "DateTime");
            //map.Add("datetimeoffset", "DateTime");
            //map.Add("smalldatetime", "DateTime");
            //map.Add("time", "DateTime");

            //map.Add("bit", "bool");
            //map.Add("bigint", "long");
            //map.Add("int", "int");
            //map.Add("smallint", "int");
            //map.Add("tinyint", "int");
            //map.Add("decimal", "decimal");
            //map.Add("numeric", "decimal");
            //map.Add("money", "decimal");
            //map.Add("smallmoney", "decimal");
            //map.Add("float", "double");
            //map.Add("real", "Single");

            //map.Add("binary", "Byte[]");
            //map.Add("varbinary", "Byte[]");
            //map.Add("image", "Byte[]");
            //map.Add("cursor", "object");
            //map.Add("hierarchyid", "object");
            //map.Add("sql_variant", "object");
            //map.Add("table", "object");
            //map.Add("xml", "object");
            //map.Add("geography", "object");
            //map.Add("geometry", "object");

            map.Add("", "");

            return map;
        }
    }
}
