
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            button1 = new Button();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            pictureBoxDataGrid = new PictureBox();
            notifyIcon1 = new NotifyIcon(components);
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            menuStrip1 = new MenuStrip();
            статистикаToolStripMenuItem = new ToolStripMenuItem();
            пользователяToolStripMenuItem = new ToolStripMenuItem();
            тестаToolStripMenuItem = new ToolStripMenuItem();
            выбранныйТестToolStripMenuItem = new ToolStripMenuItem();
            всеТестыToolStripMenuItem = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            обАвтореToolStripMenuItem = new ToolStripMenuItem();
            манипуляцииСТестомToolStripMenuItem = new ToolStripMenuItem();
            редактированиеToolStripMenuItem = new ToolStripMenuItem();
            удалениеТестаToolStripMenuItem = new ToolStripMenuItem();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDataGrid).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(753, 301);
            button1.Name = "button1";
            button1.Size = new Size(275, 63);
            button1.TabIndex = 0;
            button1.Text = "Пройти тест";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(753, 370);
            button2.Name = "button2";
            button2.Size = new Size(275, 65);
            button2.TabIndex = 1;
            button2.Text = "Создать тест";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(30, 35);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(691, 400);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // pictureBoxDataGrid
            // 
            pictureBoxDataGrid.Image = Properties.Resources.emptyDataGrid;
            pictureBoxDataGrid.Location = new Point(35, 35);
            pictureBoxDataGrid.Name = "pictureBoxDataGrid";
            pictureBoxDataGrid.Size = new Size(679, 372);
            pictureBoxDataGrid.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxDataGrid.TabIndex = 3;
            pictureBoxDataGrid.TabStop = false;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(753, 35);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(275, 259);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Кто ты воин?";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ahillesMain1;
            pictureBox1.Location = new Point(44, 34);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(184, 171);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(25, 217);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(229, 27);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Highlight;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { статистикаToolStripMenuItem, справкаToolStripMenuItem, обАвтореToolStripMenuItem, манипуляцииСТестомToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1059, 28);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // статистикаToolStripMenuItem
            // 
            статистикаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { пользователяToolStripMenuItem, тестаToolStripMenuItem });
            статистикаToolStripMenuItem.ForeColor = Color.White;
            статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            статистикаToolStripMenuItem.Size = new Size(98, 24);
            статистикаToolStripMenuItem.Text = "Статистика";
            // 
            // пользователяToolStripMenuItem
            // 
            пользователяToolStripMenuItem.Name = "пользователяToolStripMenuItem";
            пользователяToolStripMenuItem.Size = new Size(190, 26);
            пользователяToolStripMenuItem.Text = "Пользователя";
            пользователяToolStripMenuItem.Click += пользователяToolStripMenuItem_Click;
            // 
            // тестаToolStripMenuItem
            // 
            тестаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { выбранныйТестToolStripMenuItem, всеТестыToolStripMenuItem });
            тестаToolStripMenuItem.Name = "тестаToolStripMenuItem";
            тестаToolStripMenuItem.Size = new Size(190, 26);
            тестаToolStripMenuItem.Text = "Теста";
            тестаToolStripMenuItem.Click += тестаToolStripMenuItem_Click;
            // 
            // выбранныйТестToolStripMenuItem
            // 
            выбранныйТестToolStripMenuItem.Name = "выбранныйТестToolStripMenuItem";
            выбранныйТестToolStripMenuItem.Size = new Size(207, 26);
            выбранныйТестToolStripMenuItem.Text = "Выбранный тест";
            выбранныйТестToolStripMenuItem.Click += выбранныйТестToolStripMenuItem_Click;
            // 
            // всеТестыToolStripMenuItem
            // 
            всеТестыToolStripMenuItem.Name = "всеТестыToolStripMenuItem";
            всеТестыToolStripMenuItem.Size = new Size(207, 26);
            всеТестыToolStripMenuItem.Text = "Все тесты";
            всеТестыToolStripMenuItem.Click += всеТестыToolStripMenuItem_Click;
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.ForeColor = Color.White;
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(81, 24);
            справкаToolStripMenuItem.Text = "Справка";
            справкаToolStripMenuItem.Click += справкаToolStripMenuItem_Click;
            // 
            // обАвтореToolStripMenuItem
            // 
            обАвтореToolStripMenuItem.ForeColor = SystemColors.Control;
            обАвтореToolStripMenuItem.Name = "обАвтореToolStripMenuItem";
            обАвтореToolStripMenuItem.Size = new Size(97, 24);
            обАвтореToolStripMenuItem.Text = "Об Авторе";
            обАвтореToolStripMenuItem.Click += обАвтореToolStripMenuItem_Click;
            // 
            // манипуляцииСТестомToolStripMenuItem
            // 
            манипуляцииСТестомToolStripMenuItem.BackColor = SystemColors.Desktop;
            манипуляцииСТестомToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { редактированиеToolStripMenuItem, удалениеТестаToolStripMenuItem });
            манипуляцииСТестомToolStripMenuItem.Font = new Font("Trebuchet MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            манипуляцииСТестомToolStripMenuItem.ForeColor = Color.Yellow;
            манипуляцииСТестомToolStripMenuItem.Name = "манипуляцииСТестомToolStripMenuItem";
            манипуляцииСТестомToolStripMenuItem.Size = new Size(319, 24);
            манипуляцииСТестомToolStripMenuItem.Text = "Манипуляции с созданными Вами тестами";
            манипуляцииСТестомToolStripMenuItem.Visible = false;
            // 
            // редактированиеToolStripMenuItem
            // 
            редактированиеToolStripMenuItem.Name = "редактированиеToolStripMenuItem";
            редактированиеToolStripMenuItem.Size = new Size(248, 26);
            редактированиеToolStripMenuItem.Text = "Редактирование теста";
            редактированиеToolStripMenuItem.Click += редактированиеToolStripMenuItem_Click;
            // 
            // удалениеТестаToolStripMenuItem
            // 
            удалениеТестаToolStripMenuItem.Name = "удалениеТестаToolStripMenuItem";
            удалениеТестаToolStripMenuItem.Size = new Size(248, 26);
            удалениеТестаToolStripMenuItem.Text = "Удаление теста";
            удалениеТестаToolStripMenuItem.Click += удалениеТестаToolStripMenuItem_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.passTest;
            pictureBox2.Location = new Point(761, 307);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(43, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.createTest;
            pictureBox3.Location = new Point(762, 377);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(43, 50);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1059, 450);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBoxDataGrid);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Главное меню";
            FormClosed += MainMenu_FormClosed;
            Load += MainMenu_Load_1;
            Shown += MainMenu_Shown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDataGrid).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private DataGridView dataGridView1;
        private PictureBox pictureBoxDataGrid;
        private NotifyIcon notifyIcon1;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem статистикаToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem пользователяToolStripMenuItem;
        private ToolStripMenuItem тестаToolStripMenuItem;
        private ToolStripMenuItem выбранныйТестToolStripMenuItem;
        private ToolStripMenuItem всеТестыToolStripMenuItem;
        private ToolStripMenuItem обАвтореToolStripMenuItem;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private ToolStripMenuItem манипуляцииСТестомToolStripMenuItem;
        private ToolStripMenuItem редактированиеToolStripMenuItem;
        private ToolStripMenuItem удалениеТестаToolStripMenuItem;
    }
}
