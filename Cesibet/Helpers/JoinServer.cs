using Cesibet.ViewModels;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WebSocketSharp;

namespace Cesibet.Helpers
{
    class JoinServer
    {
        public static Action<int> realeaseBtn;
        public static Action<string, bool> realeaseBtnQuestions;

        public static bool Pageup = false;
        public static int CurIndex = 0;

        private static WebSocket ws;

        public JoinServer()
        {

        }

        public static void connetToServer(string url, string userName, Action<int> func)
        {
            realeaseBtn = null;
            realeaseBtn += func;
            realeaseBtnQuestions = null;

            Task.Run(() =>
            {
                ws = new WebSocket(url);
                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
                ws.Send($"username:{userName}");
                Thread.Sleep(1000);
            });
        }//

        public static void sendMessage(string str)
        {
            Task.Run(() => { ws.Send(str); Thread.Sleep(1000); });
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.WriteLine(e.Data);

            var Data = e.Data;

            if (Data.Contains("Added"))
                MessageBox.Show("connected successfully");

            else if (Data.Contains("GameOn"))
            {
                var split = Data.Split(":");
                realeaseBtn?.Invoke(Int32.Parse(split[1]));
            }

            else if (Data.Contains("ShowResult"))
            {
                var spilt = Data.Split(":");
                realeaseBtnQuestions?.Invoke($"Number of Yes: {spilt[1]}", false);
            }

            else if (Data.Contains("Next"))
            {
                Pageup = true;
                var spilt = Data.Split(":");
                CurIndex = Int32.Parse(spilt[1]);
            }

            else if (Data.Contains("Result"))
            {
                var spilt = Data.Split("~");
                ResultsViewModel.JsonString = spilt[1];
                realeaseBtnQuestions?.Invoke("", true);
            }
        }

    }//
}
