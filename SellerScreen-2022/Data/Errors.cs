using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public string TimeToString
        {
            get => _Time.ToShortDateString() + ", " + _Time.ToLongTimeString();
        }

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
            File.Encrypt(filename);
            return Task.CompletedTask;
        }

        public static Task<Errors> Load(string filename)
        {
            File.Decrypt(filename);
            using FileStream stream = new FileStream(filename, FileMode.Open);
            XmlSerializer XML = new XmlSerializer(typeof(Errors));
            File.Encrypt(filename);
            return Task.FromResult((Errors)XML.Deserialize(stream));
        }

        public static async Task ShowErrorMsg(Errors error, bool alsoSave = false)
        {
            if (alsoSave)
            {
                await error.Save().ConfigureAwait(false);
            }

            TaskDialog d = new TaskDialog
            {
                WindowTitle = "SellerScreen-2022: " + error.Page,
                MainInstruction = "Fehler: " + error.Type,
                Content = error.Msg,
                ExpandedControlText = "Details anzeigen",
                IsVerificationChecked = true,
                VerificationText = "Fehlerbericht an Entwickler senden",
                Footer = "Fehlerberichte enthalten keine personenbezogenen Daten. Es sind nur Analysedaten, die Helfen diesen Fehler in Zukunft zu vermeiden.",
                ExpandedInformation = error.Source + "\n\n" + error.TargetSite + "\n\n" + error.StackTrace,
                Width = 380,
                MainIcon = TaskDialogIcon.Error,
                FooterIcon = TaskDialogIcon.Information,
                ExpandFooterArea = true,
                Tag = error.Time.Ticks
            };
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

        public static async Task LoadList()
        {
            List<string> files = Directory.GetFiles(Paths.tempPath, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            foreach (string file in files)
            {
                Errors error = await Errors.Load(file);
                errors.Add(error);
            }
        }
    }
}
