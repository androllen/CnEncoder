using Cn.Core.Events;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using System.Text;
using System.Text.RegularExpressions;

namespace Cn.Module.Encode.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        private readonly IEventAggregator _eventAggregator;

        public ViewAViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CmdSentEvent>().Subscribe(CmdReceived);
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
            //Message = "View A from your Prism Module EncodeContentRegion\n";
        }

        private void MessageReceived(EventWithPater obj)
        {
            if (obj.ModuleInfo.ModuleType != typeof(EncodeModule).AssemblyQualifiedName)
            {
                if (string.IsNullOrEmpty(obj._msg)) return;

                Message = string.Empty;
                // Define a test string.
                var msg = obj._msg;
                //string text = "The the quick 人 fox 狐狸 jumps over the lazy 狗狗.";
                Regex rx = new Regex(@"[^\u0000-\u00FF]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                // Find matches.
                MatchCollection matches = rx.Matches(msg);
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    var bytes = Encoding.Unicode.GetBytes(groups[0].Value);
                    var temp = $"\\u{string.Format("{0:X2}", bytes[1])}{string.Format("{0:X2}", bytes[0])}".ToLower();
                    msg = rx.Replace(msg, temp, 1);
                }

                Message += msg;
            }
        }

        private void CmdReceived(IModuleInfo obj)
        {
            if (obj.ModuleType == typeof(EncodeModule).AssemblyQualifiedName)
                _eventAggregator.GetEvent<MessageSentEvent>().Publish(new EventWithPater(obj, Message));

        }
    }
}
