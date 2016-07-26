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

        //// 名前の語尾にステータスを表示
        //public static void StatusName(String status) {
        //    var delName = "";
        //    var addName = "";
        //    var Online = " (Online)";
        //    var Offline = " (Offline)";
        //    if (status == Online) {
        //        addName = Online;
        //        delName = Offline;
        //    } else {
        //        addName = Offline;
        //        delName = Online;
        //    }
        //    var oldName = tokens.Account.UpdateProfile().Name;
        //    var newName = "";
        //    if (oldName.Contains(addName)) {
        //        return;
        //    } else if (oldName.Contains(delName)) {
        //        newName = oldName.Replace(delName, "");
        //        newName += addName;
        //    } else {
        //        newName = oldName + addName;
        //    }
        //    tokens.Account.UpdateProfile(name: newName);
        //}

        // 名前の語尾にステータスを表示
        public static void StatusName(String status) {
            // 名前の最大文字数は２０文字。
            // " (Online)" が９文字で、" (Offline)" が１０文字なため、
            // それを除いた名前の最大文字数は１０文字。
            const string Online = " (Online)";
            const string Offline = " (Offline)";
            var oldName = tokens.Account.UpdateProfile().Name;
            var newName = "";
            switch (status) {
                case Online:
                    newName = oldName.Replace(Offline, "") + Online;
                    break;
                case Offline:
                    newName = oldName.Replace(Online, "") + Offline;
                    break;
            }
            tokens.Account.UpdateProfile().Name = newName;
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
