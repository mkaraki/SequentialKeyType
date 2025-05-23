namespace SequentialKeyType
{
    partial class MainForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_type = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.num_delay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tbox_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_layout = new System.Windows.Forms.ComboBox();
            this.btn_debug = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_delay)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_type, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.num_delay, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbox_text, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.combo_layout, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_debug, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(607, 342);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_type
            // 
            this.btn_type.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_type.Location = new System.Drawing.Point(123, 317);
            this.btn_type.Name = "btn_type";
            this.btn_type.Size = new System.Drawing.Size(481, 22);
            this.btn_type.TabIndex = 0;
            this.btn_type.Text = "Type";
            this.btn_type.UseVisualStyleBackColor = true;
            this.btn_type.Click += new System.EventHandler(this.btn_type_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Delay (sec)";
            // 
            // num_delay
            // 
            this.num_delay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.num_delay.Location = new System.Drawing.Point(123, 289);
            this.num_delay.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.num_delay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_delay.Name = "num_delay";
            this.num_delay.Size = new System.Drawing.Size(481, 20);
            this.num_delay.TabIndex = 2;
            this.num_delay.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Text";
            // 
            // tbox_text
            // 
            this.tbox_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbox_text.Location = new System.Drawing.Point(123, 3);
            this.tbox_text.Multiline = true;
            this.tbox_text.Name = "tbox_text";
            this.tbox_text.Size = new System.Drawing.Size(481, 224);
            this.tbox_text.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Locale";
            // 
            // combo_layout
            // 
            this.combo_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combo_layout.FormattingEnabled = true;
            this.combo_layout.Location = new System.Drawing.Point(123, 261);
            this.combo_layout.Name = "combo_layout";
            this.combo_layout.Size = new System.Drawing.Size(481, 21);
            this.combo_layout.TabIndex = 6;
            // 
            // btn_debug
            // 
            this.btn_debug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_debug.Location = new System.Drawing.Point(3, 317);
            this.btn_debug.Name = "btn_debug";
            this.btn_debug.Size = new System.Drawing.Size(114, 22);
            this.btn_debug.TabIndex = 7;
            this.btn_debug.Text = "Debug";
            this.btn_debug.UseVisualStyleBackColor = true;
            this.btn_debug.Click += new System.EventHandler(this.btn_debug_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 342);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Sequential Key Type";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_delay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown num_delay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbox_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combo_layout;
        private System.Windows.Forms.Button btn_debug;
    }
}

