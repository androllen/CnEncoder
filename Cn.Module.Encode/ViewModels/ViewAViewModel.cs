using Cn.Core.Events;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;

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
                Message = string.Empty;
                var msg = "";
                // obj.value=obj.value.replace(/[^\u0000-\u00FF]/g,function($0){return escape($0).replace(/(%u)(\w{4})/gi,"\\u$2")});
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
