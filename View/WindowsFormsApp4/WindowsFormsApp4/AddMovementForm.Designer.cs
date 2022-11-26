namespace WindowsFormsApp4
{
    partial class AddMovementForm
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
            this.LabelTypeMovement = new System.Windows.Forms.Label();
            this.TypeMoveComboBox = new System.Windows.Forms.ComboBox();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.TimeTextBox = new System.Windows.Forms.TextBox();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
#if DEBUG
            this.RandomButton = new System.Windows.Forms.Button();
#endif
            this.GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelTypeMovement
            // 
            this.LabelTypeMovement.AutoSize = true;
            this.LabelTypeMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelTypeMovement.Location = new System.Drawing.Point(13, 13);
            this.LabelTypeMovement.Name = "LabelTypeMovement";
            this.LabelTypeMovement.Size = new System.Drawing.Size(102, 16);
            this.LabelTypeMovement.TabIndex = 0;
            this.LabelTypeMovement.Text = "Тип движения:";
            // 
            // TypeMoveComboBox
            // 
            this.TypeMoveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeMoveComboBox.FormattingEnabled = true;
            this.TypeMoveComboBox.Items.AddRange(new object[] {
            "Равномерное",
            "Равноускоренное",
            "Колебательное"});
            this.TypeMoveComboBox.Location = new System.Drawing.Point(121, 12);
            this.TypeMoveComboBox.Name = "TypeMoveComboBox";
            this.TypeMoveComboBox.Size = new System.Drawing.Size(182, 21);
            this.TypeMoveComboBox.TabIndex = 1;
            this.TypeMoveComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.TimeTextBox);
            this.GroupBox.Controls.Add(this.TimeLabel);
            this.GroupBox.Location = new System.Drawing.Point(16, 54);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(287, 221);
            this.GroupBox.TabIndex = 2;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Параметры";
            // 
            // TimeTextBox
            // 
            this.TimeTextBox.Location = new System.Drawing.Point(170, 21);
            this.TimeTextBox.Name = "TimeTextBox";
            this.TimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.TimeTextBox.TabIndex = 1;
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(6, 24);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(55, 13);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "Время, с:";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(55, 290);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Ок";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(174, 290);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
#if DEBUG
            // 
            // RandomButton
            // 
            this.RandomButton.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.GameDesign;
            this.RandomButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RandomButton.Location = new System.Drawing.Point(260, 290);
            this.RandomButton.Name = "RandomButton";
            this.RandomButton.Size = new System.Drawing.Size(26, 23);
            this.RandomButton.TabIndex = 5;
            this.RandomButton.UseVisualStyleBackColor = true;
            this.RandomButton.Click += new System.EventHandler(this.RandomButton_Click);
#endif
            // 
            // AddMovementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(311, 331);
#if DEBUG
            this.Controls.Add(this.RandomButton);
#endif
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.TypeMoveComboBox);
            this.Controls.Add(this.LabelTypeMovement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddMovementForm";
            this.Text = "Добавление движения";
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

#endregion

        private System.Windows.Forms.Label LabelTypeMovement;
        private System.Windows.Forms.ComboBox TypeMoveComboBox;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.TextBox TimeTextBox;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
#if DEBUG
        private System.Windows.Forms.Button RandomButton;
#endif
    }
}