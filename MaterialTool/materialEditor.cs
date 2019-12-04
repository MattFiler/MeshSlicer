using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialTool
{
    public partial class materialEditor : Form
    {
        private string AchievementJsonPath;
        private int MaterialJsonIndex;
        private JToken EditorContent;

        /* Load form and populate */
        public materialEditor(string JsonPath, int index = 0, JToken editor_content = null)
        {
            AchievementJsonPath = JsonPath;
            MaterialJsonIndex = index;
            EditorContent = editor_content;

            InitializeComponent();

            //Load editor content if it exists (we are overwriting not making a new achievement)
            if (EditorContent != null)
            {
                MaterialName.Text = EditorContent["type"].Value<string>();
                MaterialName.ReadOnly = true;
                MaterialIsBreakable.Checked = EditorContent["can_shatter"].Value<bool>();
                if (!MaterialIsBreakable.Checked) return;
                MaterialStrength.Value = EditorContent["strength"].Value<int>();
                MaterialDensity.Value = EditorContent["density"].Value<int>();
                strengthTracker.Text = MaterialStrength.Value.ToString();
                densityTracker.Text = MaterialDensity.Value.ToString();
            }
        }

        /* Save form content */
        private void saveMaterial_Click(object sender, EventArgs e)
        {
            if (MaterialName.Text == "")
            {
                MessageBox.Show("Please complete all fields before saving.", "Incomplete data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            JObject JsonConfig = JObject.Parse(File.ReadAllText(AchievementJsonPath));
            if (EditorContent == null)
            {
                foreach (JObject MaterialEntry in JsonConfig["materials"])
                {
                    if (MaterialEntry["type"].Value<string>() == MaterialName.Text.Trim())
                    {
                        MessageBox.Show("A material with this name already exists.", "Name conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            JObject NewMaterialEntry = new JObject();
            NewMaterialEntry["type"] = MaterialName.Text.Trim();
            NewMaterialEntry["can_shatter"] = MaterialIsBreakable.Checked;
            NewMaterialEntry["strength"] = MaterialStrength.Value;
            NewMaterialEntry["density"] = MaterialDensity.Value;

            if (EditorContent != null)
            {
                JsonConfig["materials"][MaterialJsonIndex] = NewMaterialEntry;
                UpdateEnum.EditEnum(UpdateEnum.MakeEnumName(EditorContent["type"].Value<string>().Trim()), UpdateEnum.MakeEnumName(NewMaterialEntry["type"].Value<string>().Trim()));
            }
            else
            {
                ((JArray)JsonConfig["materials"]).Add(NewMaterialEntry);
                UpdateEnum.AddEnum(UpdateEnum.MakeEnumName(MaterialName.Text.Trim()));
            }

            File.WriteAllText(AchievementJsonPath, JsonConfig.ToString(Formatting.Indented));
            MessageBox.Show("Material saved!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        /* UI tweaks on input */
        private void MaterialIsBreakable_CheckedChanged(object sender, EventArgs e)
        {
            MaterialStrength.Enabled = MaterialIsBreakable.Checked;
            MaterialStrength.Value = 0;
            MaterialDensity.Enabled = MaterialIsBreakable.Checked;
            MaterialDensity.Value = 0;
        }
        private void MaterialStrength_Scroll(object sender, EventArgs e)
        {
            strengthTracker.Text = MaterialStrength.Value.ToString();
        }
        private void MaterialDensity_Scroll(object sender, EventArgs e)
        {
            densityTracker.Text = MaterialDensity.Value.ToString();
        }
    }
}
