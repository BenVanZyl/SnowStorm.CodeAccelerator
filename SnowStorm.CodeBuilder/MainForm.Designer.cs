
namespace SnowStorm.CodeBuilder
{
    partial class MainForm
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
            this.ConnectionStringText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ListOfTables = new System.Windows.Forms.ListBox();
            this.ListOfColumns = new System.Windows.Forms.DataGridView();
            this.ListOfPrimaryKeys = new System.Windows.Forms.ListBox();
            this.ListOfForeignKeys = new System.Windows.Forms.ListBox();
            this.GeneratedCodeText = new System.Windows.Forms.TextBox();
            this.GenerateCodeButton = new System.Windows.Forms.Button();
            this.CodeOptions = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListOfColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectionStringText
            // 
            this.ConnectionStringText.Location = new System.Drawing.Point(0, 12);
            this.ConnectionStringText.Name = "ConnectionStringText";
            this.ConnectionStringText.Size = new System.Drawing.Size(788, 23);
            this.ConnectionStringText.TabIndex = 0;
            this.ConnectionStringText.Text = "Server=(localdb)\\mssqllocaldb;Database=SnowBirdData;Trusted_Connection=True;Multi" +
    "pleActiveResultSets=true";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OpenDb";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ListOfTables
            // 
            this.ListOfTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListOfTables.FormattingEnabled = true;
            this.ListOfTables.ItemHeight = 15;
            this.ListOfTables.Location = new System.Drawing.Point(0, 97);
            this.ListOfTables.Name = "ListOfTables";
            this.ListOfTables.Size = new System.Drawing.Size(275, 364);
            this.ListOfTables.TabIndex = 2;
            this.ListOfTables.DoubleClick += new System.EventHandler(this.ListOfTables_DoubleClick);
            // 
            // ListOfColumns
            // 
            this.ListOfColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListOfColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListOfColumns.Location = new System.Drawing.Point(294, 97);
            this.ListOfColumns.Name = "ListOfColumns";
            this.ListOfColumns.RowTemplate.Height = 25;
            this.ListOfColumns.Size = new System.Drawing.Size(463, 364);
            this.ListOfColumns.TabIndex = 3;
            // 
            // ListOfPrimaryKeys
            // 
            this.ListOfPrimaryKeys.FormattingEnabled = true;
            this.ListOfPrimaryKeys.ItemHeight = 15;
            this.ListOfPrimaryKeys.Location = new System.Drawing.Point(763, 97);
            this.ListOfPrimaryKeys.Name = "ListOfPrimaryKeys";
            this.ListOfPrimaryKeys.Size = new System.Drawing.Size(176, 139);
            this.ListOfPrimaryKeys.TabIndex = 4;
            // 
            // ListOfForeignKeys
            // 
            this.ListOfForeignKeys.FormattingEnabled = true;
            this.ListOfForeignKeys.ItemHeight = 15;
            this.ListOfForeignKeys.Location = new System.Drawing.Point(763, 242);
            this.ListOfForeignKeys.Name = "ListOfForeignKeys";
            this.ListOfForeignKeys.Size = new System.Drawing.Size(176, 139);
            this.ListOfForeignKeys.TabIndex = 5;
            // 
            // GeneratedCodeText
            // 
            this.GeneratedCodeText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GeneratedCodeText.Location = new System.Drawing.Point(945, 12);
            this.GeneratedCodeText.Multiline = true;
            this.GeneratedCodeText.Name = "GeneratedCodeText";
            this.GeneratedCodeText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.GeneratedCodeText.Size = new System.Drawing.Size(445, 443);
            this.GeneratedCodeText.TabIndex = 6;
            // 
            // GenerateCodeButton
            // 
            this.GenerateCodeButton.Location = new System.Drawing.Point(92, 41);
            this.GenerateCodeButton.Name = "GenerateCodeButton";
            this.GenerateCodeButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateCodeButton.TabIndex = 7;
            this.GenerateCodeButton.Text = "Generate";
            this.GenerateCodeButton.UseVisualStyleBackColor = true;
            this.GenerateCodeButton.Click += new System.EventHandler(this.GenerateCodeButton_Click);
            // 
            // CodeOptions
            // 
            this.CodeOptions.FormattingEnabled = true;
            this.CodeOptions.Location = new System.Drawing.Point(551, 41);
            this.CodeOptions.Name = "CodeOptions";
            this.CodeOptions.Size = new System.Drawing.Size(237, 23);
            this.CodeOptions.TabIndex = 8;
            this.CodeOptions.SelectedIndexChanged += new System.EventHandler(this.CodeOptions_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 467);
            this.Controls.Add(this.CodeOptions);
            this.Controls.Add(this.GenerateCodeButton);
            this.Controls.Add(this.GeneratedCodeText);
            this.Controls.Add(this.ListOfForeignKeys);
            this.Controls.Add(this.ListOfPrimaryKeys);
            this.Controls.Add(this.ListOfColumns);
            this.Controls.Add(this.ListOfTables);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ConnectionStringText);
            this.Name = "MainForm";
            this.Text = "SnowStorm - Code Builder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListOfColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ConnectionStringText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox ListOfTables;
        private System.Windows.Forms.DataGridView ListOfColumns;
        private System.Windows.Forms.ListBox ListOfPrimaryKeys;
        private System.Windows.Forms.ListBox ListOfForeignKeys;
        private System.Windows.Forms.TextBox GeneratedCodeText;
        private System.Windows.Forms.Button GenerateCodeButton;
        private System.Windows.Forms.ComboBox CodeOptions;
    }
}

