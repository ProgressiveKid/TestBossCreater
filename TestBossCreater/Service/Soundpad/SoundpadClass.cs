using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace TestBossCreater.Service.Soundpad
{
    public class SoundpadClass
    {
        private IWavePlayer? waveOut;
        private AudioFileReader? audioFileReader;

        public void PlayMp3(string filePath)
        {
            try
            {
                // Остановим старое, если было
                StopMp3();

                audioFileReader = new AudioFileReader(filePath);
                waveOut = new WaveOutEvent();
                waveOut.Init(audioFileReader);
                waveOut.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при воспроизведении: {ex.Message}");
            }
        }

        private void StopMp3()
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            audioFileReader?.Dispose();
        }
    }
}
