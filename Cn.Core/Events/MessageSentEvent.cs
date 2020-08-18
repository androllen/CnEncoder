using Prism.Events;
using Prism.Modularity;

namespace Cn.Core.Events
{
    public class EventWithPater
    {
        private readonly IModuleInfo _moduleInfo;
        public string _msg;

        public IModuleInfo ModuleInfo => _moduleInfo;
        public string ModuleType => ModuleInfo?.ModuleType;

        public EventWithPater(IModuleInfo moduleInfo, string msg)
        {
            _moduleInfo = moduleInfo;
            _msg = msg;
        }
    }
    public class MessageSentEvent : PubSubEvent<EventWithPater>
    {
    }
}
