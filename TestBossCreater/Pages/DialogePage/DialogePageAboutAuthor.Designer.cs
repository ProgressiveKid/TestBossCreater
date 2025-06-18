namespace TestBossCreater.Pages.DialogePage
{
    partial class DialogePageAboutAuthor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogePageAboutAuthor));
            pictureBox1 = new PictureBox();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(47, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(326, 344);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(408, 39);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(363, 333);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(314, 399);
            button1.Name = "button1";
            button1.Size = new Size(150, 29);
            button1.TabIndex = 2;
            button1.Text = "Закрыть форму";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DialogePageAboutAuthor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DialogePageAboutAuthor";
            Text = "DialogePageAboutAuthor";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;
        private Button button1;
    }
}