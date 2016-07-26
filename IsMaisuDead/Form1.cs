using System;
using System.Windows.Forms;

namespace IsMaisuDead {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        
        // スタートアップツイートの送信
        private void Form1_Load(object sender, EventArgs e) {
            FormUnvisible();
            Elapsed.Startup = DateTime.Now;
            Twitter.OAuth();
            Twitter.StartupTweet();
            Twitter.StatusName(" (Online)");
        }

        // フォームをタスクトレイに表示
        private void FormUnvisible() {
            notifyIcon1.Visible = true;
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.Visible = false;
        }

        // フォームをタスクトレイから表示
        private void FormVisible() {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            this.ShowIcon = true;
            this.Visible = true;
        }

        // フォームが閉じたらシャットダウンツイートを送信
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            Elapsed.Shutdown = DateTime.Now;
            Twitter.ShutdownTweet();
            Twitter.StatusName(" (Offline)");
        }

        // シャットダウンツイートを送信してアプリケーションを終了
        private void toolStripTextBox1_Click(object sender, EventArgs e) {
            this.Close();
        }

        // シャットダウンツイートを送信しないでアプリケーションを終了
        private void toolStripTextBox2_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        // タスクトレイから表示
        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            FormVisible();
        }

        // 最小化を押したらタスクトレイに表示
        private void Form1_SizeChanged(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                FormUnvisible();
            }
        }

        // スタートアップ時間を偽装
        private void button1_Click(object sender, EventArgs e) {
            var year = Convert.ToInt32(numericUpDown1.Value);
            var month = Convert.ToInt32(numericUpDown2.Value);
            var day = Convert.ToInt32(numericUpDown3.Value);
            var hour = Convert.ToInt32(numericUpDown4.Value);
            var minute = Convert.ToInt32(numericUpDown5.Value);
            var second = Convert.ToInt32(numericUpDown6.Value);
            var fake = new DateTime(year, month, day, hour, minute, second);
            Elapsed.Startup = fake;
        }
    }
}
