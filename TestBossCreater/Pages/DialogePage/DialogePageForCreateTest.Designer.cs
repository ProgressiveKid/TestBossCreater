namespace TestBossCreater.Pages.DialogePage
{
    partial class DialogePageForCreateTest
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
            richTextBox1 = new RichTextBox();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            richTextBox2 = new RichTextBox();
            label3 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(196, 41);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(418, 29);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(285, 310);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 27);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 106);
            label1.Name = "label1";
            label1.Size = new Size(118, 20);
            label1.TabIndex = 2;
            label1.Text = "Описание теста";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 41);
            label2.Name = "label2";
            label2.Size = new Size(116, 20);
            label2.TabIndex = 3;
            label2.Text = "Название теста";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(196, 106);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(418, 172);
            richTextBox2.TabIndex = 4;
            richTextBox2.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 310);
            label3.Name = "label3";
            label3.Size = new Size(206, 40);
            label3.TabIndex = 5;
            label3.Text = "Кол-во правильных ответов\r\nдля сдачи теста";
            // 
            // button1
            // 
            button1.Location = new Point(229, 387);
            button1.Name = "button1";
            button1.Size = new Size(200, 44);
            button1.TabIndex = 6;
            button1.Text = "Сохранить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DialogePageForCreateTest
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(688, 455);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(richTextBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(richTextBox1);
            Name = "DialogePageForCreateTest";
            Text = "Основная информация о тесте";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private RichTextBox richTextBox2;
        private Label label3;
        private Button button1;
    }
}