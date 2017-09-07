using LiveSplit.Model;
using LiveSplit.Options;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI.Components;
using LiveSplit.UI;

namespace TwopIt.LiveSplit.UI.Components
{
    public class UrlCallerComponent : LogicComponent, IDeactivatableComponent
    {
        public override string ComponentName => "URL Caller (TWOP.IT)";

        public bool Activated { get; set; }

        private LiveSplitState State { get; set; }
        private UrlCallerSettings Settings { get; set; }
        private WebClient webClient = new WebClient();

        public UrlCallerComponent(LiveSplitState state)
        {
            Activated = true;
            State = state;
            Settings = new UrlCallerSettings();

            State.OnStart += State_OnStart;
            State.OnSplit += State_OnSplit;
            State.OnSkipSplit += State_OnSkipSplit;
            State.OnUndoSplit += State_OnUndoSplit;
            State.OnPause += State_OnPause;
            State.OnResume += State_OnResume;
            State.OnReset += State_OnReset;
        }

        public override void Dispose()
        {
            State.OnStart -= State_OnStart;
            State.OnSplit -= State_OnSplit;
            State.OnSkipSplit -= State_OnSkipSplit;
            State.OnUndoSplit -= State_OnUndoSplit;
            State.OnPause -= State_OnPause;
            State.OnResume -= State_OnResume;
            State.OnReset -= State_OnReset;

            webClient.Dispose();
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public override void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        private void State_OnStart(object sender, EventArgs e)
        {
            CallUrl(Settings.StartTimer);
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended)
            {
                if (State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod] == null || State.Run.Last().SplitTime[State.CurrentTimingMethod] < State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod])
                    CallUrl(Settings.PersonalBest);
                else
                    CallUrl(Settings.NotAPersonalBest);
            }
            else
            {
                var path = string.Empty;

                var splitIndex = State.CurrentSplitIndex - 1;
                var timeDifference = State.Run[splitIndex].SplitTime[State.CurrentTimingMethod] - State.Run[splitIndex].Comparisons[State.CurrentComparison][State.CurrentTimingMethod];

                if (timeDifference != null)
                {
                    if (timeDifference < TimeSpan.Zero)
                    {
                        path = Settings.SplitAheadGaining;

                        if (LiveSplitStateHelper.GetPreviousSegmentDelta(State, splitIndex, State.CurrentComparison, State.CurrentTimingMethod) > TimeSpan.Zero)
                        {
                            path = Settings.SplitAheadLosing;
                        }
                    }
                    else
                    {
                        path = Settings.SplitBehindLosing;

                        if (LiveSplitStateHelper.GetPreviousSegmentDelta(State, splitIndex, State.CurrentComparison, State.CurrentTimingMethod) < TimeSpan.Zero)
                        {
                            path = Settings.SplitBehindGaining;
                        }
                    }
                }

                //Check for best segment
                TimeSpan? curSegment = LiveSplitStateHelper.GetPreviousSegmentTime(State, splitIndex, State.CurrentTimingMethod);

                if (curSegment != null)
                {
                    if (State.Run[splitIndex].BestSegmentTime[State.CurrentTimingMethod] == null || curSegment < State.Run[splitIndex].BestSegmentTime[State.CurrentTimingMethod])
                    {
                        path = Settings.BestSegment;
                    }
                }

                if (string.IsNullOrEmpty(path))
                    path = Settings.Split;

                CallUrl(path);
            }
        }

        private void State_OnSkipSplit(object sender, EventArgs e)
        {
            CallUrl(Settings.SkipSplit);
        }

        private void State_OnUndoSplit(object sender, EventArgs e)
        {
            CallUrl(Settings.UndoSplit);
        }

        private void State_OnPause(object sender, EventArgs e)
        {
            CallUrl(Settings.Pause);
        }

        private void State_OnResume(object sender, EventArgs e)
        {
            CallUrl(Settings.Resume);
        }

        private void State_OnReset(object sender, TimerPhase e)
        {
            if (e != TimerPhase.Ended)
                CallUrl(Settings.Reset);
        }

        private void CallUrl(string url)
        {

            if (Activated)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (webClient.IsBusy)
                        { // if a control is spammed we need a temporary one instead
                            new WebClient().DownloadString(new Uri(url));
                        }
                        else
                        {
                            webClient.DownloadString(new Uri(url));
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e); 
                    }
                });
            }
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}
