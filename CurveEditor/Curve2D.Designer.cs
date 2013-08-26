namespace CurveEditor
{
    partial class Curve2D
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.framebuffer = new System.Windows.Forms.PictureBox();
            this.pointXtext = new System.Windows.Forms.TextBox();
            this.pointYtext = new System.Windows.Forms.TextBox();
            this.pointTtext = new System.Windows.Forms.TextBox();
            this.CurvePointsText = new System.Windows.Forms.TextBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framebuffer)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.framebuffer);
            this.mainPanel.Location = new System.Drawing.Point(12, 36);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(713, 372);
            this.mainPanel.TabIndex = 0;
            // 
            // framebuffer
            // 
            this.framebuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.framebuffer.Location = new System.Drawing.Point(0, 0);
            this.framebuffer.Name = "framebuffer";
            this.framebuffer.Size = new System.Drawing.Size(707, 365);
            this.framebuffer.TabIndex = 0;
            this.framebuffer.TabStop = false;
            this.framebuffer.Click += new System.EventHandler(this.framebuffer_Click);
            this.framebuffer.Paint += new System.Windows.Forms.PaintEventHandler(this.framebuffer_Paint);
            this.framebuffer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.framebuffer_MouseClick);
            this.framebuffer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.framebuffer_MouseDoubleClick);
            this.framebuffer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.framebuffer_MouseDown);
            this.framebuffer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.framebuffer_MouseMove);
            this.framebuffer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.framebuffer_MouseUp);
            // 
            // pointXtext
            // 
            this.pointXtext.Location = new System.Drawing.Point(29, 12);
            this.pointXtext.Name = "pointXtext";
            this.pointXtext.Size = new System.Drawing.Size(42, 22);
            this.pointXtext.TabIndex = 0;
            // 
            // pointYtext
            // 
            this.pointYtext.Location = new System.Drawing.Point(89, 12);
            this.pointYtext.Name = "pointYtext";
            this.pointYtext.Size = new System.Drawing.Size(42, 22);
            this.pointYtext.TabIndex = 1;
            // 
            // pointTtext
            // 
            this.pointTtext.Location = new System.Drawing.Point(152, 12);
            this.pointTtext.Name = "pointTtext";
            this.pointTtext.Size = new System.Drawing.Size(42, 22);
            this.pointTtext.TabIndex = 2;
            // 
            // CurvePointsText
            // 
            this.CurvePointsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurvePointsText.Location = new System.Drawing.Point(12, 411);
            this.CurvePointsText.Multiline = true;
            this.CurvePointsText.Name = "CurvePointsText";
            this.CurvePointsText.ReadOnly = true;
            this.CurvePointsText.Size = new System.Drawing.Size(710, 62);
            this.CurvePointsText.TabIndex = 3;
            this.CurvePointsText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Curve2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 485);
            this.Controls.Add(this.CurvePointsText);
            this.Controls.Add(this.pointTtext);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.pointYtext);
            this.Controls.Add(this.pointXtext);
            this.Name = "Curve2D";
            this.Text = "Curve2D";
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.framebuffer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TextBox pointXtext;
        private System.Windows.Forms.TextBox pointYtext;
        private System.Windows.Forms.TextBox pointTtext;
        private System.Windows.Forms.PictureBox framebuffer;
        private System.Windows.Forms.TextBox CurvePointsText;
    }
}