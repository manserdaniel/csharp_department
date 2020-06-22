using AbteilungenLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abteilungen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            updateTreeView();
        }

        DataHandling dataHandling = new DataHandling();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newDepart = tbNew.Text;
            var parent = treeView.SelectedNode.Text;
            dataHandling.addDepartment(newDepart, parent);
            updateTreeView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var toDelete = treeView.SelectedNode.Text;
            dataHandling.deleteDepartment(toDelete);
            updateTreeView();
        }

        private void updateTreeView()
        {
            treeView.Nodes.Clear();

            var departments = dataHandling.getDepartments();
            foreach (var department in departments)
            {
                if (department.parent_id == null)
                {
                    treeView.Nodes.Add(department.name, department.name);
                }
                else
                {
                    treeView.Nodes.Find(departments.FirstOrDefault(x => x.id == department.parent_id).name, true).ToList().FirstOrDefault().Nodes.Add(department.name, department.name);
                }
            }
        }
    }
}
