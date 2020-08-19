namespace QL_QuanCafe
{
    partial class frmDanhSachBan
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
            this.pnlDMBan = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pnlDMBan
            // 
            this.pnlDMBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDMBan.Location = new System.Drawing.Point(0, 0);
            this.pnlDMBan.Name = "pnlDMBan";
            this.pnlDMBan.Size = new System.Drawing.Size(823, 468);
            this.pnlDMBan.TabIndex = 0;
            // 
            // frmDanhSachBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 468);
            this.Controls.Add(this.pnlDMBan);
            this.Name = "frmDanhSachBan";
            this.Text = "frmDanhSachBan";
            this.Load += new System.EventHandler(this.frmDanhSachBan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlDMBan;

    }
}