using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Dto
{
    public class ProjectConfiguration
    {
        public string ProjectName { get; set; }
        public string Namespace { get; set; }
        public string DbConnectionstring { get; set; }
        public string ProjectApi { get; set; }
        public string ProjectApiClient { get; set; }
        public string ProjectTest { get; set; }
        public string ProjectSchemaMigrator { get; set; }
    }
}
