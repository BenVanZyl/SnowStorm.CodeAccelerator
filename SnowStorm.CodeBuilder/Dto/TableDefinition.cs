using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Dto
{
    public class TableDefinition
    {
        public TableListDefinition Table { get; set; }      
        public List<ColumnDefinition> Columns { get; set; }
        public List<string> PrimaryKeys { get; set; }
        public List<string> ForeignKeys { get; set; }

    }
}
