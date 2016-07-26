using System;

namespace IsMaisuDead {
    class Elapsed {
        public static DateTime Startup;
        public static DateTime Shutdown;

        // 経過時間を取得
        public static string GetElapsed() {
            var elapsed = Shutdown - Startup;
            var days = elapsed.ToString(@"d\日\と");
            var hours = elapsed.ToString(@"h\時\間");
            var minutes = elapsed.ToString(@"m\分");
            var seconds = elapsed.ToString(@"s\秒");
            if (days == "0日と") {
                days = "";
            }
            if (hours == "0時間") {
                hours = "";
            }
            if (minutes == "0分") {
                minutes = "";
            }
            if (seconds == "0秒") {
                seconds = "";
            }
            var res = days + hours + minutes + seconds;
            return res;
        }
    }
}
