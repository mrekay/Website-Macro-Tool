namespace WebStressTool.Forms
{
    partial class AddLoopVariable
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_tm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_value = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_variable = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(299, 221);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_tm
            // 
            this.btn_tm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_tm.Location = new System.Drawing.Point(218, 221);
            this.btn_tm.Name = "btn_tm";
            this.btn_tm.Size = new System.Drawing.Size(75, 23);
            this.btn_tm.TabIndex = 6;
            this.btn_tm.Text = "Ok";
            this.btn_tm.UseVisualStyleBackColor = true;
            this.btn_tm.Click += new System.EventHandler(this.btn_tm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(46, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Variable Name";
            // 
            // tb_value
            // 
            this.tb_value.Location = new System.Drawing.Point(46, 152);
            this.tb_value.Name = "tb_value";
            this.tb_value.Size = new System.Drawing.Size(294, 20);
            this.tb_value.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(46, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Variable Value";
            // 
            // cb_variable
            // 
            this.cb_variable.FormattingEnabled = true;
            this.cb_variable.Location = new System.Drawing.Point(46, 76);
            this.cb_variable.Name = "cb_variable";
            this.cb_variable.Size = new System.Drawing.Size(294, 21);
            this.cb_variable.TabIndex = 10;
            // 
            // AddLoopVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 256);
            this.Controls.Add(this.cb_variable);
            this.Controls.Add(this.tb_value);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_tm);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLoopVariable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AddLoopVariable";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_tm;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tb_value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_variable;
    }
}