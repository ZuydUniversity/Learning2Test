namespace Learning2Test_V2
{
    partial class HolidaySearchMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxFilters = new GroupBox();
            label1 = new Label();
            checkedListBoxCountries = new CheckedListBox();
            buttonReset = new Button();
            buttonSearch = new Button();
            labelAdults = new Label();
            numericUpDownAdults = new NumericUpDown();
            labelChildren = new Label();
            numericUpDownChildren = new NumericUpDown();
            labelStartDate = new Label();
            dateTimePickerStart = new DateTimePicker();
            labelEndDate = new Label();
            dateTimePickerEnd = new DateTimePicker();
            groupBoxResults = new GroupBox();
            labelAvailable = new Label();
            listBoxAvailable = new ListBox();
            labelUnavailable = new Label();
            listBoxUnavailable = new ListBox();
            groupBoxFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAdults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownChildren).BeginInit();
            groupBoxResults.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxFilters
            // 
            groupBoxFilters.BackColor = Color.White;
            groupBoxFilters.Controls.Add(label1);
            groupBoxFilters.Controls.Add(checkedListBoxCountries);
            groupBoxFilters.Controls.Add(buttonReset);
            groupBoxFilters.Controls.Add(buttonSearch);
            groupBoxFilters.Controls.Add(labelAdults);
            groupBoxFilters.Controls.Add(numericUpDownAdults);
            groupBoxFilters.Controls.Add(labelChildren);
            groupBoxFilters.Controls.Add(numericUpDownChildren);
            groupBoxFilters.Controls.Add(labelStartDate);
            groupBoxFilters.Controls.Add(dateTimePickerStart);
            groupBoxFilters.Controls.Add(labelEndDate);
            groupBoxFilters.Controls.Add(dateTimePickerEnd);
            groupBoxFilters.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxFilters.Location = new Point(30, 30);
            groupBoxFilters.Name = "groupBoxFilters";
            groupBoxFilters.Size = new Size(452, 545);
            groupBoxFilters.TabIndex = 0;
            groupBoxFilters.TabStop = false;
            groupBoxFilters.Text = "Zoekcriteria";
            // 
            // label1
            // 
            label1.Location = new Point(20, 30);
            label1.Name = "label1";
            label1.Size = new Size(120, 25);
            label1.TabIndex = 0;
            label1.Text = "Welke landen?";
            // 
            // checkedListBoxCountries
            // 
            checkedListBoxCountries.Location = new Point(20, 60);
            checkedListBoxCountries.Name = "checkedListBoxCountries";
            checkedListBoxCountries.Size = new Size(410, 220);
            checkedListBoxCountries.TabIndex = 1;
            // 
            // buttonReset
            // 
            buttonReset.BackColor = Color.LightGray;
            buttonReset.FlatStyle = FlatStyle.Flat;
            buttonReset.ForeColor = Color.Black;
            buttonReset.Location = new Point(20, 483);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(150, 40);
            buttonReset.TabIndex = 3;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = false;
            buttonReset.Click += buttonReset_Click;
            // 
            // buttonSearch
            // 
            buttonSearch.BackColor = Color.DodgerBlue;
            buttonSearch.FlatStyle = FlatStyle.Flat;
            buttonSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonSearch.ForeColor = Color.White;
            buttonSearch.Location = new Point(280, 483);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(150, 40);
            buttonSearch.TabIndex = 2;
            buttonSearch.Text = "Zoeken";
            buttonSearch.UseVisualStyleBackColor = false;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // labelAdults
            // 
            labelAdults.Location = new Point(20, 322);
            labelAdults.Name = "labelAdults";
            labelAdults.Size = new Size(161, 25);
            labelAdults.TabIndex = 2;
            labelAdults.Text = "Aantal volwassenen:";
            // 
            // numericUpDownAdults
            // 
            numericUpDownAdults.Location = new Point(183, 320);
            numericUpDownAdults.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownAdults.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownAdults.Name = "numericUpDownAdults";
            numericUpDownAdults.Size = new Size(60, 32);
            numericUpDownAdults.TabIndex = 3;
            numericUpDownAdults.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelChildren
            // 
            labelChildren.Location = new Point(20, 362);
            labelChildren.Name = "labelChildren";
            labelChildren.Size = new Size(150, 25);
            labelChildren.TabIndex = 4;
            labelChildren.Text = "Aantal kinderen:";
            // 
            // numericUpDownChildren
            // 
            numericUpDownChildren.Location = new Point(183, 360);
            numericUpDownChildren.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownChildren.Name = "numericUpDownChildren";
            numericUpDownChildren.Size = new Size(60, 32);
            numericUpDownChildren.TabIndex = 5;
            // 
            // labelStartDate
            // 
            labelStartDate.Location = new Point(20, 402);
            labelStartDate.Name = "labelStartDate";
            labelStartDate.Size = new Size(100, 25);
            labelStartDate.TabIndex = 6;
            labelStartDate.Text = "Startdatum:";
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dateTimePickerStart.Location = new Point(183, 397);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new Size(247, 30);
            dateTimePickerStart.TabIndex = 7;
            // 
            // labelEndDate
            // 
            labelEndDate.Location = new Point(20, 442);
            labelEndDate.Name = "labelEndDate";
            labelEndDate.Size = new Size(100, 25);
            labelEndDate.TabIndex = 8;
            labelEndDate.Text = "Einddatum:";
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dateTimePickerEnd.Location = new Point(183, 437);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(247, 30);
            dateTimePickerEnd.TabIndex = 9;
            dateTimePickerEnd.Value = new DateTime(2025, 10, 15, 0, 0, 0, 0);
            // 
            // groupBoxResults
            // 
            groupBoxResults.BackColor = Color.White;
            groupBoxResults.Controls.Add(labelAvailable);
            groupBoxResults.Controls.Add(listBoxAvailable);
            groupBoxResults.Controls.Add(labelUnavailable);
            groupBoxResults.Controls.Add(listBoxUnavailable);
            groupBoxResults.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxResults.Location = new Point(502, 27);
            groupBoxResults.Name = "groupBoxResults";
            groupBoxResults.Size = new Size(998, 548);
            groupBoxResults.TabIndex = 1;
            groupBoxResults.TabStop = false;
            groupBoxResults.Text = "Resultaten";
            // 
            // labelAvailable
            // 
            labelAvailable.Location = new Point(30, 40);
            labelAvailable.Name = "labelAvailable";
            labelAvailable.Size = new Size(200, 25);
            labelAvailable.TabIndex = 0;
            labelAvailable.Text = "Beschikbare steden";
            // 
            // listBoxAvailable
            // 
            listBoxAvailable.ItemHeight = 25;
            listBoxAvailable.Location = new Point(30, 70);
            listBoxAvailable.MultiColumn = true;
            listBoxAvailable.Name = "listBoxAvailable";
            listBoxAvailable.Size = new Size(946, 204);
            listBoxAvailable.TabIndex = 1;
            // 
            // labelUnavailable
            // 
            labelUnavailable.Location = new Point(30, 363);
            labelUnavailable.Name = "labelUnavailable";
            labelUnavailable.Size = new Size(200, 25);
            labelUnavailable.TabIndex = 2;
            labelUnavailable.Text = "Niet beschikbaar (datum)";
            // 
            // listBoxUnavailable
            // 
            listBoxUnavailable.ItemHeight = 25;
            listBoxUnavailable.Location = new Point(30, 391);
            listBoxUnavailable.Name = "listBoxUnavailable";
            listBoxUnavailable.Size = new Size(946, 129);
            listBoxUnavailable.TabIndex = 3;
            // 
            // HolidaySearchMain
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1512, 587);
            Controls.Add(groupBoxFilters);
            Controls.Add(groupBoxResults);
            Font = new Font("Segoe UI", 10F);
            Name = "HolidaySearchMain";
            Text = "Vakantie Zoeker";
            groupBoxFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownAdults).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownChildren).EndInit();
            groupBoxResults.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button buttonSearch;
        private Button buttonReset;
        private DateTimePicker dateTimePickerStart;
        private DateTimePicker dateTimePickerEnd;
        private GroupBox groupBoxFilters;
        private GroupBox groupBoxResults;
        private Label labelAdults;
        private Label labelChildren;
        private Label labelStartDate;
        private Label labelEndDate;
        private Label labelAvailable;
        private Label labelUnavailable;
        private ListBox listBoxAvailable;
        private ListBox listBoxUnavailable;
        private Label label1;
        private CheckedListBox checkedListBoxCountries;
        private NumericUpDown numericUpDownChildren;
        private NumericUpDown numericUpDownAdults;
    }
}
