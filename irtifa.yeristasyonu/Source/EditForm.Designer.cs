namespace irtifa
{
    class ComboItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }


    partial class EditForm
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
            this.altitudeTextBox = new System.Windows.Forms.TextBox();
            this.applyEditButton = new System.Windows.Forms.Button();
            this.cancelEditButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.latitudeTextBox = new System.Windows.Forms.TextBox();
            this.longitudeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.speedBox = new System.Windows.Forms.TextBox();

            this.roleLabel = new System.Windows.Forms.Label();
            this.roleBox = new System.Windows.Forms.TextBox();

            this.roleCombobox = new System.Windows.Forms.ComboBox();

            this.SuspendLayout();
            // 
            // altitudeTextBox
            // 
            this.altitudeTextBox.Location = new System.Drawing.Point(99, 80);
            this.altitudeTextBox.Name = "altitudeTextBox";
            this.altitudeTextBox.Size = new System.Drawing.Size(199, 20);
            this.altitudeTextBox.TabIndex = 0;
            // 
            // applyEditButton
            // 
            this.applyEditButton.Location = new System.Drawing.Point(12, 183);
            this.applyEditButton.Name = "applyEditButton";
            this.applyEditButton.Size = new System.Drawing.Size(75, 23);
            this.applyEditButton.TabIndex = 1;
            this.applyEditButton.Text = "Uygula";
            this.applyEditButton.UseVisualStyleBackColor = true;
            this.applyEditButton.Click += new System.EventHandler(this.applyEditButton_Click);
            // 
            // cancelEditButton
            // 
            this.cancelEditButton.Location = new System.Drawing.Point(223, 183);
            this.cancelEditButton.Name = "cancelEditButton";
            this.cancelEditButton.Size = new System.Drawing.Size(75, 23);
            this.cancelEditButton.TabIndex = 2;
            this.cancelEditButton.Text = "İptal";
            this.cancelEditButton.UseVisualStyleBackColor = true;
            this.cancelEditButton.Click += new System.EventHandler(this.cancelEditButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Yükseklik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enlem";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Boylam";
            // 
            // latitudeTextBox
            // 
            this.latitudeTextBox.Location = new System.Drawing.Point(99, 6);
            this.latitudeTextBox.Name = "latitudeTextBox";
            this.latitudeTextBox.Size = new System.Drawing.Size(199, 20);
            this.latitudeTextBox.TabIndex = 6;
            // 
            // longitudeTextBox
            // 
            this.longitudeTextBox.Location = new System.Drawing.Point(99, 44);
            this.longitudeTextBox.Name = "longitudeTextBox";
            this.longitudeTextBox.Size = new System.Drawing.Size(199, 20);
            this.longitudeTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Hız";
            // 
            // speedBox
            // 
            this.speedBox.Location = new System.Drawing.Point(99, 116);
            this.speedBox.Name = "speedBox";
            this.speedBox.Size = new System.Drawing.Size(199, 20);
            this.speedBox.TabIndex = 9;
            // 
            // roleLabel
            // 
            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(12, 157);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(22, 13);
            this.roleLabel.TabIndex = 8;
            this.roleLabel.Text = "Rol";
            // 
            // roleBox
            // 
            /*this.roleBox.Location = new System.Drawing.Point(99, 154);
            this.roleBox.Name = "roleBox";
            this.roleBox.Size = new System.Drawing.Size(120, 20);
            this.roleBox.TabIndex = 9;*/
            // 
            // roleCombobox
            // 
            this.roleCombobox.Location = new System.Drawing.Point(99, 154);
            this.roleCombobox.Name = "roleCombobox";
            this.roleCombobox.Size = new System.Drawing.Size(100, 20);
            this.roleCombobox.TabIndex = 9;
            this.roleCombobox.DataSource = new string[] { "NORMAL", "POI" };
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 230);
            this.Controls.Add(this.speedBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.longitudeTextBox);
            this.Controls.Add(this.latitudeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelEditButton);
            this.Controls.Add(this.applyEditButton);
            this.Controls.Add(this.altitudeTextBox);
            this.Load += new System.EventHandler(this.EditForm_Load);

            this.Controls.Add(this.roleLabel);
            //this.Controls.Add(this.roleBox);

            this.Controls.Add(this.roleCombobox);

            this.Name = "EditForm";
            this.Text = "Noktayı Düzenle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox altitudeTextBox;
        private System.Windows.Forms.Button applyEditButton;
        private System.Windows.Forms.Button cancelEditButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox latitudeTextBox;
        private System.Windows.Forms.TextBox longitudeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox speedBox;

        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.TextBox roleBox;

        private System.Windows.Forms.ComboBox roleCombobox;
    }
}