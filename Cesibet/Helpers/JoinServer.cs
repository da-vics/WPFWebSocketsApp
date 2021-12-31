using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WebSocketSharp;

namespace Cesibet.Helpers
{
    class JoinServer
    {
        public JoinServer()
        {

        }

        public void connetToServer(string url, string userName)
        {
            Task.Run(() =>
            {
                WebSocket ws = new WebSocket(url);
                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
                ws.Send($"username:{userName}");
                Thread.Sleep(1000);
            });
        }//


        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.WriteLine(e.Data);

            if (e.Data.Contains("Added"))
                MessageBox.Show("connected successfully");
        }

    }//
}
