using CoreTweet;
using System;
using System.Net;

namespace IsMaisuDead {
    class Twitter {
        public static Tokens tokens;

        // ツイッター認証
        public static void OAuth() {
            tokens = Tokens.Create(Keys.ConsumerKey, Keys.ConsumerSecret, Keys.AccessToken, Keys.AccessTokenSecret);
        }

        // 名前の語尾にステータスを表示
        public static void StatusName(String status) {
            const string Online = " (Online)";
            const string Offline = " (Offline)";
            var oldName = GetName();
            var newName = "";
            switch (status) {
                case Online:
                    newName = oldName.Replace(Offline, "") + Online;
                    break;
                case Offline:
                    newName = oldName.Replace(Online, "") + Offline;
                    break;
            }
            ChangeName(newName);
        }

        // ツイートを送信
        public static void Tweet(String message) {
            while (true) {
                if (message.Length > 140) return;
                try {
                    tokens.Statuses.Update(status: message);
                    break;
                } catch (TwitterException ex) {
                    if (ex.Message == "Status is a duplicate.") return;
                    throw;
                } catch (WebException) {
                }
            }

        }

        // 名前を取得
        private static string GetName() {
            return tokens.Account.UpdateProfile().Name;
        }

        // 名前を変更
        private static void ChangeName(string name) {
            tokens.Account.UpdateProfile().Name = name;
        }

        // 改行コード
        private static string NewLine = Environment.NewLine;

        // スタートアップツイートを送信
        public static void StartupTweet() {
            var msg = "コンピューターがスタートアップされました。" + NewLine + Elapsed.Startup.ToString("F");
            Tweet(msg);
        }

        // シャットダウンツイートを送信
        public static void ShutdownTweet() {
            var msg = "コンピューターがシャットダウンされました。" + NewLine + "経過時間は" + Elapsed.GetElapsed() + "だったと推測されます。" + NewLine + Elapsed.Shutdown.ToString("F");
            Tweet(msg);
        }
    }
}
