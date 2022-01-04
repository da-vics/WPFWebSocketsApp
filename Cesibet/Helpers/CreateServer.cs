using Cesibet.Models;
using System;
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
            var listIDs = Sessions.IDs.ToList<string>();
            var ID = listIDs[listIDs.Count - 1];
            var Data = e.Data;

            if (Data.Contains("username:"))
            {
                var tempSplit = Data.Split(":");
                CreateServer.UsersID.Add(tempSplit[1], new UserModel { ID = ID, Points = 0 });
                Send("Added:Done");

                if (CreateServer.UsersID.Count - 1 == CreateServer.NoPlayers)
                {
                    CreateServer.IsGamePlay = true;
                    CreateServer.curNoRounds++;
                    var randNum = CreateServer.questionIndex();
                    Sessions.Broadcast($"GameOn:{randNum}");
                    CreateServer.realeaseBtn?.Invoke(randNum);
                }//
            }

            else if (Data.Contains("QT"))
            {
                var split = Data.Split(":");
                CreateServer.UsersAnswer.Add(ID, new Answers { QuestionAnswer = split[1], Estimate = Int32.Parse(split[2]) });

                if (CreateServer.UsersAnswer.Count - 1 == CreateServer.NoPlayers)
                {
                    if (CreateServer.curNoRounds < CreateServer.NoRounds)
                    {
                        ++CreateServer.curNoRounds;
                        var keys = CreateServer.UsersAnswer.Keys;

                        int count = 0;
                        foreach (var key in keys)
                        {
                            var model = CreateServer.UsersAnswer[key];
                            if (model.QuestionAnswer == "Yes")
                                ++count;
                        }
                        Sessions.Broadcast($"ShowResult:{count}");
                        CreateServer.realeaseBtnQuestions?.Invoke($"Number of Yes: {count}");
                    }

                    CreateServer.UsersAnswer.Clear();
                }//

            }

        }

    }

    class CreateServer
    {
        public static List<int> questionsDisplayed = new List<int>();
        private static WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7898");
        public static int NoPlayers = 0;
        public static int NoRounds = 0;
        public static int curNoRounds = 0;
        public static bool IsGamePlay = false;
        public static Action<int> realeaseBtn;
        public static Action<string> realeaseBtnQuestions;
        public static Dictionary<string, UserModel> UsersID = new Dictionary<string, UserModel>();
        public static Dictionary<string, Answers> UsersAnswer = new Dictionary<string, Answers>();
        public static bool answered = false;

        public CreateServer()
        {
        }

        public static void AddAnswer(string answer, int estimate)
        {
            UsersAnswer.Add("NULL", new Answers { QuestionAnswer = answer, Estimate = estimate });
        }

        public static int questionIndex()
        {
            var generator = new Random();
            GenerateQuestions generateQuestions = new GenerateQuestions();

            var randomNumber = 0;
            while (true)
            {
                randomNumber = generator.Next(generateQuestions.Questions.Count);

                bool check = false;

                foreach (var num in questionsDisplayed)
                {
                    if (num == randomNumber)
                    {
                        check = true;
                        break;
                    }
                }

                if (questionsDisplayed.Count < 1 || check == false)
                {
                    questionsDisplayed.Add(randomNumber);
                    break;
                }
            }
            return randomNumber;
        }

        public void beginProcess(int noPlayers, int noRounds, Action<int> func)
        {
            answered = false;
            questionsDisplayed.Clear();
            realeaseBtn = null;
            realeaseBtn += func;
            IsGamePlay = false;
            realeaseBtnQuestions = null;
            curNoRounds = 0;
            NoPlayers = noPlayers;
            NoRounds = noRounds;
            UsersID.Clear();
            UsersAnswer.Clear();
            wssv?.Stop();
            wssv.AddWebSocketService<Echo>("/Echo");
            wssv.Start();
            Debug.WriteLine("WS server started on ws://127.0.0.1:7890/Echo");
            UsersID.Add("HOST", new UserModel { ID = "NULL", Points = 0 });

        }

        public static void Sendmessage(string message)
        {
            wssv.WebSocketServices.Broadcast(message);
        }

    }
}
