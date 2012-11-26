using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DLLInjector
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        //Formulaire chargé
        private void main_Load(object sender, EventArgs e)
        {
            //on init la liste des processus
            Injection.InitProcessList();

            //on affiche tout les processus ouverts
            Injection.GetProcessList(ref this.ProcessListView);
        }

        //Rafraîchis la liste des processus
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            //on supprime d'abord les items de la listview et les processus contenus dans la liste
            this.DeleteItems();

            //et ensuite on ajoute les processus
            Injection.GetProcessList(ref this.ProcessListView);
        }

        //Supprime tout les items de la liste
        private void DeleteItems()
        {
            //on regarde si il y a au moins un item dans la liste
            if (this.ProcessListView.Items.Count > 0)
            {
                //on supprime d'abord tout les processus de la liste des processus
                Injection.ClearProcessList();

                //on va ensuite boucler les items de la listview pour les supprimer
                foreach (ListViewItem Item in this.ProcessListView.Items)
                {
                    Item.Remove();
                }
            }

        }

        //Injecte la dll dans le processus séléctionné
        private void InjectButton_Click(object sender, EventArgs e)
        {
            //on vérifie qu'il y a au moins un item qui a le focus et que la dll est bien chargée
            if (this.ProcessListView.FocusedItem != null && !string.IsNullOrEmpty(this.DllPathTextBox.Text))
            {
                //si tout es ok on prend son id à partir de son nom
                uint CurrentSelectedPID = Injection.GetPIDbyName(this.ProcessListView.FocusedItem.Text);

                //et on injecte la dll dans le processus avec son id
                Injection.StartInjection(this.DllPathTextBox.Text, CurrentSelectedPID);
            }
            else MessageBox.Show("Aucun processus séléctionné ou dll non chargée !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Charger la dll
        private void LoadDllButton_Click(object sender, EventArgs e)
        {
            //nouvelle boite de dialogue "ouvrir un fichier"
            OpenFileDialog File = new OpenFileDialog();
            File.Title = "Choissisez la dll à injecter"; //titre de la boite
            File.Filter = "Fichiers DLL|*.dll"; //filtre des fichiers

            //fichier choisit
            if (File.ShowDialog() == DialogResult.OK)
            {
                //on met le nom complet dans la textbox
                this.DllPathTextBox.Text = File.FileName;
            }
        }

    }
}