namespace SequentialKeyType
{
    partial class Debug
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
            this.tbox_msg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbox_msg
            // 
            this.tbox_msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbox_msg.Location = new System.Drawing.Point(0, 0);
            this.tbox_msg.Multiline = true;
            this.tbox_msg.Name = "tbox_msg";
            this.tbox_msg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbox_msg.Size = new System.Drawing.Size(800, 450);
            this.tbox_msg.TabIndex = 0;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbox_msg);
            this.Name = "Debug";
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_msg;
    }
}