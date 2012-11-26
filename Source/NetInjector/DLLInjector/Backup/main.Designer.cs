namespace DLLInjector
{
    partial class main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.DllPathTextBox = new System.Windows.Forms.TextBox();
            this.LoadDllButton = new System.Windows.Forms.Button();
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.InjectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DllPathTextBox
            // 
            this.DllPathTextBox.BackColor = System.Drawing.Color.White;
            this.DllPathTextBox.Location = new System.Drawing.Point(12, 12);
            this.DllPathTextBox.Name = "DllPathTextBox";
            this.DllPathTextBox.Size = new System.Drawing.Size(213, 20);
            this.DllPathTextBox.TabIndex = 0;
            // 
            // LoadDllButton
            // 
            this.LoadDllButton.Location = new System.Drawing.Point(231, 10);
            this.LoadDllButton.Name = "LoadDllButton";
            this.LoadDllButton.Size = new System.Drawing.Size(55, 23);
            this.LoadDllButton.TabIndex = 1;
            this.LoadDllButton.Text = "Charger";
            this.LoadDllButton.UseVisualStyleBackColor = true;
            this.LoadDllButton.Click += new System.EventHandler(this.LoadDllButton_Click);
            // 
            // ProcessListView
            // 
            this.ProcessListView.BackColor = System.Drawing.Color.White;
            listViewGroup3.Header = "ListViewGroup";
            listViewGroup3.Name = "listViewGroup3";
            this.ProcessListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3});
            this.ProcessListView.Location = new System.Drawing.Point(12, 65);
            this.ProcessListView.MultiSelect = false;
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.Size = new System.Drawing.Size(268, 192);
            this.ProcessListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ProcessListView.TabIndex = 2;
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.List;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(12, 36);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(74, 23);
            this.RefreshButton.TabIndex = 3;
            this.RefreshButton.Text = "Rafraîchir processus";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // InjectButton
            // 
            this.InjectButton.Location = new System.Drawing.Point(92, 36);
            this.InjectButton.Name = "InjectButton";
            this.InjectButton.Size = new System.Drawing.Size(75, 23);
            this.InjectButton.TabIndex = 4;
            this.InjectButton.Text = "Injecter";
            this.InjectButton.UseVisualStyleBackColor = true;
            this.InjectButton.Click += new System.EventHandler(this.InjectButton_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.InjectButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.ProcessListView);
            this.Controls.Add(this.LoadDllButton);
            this.Controls.Add(this.DllPathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "main";
            this.Text = "Injection de dll par Misugi";
            this.Load += new System.EventHandler(this.main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DllPathTextBox;
        private System.Windows.Forms.Button LoadDllButton;
        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button InjectButton;
    }
}

