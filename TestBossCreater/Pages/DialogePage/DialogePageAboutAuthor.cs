using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;

namespace TestBossCreater.Pages.DialogePage
{
    public partial class DialogePageAboutAuthor : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        public DialogePageAboutAuthor()
        {
            InitializeComponent();
            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
        }
        private void StartRadio()
        {
            string url = "https://radiorecord.hostingradio.ru/rr_main96.aacp";

            using (var media = new Media(_libVLC, new Uri(url)))
            {
                _mediaPlayer.Volume = 50; // громкость 50%
                _mediaPlayer.Play(media);
            }
        }

        private void StopRadio()
        {
            _mediaPlayer.Stop();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _mediaPlayer.Stop();
            this.Close();
            this.Dispose();
        }

        private CreditsControl credits;
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void DialogePageAboutAuthor_Load(object sender, EventArgs e)
        {
            credits = new CreditsControl();
            credits.Dock = DockStyle.Fill;
            panel1.Controls.Add(credits);
            credits.Start();
            StartRadio();
        }
    }
}
