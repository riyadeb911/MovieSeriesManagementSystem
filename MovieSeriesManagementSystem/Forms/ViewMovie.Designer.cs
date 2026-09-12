namespace MovieSeriesManagementSystem.Forms
{
    partial class ViewMovie
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
            dgvRatings = new DataGridView();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRatings).BeginInit();
            SuspendLayout();
            // 
            // dgvRatings
            // 
            dgvRatings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRatings.Location = new Point(12, 155);
            dgvRatings.Name = "dgvRatings";
            dgvRatings.RowHeadersWidth = 51;
            dgvRatings.Size = new Size(1059, 427);
            dgvRatings.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(407, 63);
            label1.Name = "label1";
            label1.Size = new Size(252, 28);
            label1.TabIndex = 1;
            label1.Text = "MOVIE AND SERIES VIEW";
            // 
            // ViewMovie
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1083, 603);
            Controls.Add(label1);
            Controls.Add(dgvRatings);
            Name = "ViewMovie";
            Text = "ViewMovie";
            ((System.ComponentModel.ISupportInitialize)dgvRatings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvRatings;
        private Label label1;
    }
}