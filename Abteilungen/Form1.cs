using AbteilungenData;
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
            treeView.AllowDrop = true;
            treeView.Dock = DockStyle.Fill;
            treeView.ItemDrag += new ItemDragEventHandler(treeView_ItemDrag);
            treeView.DragEnter += new DragEventHandler(treeView_DragEnter);
            treeView.DragOver += new DragEventHandler(treeView_DragOver);
            treeView.DragDrop += new DragEventHandler(treeView_DragDrop);
        }

        DataHandling dataHandling = new DataHandling();
        List<Department> departments = new List<Department>();


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

            departments = dataHandling.getDepartments();

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

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            // Copy the dragged node when the right mouse button is used.
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }

        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeView.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current 
                // location and add it to the node at the drop location.
                if (e.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode); 
                    departments.FirstOrDefault(x => x.name == draggedNode.Text).parent_id = departments.FirstOrDefault(x => x.name == targetNode.Text).id;
                }

                // If it is a copy operation, clone the dragged node 
                // and add it to the node at the drop location.
                else if (e.Effect == DragDropEffects.Copy)
                {
                    targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                }

                // Expand the node at the location 
                // to show the dropped node.
                targetNode.Expand();

            }
        }

        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));

            treeView.SelectedNode = treeView.GetNodeAt(targetPoint);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataHandling.updateDepartments(departments);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lbParentDep.Text = treeView.SelectedNode?.Text;
        }
    }
}
