using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Cesibet.Helpers
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var t = Context.Host;
            var listIDs = Sessions.IDs.ToList<string>();
            var ID = listIDs[listIDs.Count - 1];

            if (e.Data.Contains("username:"))
            {
                var tempSplit = e.Data.Split(":");
                CreateServer.UsersID.Add(tempSplit[1], ID);
            }

            Debug.WriteLine("Received message from Echo client: " + e.Data);
            Send("Added:Done");

            if (CreateServer.UsersID.Count == CreateServer.NoPlayers)
            {
                Sessions.Broadcast("");
            }//
        }

    }

    class CreateServer
    {
        private static WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7898");
        public static int NoPlayers = 0;
        public static int NoRounds = 0;
        public static int curNoRounds = 0;

        public static Dictionary<string, string> UsersID = new Dictionary<string, string>();

        public CreateServer()
        {
        }

        public void beginProcess(int noPlayers, int noRounds)
        {
            curNoRounds = 0;
            NoPlayers = noPlayers;
            NoRounds = noRounds;
            UsersID.Clear();
            wssv?.Stop();
            wssv.AddWebSocketService<Echo>("/Echo");
            wssv.Start();
            Debug.WriteLine("WS server started on ws://127.0.0.1:7890/Echo");
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(5000);
            //        wssv.WebSocketServices.Broadcast("from me to you");
            //    }
            //});
        }

    }
}
