using Cn.Core.Events;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using System.Text.RegularExpressions;

namespace Cn.Module.Decode.ViewModels
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

            //Message = "View A from your Prism Module DecodeContentRegion\n";


        }

        private void MessageReceived(EventWithPater obj)
        {
            if (obj.ModuleInfo.ModuleType != typeof(DecodeModule).AssemblyQualifiedName)
            {
                if (string.IsNullOrEmpty(obj._msg)) return;
                Message = string.Empty;
                var msg = Regex.Unescape(obj._msg);
                //msg = Helper.Unicode2String(obj._msg);
                //msg = HttpUtility.UrlDecode(obj._msg.Replace(@"\u", @"%u").Replace(@";", ""));
                Message += msg;
            }
        }

        private void CmdReceived(IModuleInfo obj)
        {
            if (obj.ModuleType == typeof(DecodeModule).AssemblyQualifiedName)
                _eventAggregator.GetEvent<MessageSentEvent>().Publish(new EventWithPater(obj, Message));

        }
    }
}
