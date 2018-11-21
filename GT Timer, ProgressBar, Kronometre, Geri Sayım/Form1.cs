using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sayım_Uygulaması_Counting_Application
{
    public partial class Form1 : Form
    {
        // Değişkeni seçip 2 kez CTRL+R yaparak yeni değişken adı atayıp programdaki her yerde değişmesini sağlayabilirsiniz.
        // For the change variable names, you can select a variable and press CTRL+R 2 times. Then write your own variable name.

        // Değişken Tanımlamaları // Variables
        // Kronometre   //Stopwatch
        int k_salise = 1;   // splitsecond
        int k_saniye = 0;   // second
        int k_dakika = 0;   // minute
        int k_saat = 0;     // hour
        int k_tursayisi = 1;    // lap

        // Geri Sayım   //Countdown
        int gs_saniye = 0;      // Saniye   // Second
        int gs_dakika = 0;      // Dakika   // Minute

        // İşlem Çubuğu -diyelim- //ProgressBar
        int ilerleme_toplam;            // ProgressBar Toplam (Maximum)
        int ilerleme_artma_miktari;     // ProgressBar için artma miktarı (Increase Value)

        string lang = "Türkçe";     // Dil Belirleme   // Choosing Language

        public Form1()
        {
            InitializeComponent();

            // Başlangıç Ayarlamaları // Settings for Start
            maskedTextBox1_saniye.Text = "0";   // Geri Sayım saniye    // Countdown second
            maskedTextBox2_dakika.Text = "0";   // Geri Sayım dakika    // Countdown minute
            this.Text = "Sayım Uygulaması";
            this.AcceptButton = button3;    // TextBox'ta enter basılırsa // When pressed to enter in textBox
            this.MaximizeBox = false;
            button1.Enabled = false;    // Tur başta aktif olmasın // Lap disabled for start
        }

        //Kronometre - Başlat   // Stopwatch - Start
        private void btn1_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;

            // Kronometre Başlat Tuşu Yazı Değişimi 
            if (btn1.Text == "&Başlat")     // Başlat Butou yazı değişimi //Change Start Button Text
            {
                btn1.Text = "&Durdur";      // & ile kısayol tuşunu D olarak atamış oluyorz.
            }                               // Using & for shortcut(D).
            else if (btn1.Text == "&Durdur")
            {
                btn1.Text = "&Başlat";
            }

            if (btn1.Text == "&Start")     // Başlat Butou yazı değişimi //Change Start Button Text
            {
                btn1.Text = "&Stop";      // & ile kısayol tuşunu D olarak atamış oluyorz.
            }                               // Using & for shortcut(D).
            else if (btn1.Text == "&Stop")
            {
                btn1.Text = "&Start";
            }

            timer1.Interval = 10;   // timer artma hızı 

            // Eğer Timer1 Açıksa Durdur, değilse Başlat
            if (timer1.Enabled == true)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        // Kronometre Timer
        // Kronometre Salise, Saniye, Dakika, Saat Döngüsü
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = k_salise.ToString();

            if (k_salise == 59)
            {
                k_saniye++;
                k_salise = 0;
                label2.Text = k_saniye.ToString();
            }
            if (k_saniye == 59)
            {
                k_dakika += 1;
                k_saniye = 0;
                label1.Text = k_dakika.ToString();
            }
            if (k_dakika == 60)
            {
                k_saat += 1;
                k_dakika = 0;
                label12.Text = k_saat.ToString();
            }

            k_salise++;
        }

        // Kronometre - Tur Yazdırma    // Stopwatch Lap
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(k_tursayisi + ". " + label12.Text + ":" + label1.Text + ":" + label2.Text + ":" + label3.Text);
            k_tursayisi++;
        }

        //Kronometre - Sıfırla  // Stopwatch - Reset
        private void button2_Click(object sender, EventArgs e)
        {
            if (lang == "Türkçe")
            {
                btn1.Text = "&Başlat";
            }
            else
            {
                btn1.Text = "&Start";
            }
                

            k_tursayisi = 1;
            timer1.Stop();
            label1.Text = "00";
            label2.Text = "00";
            label3.Text = "00";
            listBox1.Items.Clear();
            button1.Enabled = false;
        }

        // Geri Sayım - Başlat  // Countdown - Start
        private void button3_Click(object sender, EventArgs e)
        {
            //Geri Sayım - Giriş Boş İse

            if (maskedTextBox1_saniye.Text == "")
            {
                maskedTextBox1_saniye.Text = "0";
            }

            if (maskedTextBox2_dakika.Text == "")
            {
                maskedTextBox2_dakika.Text = "0";
            }

            //___
            button4.Enabled = true;
            button3.Enabled = false;

            gs_saniye = int.Parse(maskedTextBox1_saniye.Text);
            gs_dakika = int.Parse(maskedTextBox2_dakika.Text);

            timer2.Interval = 1000;

            // Girilen saniye 60'tan büyükse
            if (gs_saniye > 60)
            {
                gs_dakika += gs_saniye / 60;
                gs_saniye = gs_saniye % 60;
            }

            // Girilen değerler 0 ise
            if (gs_saniye == 0 && gs_dakika == 0)
            {
                MessageBox.Show("Geçerli bir süre giriniz.");
                timer2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = false;
            }

            // Geri Sayım Değerler 0 Değilse
            else
            {
                timer2.Enabled = true;
                timer2.Start();
            }

            progressBar1.Enabled = true;
            progressBar1.Value = 0;

            if (gs_saniye == 0 && gs_dakika == 0)
            {
                progressBar1.Enabled = false;
            }
            else
            {
                ilerleme_toplam = ((gs_dakika * 60) + gs_saniye) * 10;
                ilerleme_artma_miktari = ilerleme_toplam / (gs_saniye + gs_dakika * 60);
                progressBar1.Maximum = ilerleme_toplam; // Neden, nasıl işledi bilinmez. *Üzerine düşün
            }
        }

        // Geri Sayım - İptal   // Countdown - Cancel
        private void button4_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            maskedTextBox1_saniye.Text = "0";
            maskedTextBox2_dakika.Text = "0";
            gs_dakika = 0;
            gs_saniye = 0;
            progressBar1.Value = 0;
            button3.Enabled = true;
        }

        // Geri Sayım - Timer   // Countdown - Timer
        private void timer2_Tick(object sender, EventArgs e)
        {
            // Geri Sayım saniye 0 ve dakika bitmemiş ise
            if (gs_saniye == 0 & gs_dakika > 0)
            {
                gs_saniye = 60;
                gs_dakika--;
            }

            // ProgressBar Dolunca  // When Filled the ProgressBar 
            if (progressBar1.Value == progressBar1.Maximum || gs_dakika == 0 && gs_saniye == 0)
            {
                progressBar1.Enabled = false;
            }
            else
                progressBar1.Value += ilerleme_artma_miktari;
            // Geri Sayım - Bitiş     // Countdown - Finish
            if (gs_dakika == 0 & gs_saniye == 0)
            {
                this.TopMost = true;
                timer2.Stop();
                MessageBox.Show("Geri Sayım Sonlandı !", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gs_saniye = 1;
                this.TopMost = false;
                button3.Enabled = true;
                progressBar1.Value = 0;
            }
            gs_saniye--;
            maskedTextBox1_saniye.Text = gs_saniye.ToString();
            maskedTextBox2_dakika.Text = gs_dakika.ToString();
        }

        // Geri Sayım - Saniye Kutusuna Giriş Yapıldığında  // Entering the second Text
        private void maskedTextBox1_saniye_Enter(object sender, EventArgs e)
        {
            maskedTextBox1_saniye.SelectAll();
        }

        // Geri Sayım - Dakika Kutusuna Giriş Yapıldığında  // Entering the minute text
        private void maskedTextBox2_dakika_Enter(object sender, EventArgs e)
        {
            maskedTextBox2_dakika.SelectAll();
        }

        // maskedTextBox1 ve 2 için sadece sayı girişi  // Using only Digits for maskedTextBox1,2
        private void maskedTextBox1_saniye_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))   // olmuyor
            //{
            //    e.Handled = true;
            //}

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        // Kapatma Butonu   // Exit Button
        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lang == "Türkçe")
            {
                MessageBox.Show("Ertuğrul Baran tarafından hazırlandı.\nİletişim: ert.baran@gmail.com", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Created by Ertuğrul Baran.\nContact: ert.baran@gmail.com", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        // Simge Durumuna Al Butonu  // Minimized Button
        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Türkçe Seçilince     // When clicked Turkish Button
        private void button7_turkish_Click(object sender, EventArgs e)
        {
            if (btn1.Text== "&Stop")
            {
                btn1.Text = "&Durdur";
            }

            if (btn1.Text == "&Start")
            {
                btn1.Text = "&Başlat";
            }

            label11.Text = "Kronometre";
            label13.Text = "sa";
            label5.Text = "dk";
            label6.Text = "sn";
            label7.Text = "ss";
            button1.Text = "&Tur";
            button2.Text = "&Sıfırla";
            label14.Text = "Tur:";
            linkLabel1.Text = "Bilgi";
            label4.Text = "Geri Sayım";
            label8.Text = "Dakika";
            label9.Text = "Saniye";
            button3.Text = "Başlat";
            button4.Text = "&İptal";
            lang = "Türkçe";
            this.Text = "Sayım Uygulaması";
        }

        // İngilizce Seçilince  // When clicked English Button
        private void button8_english_Click(object sender, EventArgs e)
        {
            if (btn1.Text == "&Durdur")
            {
                btn1.Text = "&Stop";
            }

            if (btn1.Text == "&Başlat")
            {
                btn1.Text = "&Start";
            }

            label11.Text = "Stopwatch";
            label13.Text = "h";
            label5.Text = "min";
            label6.Text = "sec";
            label7.Text = "ss";
            button1.Text = "&Lap";
            button2.Text = "&Reset";
            label14.Text = "Lap:";
            linkLabel1.Text = "&İnfo";
            label4.Text = "Countdown";
            label8.Text = "Minute";
            label9.Text = "Second";
            button3.Text = "Start";
            button4.Text = "&Cancel";
            lang = "English";
            this.Text = "Counting Application";
        }
    }
}
// ******** ert.baran@gmail.com ********