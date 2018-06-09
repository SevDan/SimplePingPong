namespace Game
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pvpChose = new System.Windows.Forms.Button();
            this.pveChose = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pvpChose
            // 
            this.pvpChose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pvpChose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pvpChose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pvpChose.Location = new System.Drawing.Point(12, 32);
            this.pvpChose.Name = "pvpChose";
            this.pvpChose.Size = new System.Drawing.Size(395, 119);
            this.pvpChose.TabIndex = 0;
            this.pvpChose.Text = "PVP 1 НА 1";
            this.pvpChose.UseVisualStyleBackColor = true;
            this.pvpChose.Click += new System.EventHandler(this.pvpChose_Click);
            // 
            // pveChose
            // 
            this.pveChose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pveChose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pveChose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pveChose.Location = new System.Drawing.Point(12, 171);
            this.pveChose.Name = "pveChose";
            this.pveChose.Size = new System.Drawing.Size(395, 117);
            this.pveChose.TabIndex = 1;
            this.pveChose.Text = "PVE ПРОТИВ БОТА";
            this.pveChose.UseVisualStyleBackColor = true;
            this.pveChose.Click += new System.EventHandler(this.pveChose_Click);
            // 
            // settings
            // 
            this.settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.settings.Location = new System.Drawing.Point(12, 311);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(395, 99);
            this.settings.TabIndex = 2;
            this.settings.Text = "Настройки";
            this.settings.UseVisualStyleBackColor = true;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 441);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.pveChose);
            this.Controls.Add(this.pvpChose);
            this.MaximumSize = new System.Drawing.Size(436, 480);
            this.MinimumSize = new System.Drawing.Size(436, 480);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "МЕНЮ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pvpChose;
        private System.Windows.Forms.Button pveChose;
        private System.Windows.Forms.Button settings;
    }
}