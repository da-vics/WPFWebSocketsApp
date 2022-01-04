using Cesibet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using Cesibet.ViewModels;

namespace Cesibet.Helpers
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var Data = e.Data;

            if (Data.Contains("username:"))
            {
                var tempSplit = Data.Split(":");
                CreateServer.UsersID.Add(ID, new UserModel { UserName = tempSplit[1], Points = 0 });
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
                CreateServer.CheckQuestion();
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
        public static Action<string, bool> realeaseBtnQuestions;
        public static Dictionary<string, UserModel> UsersID = new Dictionary<string, UserModel>();
        public static Dictionary<string, Answers> UsersAnswer = new Dictionary<string, Answers>();
        public static bool answered = false;

        public CreateServer()
        {
        }

        public static void CheckQuestion()
        {
            if (UsersAnswer.Count - 1 == NoPlayers)
            {
                if (curNoRounds < NoRounds)
                {
                    ++curNoRounds;
                    var keys = UsersAnswer.Keys;
                    int count = 0;
                    foreach (var key in keys)
                    {
                        var model = UsersAnswer[key];
                        if (model.QuestionAnswer == "Yes")
                        {
                            ++count;
                        }
                    }


                    foreach (var key in keys)
                    {
                        if (UsersAnswer[key].Estimate == count)
                        {
                            UsersID[key].Points += 2;
                        }
                    }

                    Sendmessage($"ShowResult:{count}");
                    realeaseBtnQuestions?.Invoke($"Number of Yes: {count}", false);
                    UsersAnswer.Clear();
                }

                else
                {
                    var userModelList = new List<UserModel>();
                    var keys = UsersID.Keys;
                    foreach (var key in keys)
                    {
                        userModelList.Add(new UserModel { UserName = UsersID[key].UserName, Points = UsersID[key].Points });
                    }
                    string json = JsonConvert.SerializeObject(userModelList);
                    ResultsViewModel.JsonString = json;
                    Sendmessage($"Result~{json}");
                    realeaseBtnQuestions?.Invoke("", true);
                }

            }//
        }

        public static void AddAnswer(string answer, int estimate)
        {
            UsersAnswer.Add("NULL", new Answers { QuestionAnswer = answer, Estimate = estimate });
            CheckQuestion();
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
            UsersID.Add("NULL", new UserModel { UserName = "HOST", Points = 0 });

        }

        public static void Sendmessage(string message)
        {
            wssv.WebSocketServices.Broadcast(message);
        }

    }

}
