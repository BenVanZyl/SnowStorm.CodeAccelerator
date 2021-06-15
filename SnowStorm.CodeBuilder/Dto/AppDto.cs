using System;
using System.Collections.Generic;
using System.Text;

namespace SnowStorm.CodeBuilder.Dto
{
    public class AppDto
    {
        public string AppName { get; set; }
        public string DefaultNameSpace { get; set; }
        public string ConnectionString { get; set; }
        //public string SchemaName { get; set; }
        //public string TableName { get; set; }
        //public string DomainClassName { get; set; }
        //public string ShemaPrefix { get; set; }
    }
}
