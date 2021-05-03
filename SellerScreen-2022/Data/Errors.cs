using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SellerScreen_2022.Data
{
    public class Errors
    {
        public Errors(string page, string type, string msg, string source, string targetsite, string stacktrace)
        {
            Errors error = new Errors()
            {
                Time = DateTime.Now,
                Page = page,
                Type = type,
                Msg = msg,
                Source = source,
                TargetSite = targetsite,
                StackTrace = stacktrace
            };

            error.Save();
        }

        public Errors() { }

        public string TimeToString => _Time.ToShortDateString() + ", " + _Time.ToLongTimeString();

        private DateTime _Time;
        public DateTime Time
        {
            get => _Time;
            set => _Time = value;
        }

        private string _Page;
        public string Page
        {
            get => _Page;
            set => _Page = value;
        }

        private string _Type;
        public string Type
        {
            get => _Type;
            set => _Type = value;
        }

        private string _Msg;
        public string Msg
        {
            get => _Msg;
            set => _Msg = value;
        }

        private string _Source;
        public string Source
        {
            get => _Source;
            set => _Source = value;
        }

        private string _TargetSite;
        public string TargetSite
        {
            get => _TargetSite;
            set => _TargetSite = value;
        }

        private string _StackTrace;
        public string StackTrace
        {
            get => _StackTrace;
            set => _StackTrace = value;
        }

        public Task Save()
        {
            string filename = Paths.tempPath + $"{DateTime.Now.Ticks}.xml";
            using FileStream stream = new FileStream(filename, FileMode.Create);
            XmlSerializer XML = new XmlSerializer(typeof(Errors));
            XML.Serialize(stream, this);
            return Task.CompletedTask;
        }

        public static Task<Errors> Load(string filename)
        {
            using FileStream stream = new FileStream(filename, FileMode.Open);
            XmlSerializer XML = new XmlSerializer(typeof(Errors));
            return Task.FromResult((Errors)XML.Deserialize(stream));
        }

        public static async Task ShowErrorMsg(Exception ex, string page, bool alsoSave = false)
        {
            Errors error = new Errors
            {
                Msg = ex.Message,
                Page = page,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                TargetSite = ex.TargetSite.ToString(),
                Time = DateTime.Now,
                Type = ex.GetType().ToString()
            };

            if (alsoSave)
            {
                await error.Save().ConfigureAwait(false);
            }

            TaskDialog d = ErrorDialogs.ShowErrorDialog;
            d.WindowTitle += error.Page;
            d.MainInstruction += error.Type;
            d.Content = error.Msg;
            d.ExpandedInformation = error.Source + "\n\n" + error.TargetSite + "\n\n" + error.StackTrace;
            d.Tag = error.Time.Ticks;
            d.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
            d.ButtonClicked += new EventHandler<TaskDialogItemClickedEventArgs>(TaskDialogClose);
            d.ShowDialog();
        }

        private static async void TaskDialogClose(object sender, TaskDialogItemClickedEventArgs e)
        {
            if (sender is TaskDialog d)
            {
                if (d.IsVerificationChecked)
                {
                    await SendError(d.Tag).ConfigureAwait(false);
                }
            }
        }

        private static Task SendError(object obj)
        {
            ulong ticks = ulong.Parse(obj.ToString());

            ProgressDialog p = new ProgressDialog
            {
                ProgressBarStyle = ProgressBarStyle.MarqueeProgressBar,
                ShowTimeRemaining = true,
                ShowCancelButton = false,
                Text = "Fehlerbericht wird gesendet...",
                WindowTitle = "SellerScreen-2022",
            };
            p.DoWork += new DoWorkEventHandler(SendErrorWorker);
            p.Show();

            return Task.CompletedTask;
        }

        private static void SendErrorWorker(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(10000);
        }
    }

    public class ErrorList
    {
        public static List<Errors> errors = new List<Errors>();

        public static async Task Load()
        {
            List<string> files = Directory.GetFiles(Paths.tempPath, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            errors.Clear();
            foreach (string file in files)
            {
                Errors error = await Errors.Load(file);
                errors.Add(error);
            }
        }
    }

    public class ErrorDialogs
    {
        public static TaskDialog ShowErrorDialog = new TaskDialog
        {
            WindowTitle = "SellerScreen-2022: ",
            MainInstruction = "Error: ",
            ExpandedControlText = "Show details",
            IsVerificationChecked = true,
            VerificationText = "Send error report to T-App Germany",
            Footer = "Error reports do not contain any personal data. It is only analysis data that will help to avoid this error in the future.",
            Width = 380,
            MainIcon = TaskDialogIcon.Error,
            FooterIcon = TaskDialogIcon.Information,
            ExpandFooterArea = true,
        };
    }
}
