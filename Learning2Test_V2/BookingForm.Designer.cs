namespace Learning2Test_V2
{
    partial class BookingForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTitle = new Label();
            labelBookingInfo = new Label();
            groupBoxBookerInfo = new GroupBox();
            labelName = new Label();
            textBoxName = new TextBox();
            labelBirthDate = new Label();
            dateTimePickerBirthDate = new DateTimePicker();
            buttonConfirm = new Button();
            buttonCancel = new Button();
            groupBoxBookerInfo.SuspendLayout();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.Location = new Point(30, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(540, 35);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Boek uw vakantie";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelBookingInfo
            // 
            labelBookingInfo.BackColor = Color.LightBlue;
            labelBookingInfo.BorderStyle = BorderStyle.FixedSingle;
            labelBookingInfo.Font = new Font("Segoe UI", 10F);
            labelBookingInfo.Location = new Point(30, 70);
            labelBookingInfo.Name = "labelBookingInfo";
            labelBookingInfo.Padding = new Padding(10);
            labelBookingInfo.Size = new Size(540, 120);
            labelBookingInfo.TabIndex = 1;
            labelBookingInfo.Text = "Boeking Details";
            // 
            // groupBoxBookerInfo
            // 
            groupBoxBookerInfo.BackColor = Color.White;
            groupBoxBookerInfo.Controls.Add(labelName);
            groupBoxBookerInfo.Controls.Add(textBoxName);
            groupBoxBookerInfo.Controls.Add(labelBirthDate);
            groupBoxBookerInfo.Controls.Add(dateTimePickerBirthDate);
            groupBoxBookerInfo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxBookerInfo.Location = new Point(30, 210);
            groupBoxBookerInfo.Name = "groupBoxBookerInfo";
            groupBoxBookerInfo.Padding = new Padding(10);
            groupBoxBookerInfo.Size = new Size(540, 150);
            groupBoxBookerInfo.TabIndex = 2;
            groupBoxBookerInfo.TabStop = false;
            groupBoxBookerInfo.Text = "Uw gegevens";
            // 
            // labelName
            // 
            labelName.Font = new Font("Segoe UI", 10F);
            labelName.Location = new Point(20, 40);
            labelName.Name = "labelName";
            labelName.Size = new Size(150, 25);
            labelName.TabIndex = 0;
            labelName.Text = "Naam:";
            // 
            // textBoxName
            // 
            textBoxName.Font = new Font("Segoe UI", 10F);
            textBoxName.Location = new Point(180, 37);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(340, 30);
            textBoxName.TabIndex = 1;
            // 
            // labelBirthDate
            // 
            labelBirthDate.Font = new Font("Segoe UI", 10F);
            labelBirthDate.Location = new Point(20, 90);
            labelBirthDate.Name = "labelBirthDate";
            labelBirthDate.Size = new Size(150, 25);
            labelBirthDate.TabIndex = 2;
            labelBirthDate.Text = "Geboortedatum:";
            // 
            // dateTimePickerBirthDate
            // 
            dateTimePickerBirthDate.Font = new Font("Segoe UI", 10F);
            dateTimePickerBirthDate.Format = DateTimePickerFormat.Short;
            dateTimePickerBirthDate.Location = new Point(180, 87);
            dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
            dateTimePickerBirthDate.Size = new Size(200, 30);
            dateTimePickerBirthDate.TabIndex = 3;
            // 
            // buttonConfirm
            // 
            buttonConfirm.BackColor = Color.Green;
            buttonConfirm.FlatStyle = FlatStyle.Flat;
            buttonConfirm.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonConfirm.ForeColor = Color.White;
            buttonConfirm.Location = new Point(350, 380);
            buttonConfirm.Name = "buttonConfirm";
            buttonConfirm.Size = new Size(220, 45);
            buttonConfirm.TabIndex = 3;
            buttonConfirm.Text = "Bevestig boeking";
            buttonConfirm.UseVisualStyleBackColor = false;
            buttonConfirm.Click += buttonConfirm_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.Gray;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(30, 380);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(150, 45);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Annuleren";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // BookingForm
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(600, 450);
            Controls.Add(labelTitle);
            Controls.Add(labelBookingInfo);
            Controls.Add(groupBoxBookerInfo);
            Controls.Add(buttonConfirm);
            Controls.Add(buttonCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BookingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Boeking maken";
            groupBoxBookerInfo.ResumeLayout(false);
            groupBoxBookerInfo.PerformLayout();
            ResumeLayout(false);
        }

        private Label labelTitle;
        private Label labelBookingInfo;
        private GroupBox groupBoxBookerInfo;
        private Label labelName;
        private TextBox textBoxName;
        private Label labelBirthDate;
        private DateTimePicker dateTimePickerBirthDate;
        private Button buttonConfirm;
        private Button buttonCancel;
    }
}
