
namespace TestBossCreater
{
    partial class MainMenu
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
            button1 = new Button();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            pictureBoxDataGrid = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDataGrid).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(471, 36);
            button1.Name = "button1";
            button1.Size = new Size(266, 29);
            button1.TabIndex = 0;
            button1.Text = "Пройти тест";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(471, 94);
            button2.Name = "button2";
            button2.Size = new Size(266, 29);
            button2.TabIndex = 1;
            button2.Text = "Создать тест";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(41, 23);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(378, 372);
            dataGridView1.TabIndex = 2;
            // 
            // pictureBoxDataGrid
            // 
            pictureBoxDataGrid.Image = Properties.Resources.emptyDataGrid;
            pictureBoxDataGrid.Location = new Point(41, 23);
            pictureBoxDataGrid.Name = "pictureBoxDataGrid";
            pictureBoxDataGrid.Size = new Size(378, 372);
            pictureBoxDataGrid.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxDataGrid.TabIndex = 3;
            pictureBoxDataGrid.TabStop = false;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBoxDataGrid);
            Name = "MainMenu";
            Text = "BossTestCreater";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDataGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private DataGridView dataGridView1;
        private PictureBox pictureBoxDataGrid;
    }
}
