namespace WebStressTool.Forms
{
    partial class Loops
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btn_add_loop = new System.Windows.Forms.Button();
            this.btn_remove_loop = new System.Windows.Forms.Button();
            this.btn_add_variable = new System.Windows.Forms.Button();
            this.btn_remove_variable = new System.Windows.Forms.Button();
            this.btn_settings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(664, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // btn_add_loop
            // 
            this.btn_add_loop.Location = new System.Drawing.Point(682, 12);
            this.btn_add_loop.Name = "btn_add_loop";
            this.btn_add_loop.Size = new System.Drawing.Size(106, 23);
            this.btn_add_loop.TabIndex = 1;
            this.btn_add_loop.Text = "Add Loop";
            this.btn_add_loop.UseVisualStyleBackColor = true;
            this.btn_add_loop.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_remove_loop
            // 
            this.btn_remove_loop.Location = new System.Drawing.Point(682, 41);
            this.btn_remove_loop.Name = "btn_remove_loop";
            this.btn_remove_loop.Size = new System.Drawing.Size(106, 23);
            this.btn_remove_loop.TabIndex = 2;
            this.btn_remove_loop.Text = "Remove Loop";
            this.btn_remove_loop.UseVisualStyleBackColor = true;
            this.btn_remove_loop.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_add_variable
            // 
            this.btn_add_variable.Location = new System.Drawing.Point(682, 386);
            this.btn_add_variable.Name = "btn_add_variable";
            this.btn_add_variable.Size = new System.Drawing.Size(106, 23);
            this.btn_add_variable.TabIndex = 3;
            this.btn_add_variable.Text = "Add Variable";
            this.btn_add_variable.UseVisualStyleBackColor = true;
            this.btn_add_variable.Click += new System.EventHandler(this.btn_add_variable_Click);
            // 
            // btn_remove_variable
            // 
            this.btn_remove_variable.Location = new System.Drawing.Point(682, 415);
            this.btn_remove_variable.Name = "btn_remove_variable";
            this.btn_remove_variable.Size = new System.Drawing.Size(106, 23);
            this.btn_remove_variable.TabIndex = 4;
            this.btn_remove_variable.Text = "Remove Variable";
            this.btn_remove_variable.UseVisualStyleBackColor = true;
            this.btn_remove_variable.Click += new System.EventHandler(this.btn_remove_variable_Click);
            // 
            // btn_settings
            // 
            this.btn_settings.Location = new System.Drawing.Point(682, 70);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(106, 23);
            this.btn_settings.TabIndex = 5;
            this.btn_settings.Text = "Settings";
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // Loops
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.btn_remove_variable);
            this.Controls.Add(this.btn_add_variable);
            this.Controls.Add(this.btn_remove_loop);
            this.Controls.Add(this.btn_add_loop);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loops";
            this.Text = "Loops";
            this.Load += new System.EventHandler(this.Loops_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btn_add_loop;
        private System.Windows.Forms.Button btn_remove_loop;
        private System.Windows.Forms.Button btn_add_variable;
        private System.Windows.Forms.Button btn_remove_variable;
        private System.Windows.Forms.Button btn_settings;
    }
}