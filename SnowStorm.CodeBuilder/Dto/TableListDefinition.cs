using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Dto
{
    public class TableListDefinition : DefinitionBaseClass
    {
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }

        public string TableId => $"{TABLE_SCHEMA}.{TABLE_NAME}";
        public string ApiClassName => ToCamelCase(TABLE_NAME);

    }
}
