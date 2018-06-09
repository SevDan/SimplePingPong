namespace Game
{
    partial class ViewLayer
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.levelCounter = new System.Windows.Forms.Label();
            this.upPlayerCounter = new System.Windows.Forms.Label();
            this.downPlayerCounter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mainPanel.Location = new System.Drawing.Point(-1, 58);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(986, 549);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "LEVEL";
            // 
            // levelCounter
            // 
            this.levelCounter.AutoSize = true;
            this.levelCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.levelCounter.Location = new System.Drawing.Point(83, 12);
            this.levelCounter.Name = "levelCounter";
            this.levelCounter.Size = new System.Drawing.Size(29, 31);
            this.levelCounter.TabIndex = 3;
            this.levelCounter.Text = "1";
            this.levelCounter.Click += new System.EventHandler(this.levelCounter_Click);
            // 
            // upPlayerCounter
            // 
            this.upPlayerCounter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upPlayerCounter.AutoSize = true;
            this.upPlayerCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.upPlayerCounter.Location = new System.Drawing.Point(488, 9);
            this.upPlayerCounter.Name = "upPlayerCounter";
            this.upPlayerCounter.Size = new System.Drawing.Size(42, 46);
            this.upPlayerCounter.TabIndex = 4;
            this.upPlayerCounter.Text = "0";
            // 
            // downPlayerCounter
            // 
            this.downPlayerCounter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downPlayerCounter.AutoSize = true;
            this.downPlayerCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.downPlayerCounter.Location = new System.Drawing.Point(477, 610);
            this.downPlayerCounter.Name = "downPlayerCounter";
            this.downPlayerCounter.Size = new System.Drawing.Size(42, 46);
            this.downPlayerCounter.TabIndex = 5;
            this.downPlayerCounter.Text = "0";
            // 
            // ViewLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.downPlayerCounter);
            this.Controls.Add(this.upPlayerCounter);
            this.Controls.Add(this.levelCounter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainPanel);
            this.Name = "ViewLayer";
            this.Text = "THE GAME";
            this.Load += new System.EventHandler(this.ViewLayer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewLayer_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ViewLayer_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label levelCounter;
        private System.Windows.Forms.Label upPlayerCounter;
        private System.Windows.Forms.Label downPlayerCounter;
    }
}

