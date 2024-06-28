using System.Collections.Generic;
using System.IO;

namespace Source.Scripts.CodeGenerator
{
    public class ClassGenerator
    {
        public void Generate(string className, List<string> pfull,string path)
        {
            string fileName = Path.Combine(path, className + ".cs");
            string classContent = $"public class {className} {{\n";

            foreach (string property in pfull)
            {
                classContent += $"public static string {property} = \"{property}\";\n";
            }

            classContent += "}";

            File.WriteAllText(fileName, classContent);
        }
    }
}
