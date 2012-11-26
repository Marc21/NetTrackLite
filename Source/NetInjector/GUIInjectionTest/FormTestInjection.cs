using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using NetInjector;
using System.Reflection;
using System.IO;


namespace GUIInjectionTest
{
    public partial class FormTestInjection : Form
    {
        private BindingSource _processList = new BindingSource();

        public FormTestInjection()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshProcessList();
            txtDllToInject.Text=Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\NetBootStrap.dll";
            dataGridView1.DataSource = _processList;
        }

        private void RefreshProcessList()
        {
            _processList.Clear();
            List<ProcessGUIPresenter> innerList = new List<ProcessGUIPresenter>();
            _processList.SuspendBinding();
            try
            {

                foreach (Process process in Process.GetProcesses())
                {
                    try
                    {
                        if (!process.HasExited)

                            innerList.Add(new ProcessGUIPresenter(process));
                    }
                    catch
                    {
                        //nothing here : we are just on a gui tsting project
                    }
                }
                innerList.Sort((a, b) => a.ProcessName.CompareTo(b.ProcessName));
                _processList.DataSource = innerList;    
            }
            finally
            {
                _processList.ResumeBinding();
            }
            
        
        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            RefreshProcessList();
        }

        private void btInject_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Injector injector = new Injector();
                ProcessGUIPresenter processPresenter = (ProcessGUIPresenter)dataGridView1.SelectedRows[0].DataBoundItem;
                injector.Inject((uint)processPresenter.Id, txtDllToInject.Text);
                
            }
        }
    }
}
