namespace ImagenForm
{
    partial class FormPixelar
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
            this.bar = new System.Windows.Forms.TrackBar();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.inputX = new System.Windows.Forms.TextBox();
            this.inputY = new System.Windows.Forms.TextBox();
            this.bar10 = new System.Windows.Forms.Label();
            this.bar5 = new System.Windows.Forms.Label();
            this.bar1 = new System.Windows.Forms.Label();
            this.inputX2 = new System.Windows.Forms.TextBox();
            this.inputY2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).BeginInit();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.LargeChange = 1;
            this.bar.Location = new System.Drawing.Point(27, 52);
            this.bar.Minimum = 1;
            this.bar.Name = "bar";
            this.bar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bar.Size = new System.Drawing.Size(217, 56);
            this.bar.TabIndex = 0;
            this.bar.Value = 10;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Checked = true;
            this.checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox.Location = new System.Drawing.Point(38, 271);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(128, 21);
            this.checkBox.TabIndex = 1;
            this.checkBox.Text = "Toda la imagen";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tamaño de Pixelación";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(98, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Pixelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "X inicial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y inicial";
            // 
            // inputX
            // 
            this.inputX.Enabled = false;
            this.inputX.Location = new System.Drawing.Point(38, 117);
            this.inputX.Name = "inputX";
            this.inputX.Size = new System.Drawing.Size(132, 22);
            this.inputX.TabIndex = 6;
            // 
            // inputY
            // 
            this.inputY.Enabled = false;
            this.inputY.Location = new System.Drawing.Point(38, 151);
            this.inputY.Name = "inputY";
            this.inputY.Size = new System.Drawing.Size(132, 22);
            this.inputY.TabIndex = 7;
            // 
            // bar10
            // 
            this.bar10.AutoSize = true;
            this.bar10.Location = new System.Drawing.Point(35, 91);
            this.bar10.Name = "bar10";
            this.bar10.Size = new System.Drawing.Size(24, 17);
            this.bar10.TabIndex = 8;
            this.bar10.Text = "10";
            // 
            // bar5
            // 
            this.bar5.AutoSize = true;
            this.bar5.Location = new System.Drawing.Point(101, 91);
            this.bar5.Name = "bar5";
            this.bar5.Size = new System.Drawing.Size(72, 17);
            this.bar5.TabIndex = 9;
            this.bar5.Text = "7            4";
            // 
            // bar1
            // 
            this.bar1.AutoSize = true;
            this.bar1.Location = new System.Drawing.Point(217, 91);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(16, 17);
            this.bar1.TabIndex = 10;
            this.bar1.Text = "1";
            // 
            // inputX2
            // 
            this.inputX2.Enabled = false;
            this.inputX2.Location = new System.Drawing.Point(38, 186);
            this.inputX2.Name = "inputX2";
            this.inputX2.Size = new System.Drawing.Size(132, 22);
            this.inputX2.TabIndex = 11;
            // 
            // inputY2
            // 
            this.inputY2.Enabled = false;
            this.inputY2.Location = new System.Drawing.Point(38, 225);
            this.inputY2.Name = "inputY2";
            this.inputY2.Size = new System.Drawing.Size(132, 22);
            this.inputY2.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "X final";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(177, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Y final";
            // 
            // FormPixelar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 354);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inputY2);
            this.Controls.Add(this.inputX2);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.bar5);
            this.Controls.Add(this.bar10);
            this.Controls.Add(this.inputY);
            this.Controls.Add(this.inputX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.bar);
            this.Name = "FormPixelar";
            this.Text = "FormPixelar";
            ((System.ComponentModel.ISupportInitialize)(this.bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar bar;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inputX;
        private System.Windows.Forms.TextBox inputY;
        private System.Windows.Forms.Label bar10;
        private System.Windows.Forms.Label bar5;
        private System.Windows.Forms.Label bar1;
        private System.Windows.Forms.TextBox inputX2;
        private System.Windows.Forms.TextBox inputY2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}