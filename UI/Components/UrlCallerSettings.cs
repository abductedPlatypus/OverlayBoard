using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI.Components;
using LiveSplit.UI;

namespace TwopIt.LiveSplit.UI.Components
{
    public partial class UrlCallerSettings : UserControl
    {
        public string Split { get; set; }
        public string SplitAheadGaining { get; set; }
        public string SplitAheadLosing { get; set; }
        public string SplitBehindGaining { get; set; }
        public string SplitBehindLosing { get; set; }
        public string BestSegment { get; set; }
        public string UndoSplit { get; set; }
        public string SkipSplit { get; set; }
        public string PersonalBest { get; set; }
        public string NotAPersonalBest { get; set; }
        public string Reset { get; set; }
        public string Pause { get; set; }
        public string Resume { get; set; }
        public string StartTimer { get; set; }

        public string TwopBoardCode { get; set; }
        public string TwitchUniqueName { get; set; }
        public string ErrorLogs { get; set; }

        public UrlCallerSettings()
        {
            InitializeComponent();

            Split =
            SplitAheadGaining =
            SplitAheadLosing =
            SplitBehindGaining =
            SplitBehindLosing =
            BestSegment =
            UndoSplit =
            SkipSplit =
            PersonalBest =
            NotAPersonalBest =
            Reset =
            Pause =
            Resume =
            StartTimer = "";

            txtSplitPath.DataBindings.Add("Text", this, "Split");
            txtSplitAheadGaining.DataBindings.Add("Text", this, "SplitAheadGaining");
            txtSplitAheadLosing.DataBindings.Add("Text", this, "SplitAheadLosing");
            txtSplitBehindGaining.DataBindings.Add("Text", this, "SplitBehindGaining");
            txtSplitBehindLosing.DataBindings.Add("Text", this, "SplitBehindLosing");
            txtBestSegment.DataBindings.Add("Text", this, "BestSegment");
            txtUndo.DataBindings.Add("Text", this, "UndoSplit");
            txtSkip.DataBindings.Add("Text", this, "SkipSplit");
            txtPersonalBest.DataBindings.Add("Text", this, "PersonalBest");
            txtNotAPersonalBest.DataBindings.Add("Text", this, "NotAPersonalBest");
            txtReset.DataBindings.Add("Text", this, "Reset");
            txtPause.DataBindings.Add("Text", this, "Pause");
            txtResume.DataBindings.Add("Text", this, "Resume");
            txtStartTimer.DataBindings.Add("Text", this, "StartTimer");
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            Split = SettingsHelper.ParseString(element["Split"]);
            SplitAheadGaining = SettingsHelper.ParseString(element["SplitAheadGaining"]);
            SplitAheadLosing = SettingsHelper.ParseString(element["SplitAheadLosing"]);
            SplitBehindGaining = SettingsHelper.ParseString(element["SplitBehindGaining"]);
            SplitBehindLosing = SettingsHelper.ParseString(element["SplitBehindLosing"]);
            BestSegment = SettingsHelper.ParseString(element["BestSegment"]);
            UndoSplit = SettingsHelper.ParseString(element["UndoSplit"]);
            SkipSplit = SettingsHelper.ParseString(element["SkipSplit"]);
            PersonalBest = SettingsHelper.ParseString(element["PersonalBest"]);
            NotAPersonalBest = SettingsHelper.ParseString(element["NotAPersonalBest"]);
            Reset = SettingsHelper.ParseString(element["Reset"]);
            Pause = SettingsHelper.ParseString(element["Pause"]);
            Resume = SettingsHelper.ParseString(element["Resume"]);
            StartTimer = SettingsHelper.ParseString(element["StartTimer"]);

            TwitchUniqueName = SettingsHelper.ParseString(element["TwitchUniqueName"]);
            TwopBoardCode = SettingsHelper.ParseString(element["TwopBoardCode"]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "0.1") ^
            SettingsHelper.CreateSetting(document, parent, "Split", Split) ^
            SettingsHelper.CreateSetting(document, parent, "SplitAheadGaining", SplitAheadGaining) ^
            SettingsHelper.CreateSetting(document, parent, "SplitAheadLosing", SplitAheadLosing) ^
            SettingsHelper.CreateSetting(document, parent, "SplitBehindGaining", SplitBehindGaining) ^
            SettingsHelper.CreateSetting(document, parent, "SplitBehindLosing", SplitBehindLosing) ^
            SettingsHelper.CreateSetting(document, parent, "BestSegment", BestSegment) ^
            SettingsHelper.CreateSetting(document, parent, "UndoSplit", UndoSplit) ^
            SettingsHelper.CreateSetting(document, parent, "SkipSplit", SkipSplit) ^
            SettingsHelper.CreateSetting(document, parent, "PersonalBest", PersonalBest) ^
            SettingsHelper.CreateSetting(document, parent, "NotAPersonalBest", NotAPersonalBest) ^
            SettingsHelper.CreateSetting(document, parent, "Reset", Reset) ^
            SettingsHelper.CreateSetting(document, parent, "Pause", Pause) ^
            SettingsHelper.CreateSetting(document, parent, "Resume", Resume) ^
            SettingsHelper.CreateSetting(document, parent, "StartTimer", StartTimer) ^
            SettingsHelper.CreateSetting(document, parent, "TwitchUniqueName", TwitchUniqueName) ^
            SettingsHelper.CreateSetting(document, parent, "TwopBoardCode", TwopBoardCode);
        }

        private void ButtonBoard_Click(object sender, EventArgs e)
        {
            var f = new BoardCodeModal();
            f.TextBox.Text = TwopBoardCode;
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.TwopBoardCode = f.TextBox.Text;
            }
        }

        private void ButtonTwitch_Click(object sender, EventArgs e)
        {
            var f = new TwitchNameModal();
            f.TextBox.Text = TwitchUniqueName;
            var res = f.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                this.TwitchUniqueName = f.TextBox.Text;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ErrorLogs = "";
            log.Text = "";
            TestUrl(Split, "Split");
            TestUrl(SplitAheadGaining, "Split Ahead Gaining");
            TestUrl(SplitAheadLosing, "Split Ahead Losing");
            TestUrl(SplitBehindGaining, "Split Behind Gaining");
            TestUrl(SplitBehindLosing, "Split Behind Losing");
            TestUrl(BestSegment, "Best Segment");
            TestUrl(UndoSplit, "Undo Split");
            TestUrl(PersonalBest, "Personal Best");
            TestUrl(NotAPersonalBest, "Not A Personal Best");
            TestUrl(Reset, "Reset");
            TestUrl(Pause, "Pause");
            TestUrl(Resume, "Resume");
        }

        private async void TestUrl(string url, string name)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    ErrorLogs += Environment.NewLine;
                    ErrorLogs += "--------------------------------" + Environment.NewLine;
                    ErrorLogs += "Invalid URL (" + name + "):" + url + Environment.NewLine;
                    ErrorLogs += "An url should be written in full, e.g. 'http(s)://www.twop.it/mrkappa/abcdefgh234567890" + Environment.NewLine;
                    ErrorLogs += "--------------------------------" + Environment.NewLine;
                }
                else
                {

                    await System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            var webClient = new WebClient();

                            if (webClient.DownloadString(new Uri(url)) != null)
                            {
                                ErrorLogs += Environment.NewLine;
                                ErrorLogs += "--------------------------------" + Environment.NewLine;
                                ErrorLogs += "SUCCESS (" + name + "):" + url + Environment.NewLine;
                                ErrorLogs += "--------------------------------" + Environment.NewLine;

                            }
                        }
                        catch (Exception e)
                        {
                            ErrorLogs += Environment.NewLine;
                            ErrorLogs += "--------------------------------" + Environment.NewLine;
                            ErrorLogs += "ERROR (" + name + "):" + url + Environment.NewLine;
                            ErrorLogs += e.Message + Environment.NewLine;
                            ErrorLogs += "--------------------------------" + Environment.NewLine;
                        }
                    });
                }

                log.Text = ErrorLogs;
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://twop.it");
            Process.Start(sInfo);
        }
    }
}
