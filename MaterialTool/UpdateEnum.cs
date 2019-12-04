using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialTool
{
    class UpdateEnum
    {
        private static string PathToFile = "MeshSlicer/Assets/Scripts/ObjectMaterialDefinition.cs";
        private static bool CanEdit = false;

        /* Make an enum from the existing name */
        public static string MakeEnumName(string existing_name)
        {
            return existing_name.ToUpper().Replace(' ', '_');
        }

        /* Edit an enum entry in the game */
        public static void EditEnum(string old_enum_name, string new_enum_name)
        {
            List<string> FileContent = File.ReadAllLines(PathToFile).ToList<string>();
            for (int i = 0; i < FileContent.Count; i++)
            {
                //Tag Management
                if (FileContent[i].Replace("    ", "").Replace(" ", "") == "/*START*/") { CanEdit = true; }
                if (!CanEdit) { continue; }

                //Edit enum when found
                string ThisEnum = FileContent[i].Replace(" ", "");
                ThisEnum = ThisEnum.Substring(0, ThisEnum.Length - 1);
                if (ThisEnum == old_enum_name)
                {
                    FileContent[i] = "    " + new_enum_name + ",";
                    break;
                }
            }
            CanEdit = false;
            File.WriteAllLines(PathToFile, FileContent);
        }

        /* Remove an enum entry from the game */
        public static void RemoveEnum(string enum_name)
        {
            List<string> FileContent = File.ReadAllLines(PathToFile).ToList<string>();
            int IndexToRemove = -1;
            for (int i = 0; i < FileContent.Count; i++)
            {
                //Tag Management
                if (FileContent[i].Replace("    ", "").Replace(" ", "") == "/*START*/") { CanEdit = true; }
                if (!CanEdit) { continue; }

                //Grab index if found (later use index to remove)
                string ThisEnum = FileContent[i].Replace(" ", "");
                ThisEnum = ThisEnum.Substring(0, ThisEnum.Length - 1);
                if (ThisEnum == enum_name)
                {
                    IndexToRemove = i;
                    break;
                }
            }
            if (IndexToRemove != -1)
            {
                FileContent.RemoveAt(IndexToRemove);
            }
            CanEdit = false;
            File.WriteAllLines(PathToFile, FileContent);
        }

        /* Add a new enum entry to the game */
        public static void AddEnum(string enum_name)
        {
            List<string> FileContent = File.ReadAllLines(PathToFile).ToList<string>();
            for (int i = 0; i < FileContent.Count; i++)
            {
                string test = FileContent[i].Replace("    ", "").Replace(" ", "").Trim();
                //Insert enum
                if (FileContent[i].Replace("    ", "").Replace(" ", "").Trim() == "/*END*/")
                {
                    FileContent.Insert(i - 1, "    " + enum_name + ",");
                    break;
                }
            }
            CanEdit = false;
            File.WriteAllLines(PathToFile, FileContent);
        }
    }
}
