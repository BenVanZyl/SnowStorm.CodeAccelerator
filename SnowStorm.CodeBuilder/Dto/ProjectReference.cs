using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace SnowStorm.CodeBuilder.Dto
{
    public class ProjectReference
    {
        public string ProjectName { get; private set; }
        public string ProjectPath { get; private set; }
        public string ProjectRootPath { get; private set; }
        public string ProjectGuid { get; private set; }
        //public Microsoft.Build.Evaluation.Project Content { get; private set; }

        public ProjectReference(string[] content, string filter, string rootPath)
        {
            try
            {
                var project = content.Where(w => w.Contains(filter)).ToArray();
                var x1 = project[0].Split('=');
                var x2 = x1[1].Split(',');
                ProjectName = x2[0].Replace("\"", "").Replace(" ", "");
                ProjectPath = $"{rootPath}\\{x2[1].Replace("\"", "").Replace(" ", "")}";
                ProjectGuid = x2[2].Replace("\"", "").Replace(" ", "");

                ProjectRootPath = ProjectPath.Replace($"{ProjectName}.csproj", "");

                //Content = new Microsoft.Build.Evaluation.Project(ProjectPath);
            }
            catch (System.Exception ex)
            {

                content = null;
            }
        }

        public void AddFileToProject(ExportDefinition item)
        {
            //if (Content.GetItems("Compile").FirstOrDefault(w => w.EvaluatedInclude == item.FilePath) == null)
            //{
            //    Content.AddItem("Compile", item.FilePath);
            //    Content.Save();
            //}
        }
    }
}
