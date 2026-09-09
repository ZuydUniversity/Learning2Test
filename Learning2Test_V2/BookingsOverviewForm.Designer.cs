namespace Learning2Test_V2
{
    partial class BookingsOverviewForm
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
            dataGridViewBookings = new DataGridView();
            labelTotalBookings = new Label();
            labelNoBookings = new Label();
            buttonRefresh = new Button();
            buttonClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBookings).BeginInit();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.Location = new Point(30, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(1140, 35);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Overzicht van alle boekingen";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridViewBookings
            // 
            dataGridViewBookings.AllowUserToAddRows = false;
            dataGridViewBookings.AllowUserToDeleteRows = false;
            dataGridViewBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBookings.BackgroundColor = SystemColors.ControlLight;
            dataGridViewBookings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBookings.Location = new Point(30, 70);
            dataGridViewBookings.Name = "dataGridViewBookings";
            dataGridViewBookings.ReadOnly = true;
            dataGridViewBookings.RowHeadersWidth = 51;
            dataGridViewBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBookings.Size = new Size(1140, 450);
            dataGridViewBookings.TabIndex = 1;
            // 
            // labelTotalBookings
            // 
            labelTotalBookings.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelTotalBookings.Location = new Point(30, 530);
            labelTotalBookings.Name = "labelTotalBookings";
            labelTotalBookings.Size = new Size(600, 25);
            labelTotalBookings.TabIndex = 2;
            labelTotalBookings.Text = "Totaal aantal boekingen: 0";
            // 
            // labelNoBookings
            // 
            labelNoBookings.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            labelNoBookings.ForeColor = Color.Gray;
            labelNoBookings.Location = new Point(30, 250);
            labelNoBookings.Name = "labelNoBookings";
            labelNoBookings.Size = new Size(1140, 30);
            labelNoBookings.TabIndex = 3;
            labelNoBookings.Text = "Er zijn nog geen boekingen gemaakt.";
            labelNoBookings.TextAlign = ContentAlignment.MiddleCenter;
            labelNoBookings.Visible = false;
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = Color.DodgerBlue;
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonRefresh.ForeColor = Color.White;
            buttonRefresh.Location = new Point(850, 525);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(150, 40);
            buttonRefresh.TabIndex = 4;
            buttonRefresh.Text = "Vernieuwen";
            buttonRefresh.UseVisualStyleBackColor = false;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = Color.Gray;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonClose.ForeColor = Color.White;
            buttonClose.Location = new Point(1020, 525);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(150, 40);
            buttonClose.TabIndex = 5;
            buttonClose.Text = "Sluiten";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // BookingsOverviewForm
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1200, 585);
            Controls.Add(labelTitle);
            Controls.Add(dataGridViewBookings);
            Controls.Add(labelTotalBookings);
            Controls.Add(labelNoBookings);
            Controls.Add(buttonRefresh);
            Controls.Add(buttonClose);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "BookingsOverviewForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Boekingen Overzicht";
            ((System.ComponentModel.ISupportInitialize)dataGridViewBookings).EndInit();
            ResumeLayout(false);
        }

        private Label labelTitle;
        private DataGridView dataGridViewBookings;
        private Label labelTotalBookings;
        private Label labelNoBookings;
        private Button buttonRefresh;
        private Button buttonClose;
    }
}
