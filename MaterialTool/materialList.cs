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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialTool
{
    public partial class materialList : Form
    {
        private string AchievementJsonPath = "MeshSlicer/Assets/StreamingAssets/material_types.json";
        private JObject MaterialJSON;

        /* Instanciated */
        public materialList()
        {
            InitializeComponent();
        }

        /* Reload GUI List */
        private void ReloadList(object sender, EventArgs e)
        {
            ReloadList();
        }
        private void ReloadList()
        {
            MaterialJSON = JObject.Parse(File.ReadAllText(AchievementJsonPath));
            materialListBox.Items.Clear();
            foreach (var AchievementEntry in MaterialJSON["materials"])
            {
                materialListBox.Items.Add(AchievementEntry["type"]);
            }
        }

        /* ON LOAD */
        private void materialList_Load(object sender, EventArgs e)
        {
            ReloadList();
        }

        /* GUI BUTTONS */
        private void addNew_Click(object sender, EventArgs e)
        {
            materialEditor materialEditorInstance = new materialEditor(AchievementJsonPath);
            materialEditorInstance.FormClosed += new FormClosedEventHandler(ReloadList);
            materialEditorInstance.Show();
        }
        private void editSelected_Click(object sender, EventArgs e)
        {
            if (materialListBox.SelectedIndex == -1)
            {
                MessageBox.Show("No material selected.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            materialEditor materialEditorInstance = new materialEditor(AchievementJsonPath, materialListBox.SelectedIndex, MaterialJSON["materials"][materialListBox.SelectedIndex]);
            materialEditorInstance.FormClosed += new FormClosedEventHandler(ReloadList);
            materialEditorInstance.Show();

        }
        private void deleteSelected_Click(object sender, EventArgs e)
        {
            if (materialListBox.SelectedIndex == -1)
            {
                MessageBox.Show("No material selected.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MaterialJSON = JObject.Parse(File.ReadAllText(AchievementJsonPath));
            MaterialJSON["materials"][materialListBox.SelectedIndex].Remove();
            File.WriteAllText(AchievementJsonPath, MaterialJSON.ToString(Formatting.Indented));

            UpdateEnum.RemoveEnum(UpdateEnum.MakeEnumName(materialListBox.SelectedItem.ToString()));
            ReloadList();

            MessageBox.Show("Selected material deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
