using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using TwopIt.LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(UrlCallerFactory))]

namespace TwopIt.LiveSplit.UI.Components
{
    public class UrlCallerFactory : IComponentFactory
    {
        public string ComponentName => "URL Caller (TWOP.IT)";

        public string Description => "Call an URL, such as a stream overlay (e.g. www.twop.it), on your favourite situations.";

        public ComponentCategory Category => ComponentCategory.Media;

        public IComponent Create(LiveSplitState state) => new UrlCallerComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "http://plugins.twop.it/LiveSplit/update.LiveSplit.UrlCaller.xml";

        public string UpdateURL => "https://github.com/abductedPlatypus/OverlayBoard/releases/";

        public Version Version => Version.Parse("0.0.1");
    }
}
