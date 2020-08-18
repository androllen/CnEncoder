using Cn.Core.Events;
using Cn.Module.Decode;
using Cn.Module.Encode;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using System.Linq;

namespace CnEncoder
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleCatalog _moduleCatalog;
        public MainWindowViewModel(IEventAggregator eventAggregator, IModuleCatalog moduleCatalog)
        {
            _eventAggregator = eventAggregator;
            _moduleCatalog = moduleCatalog;
        }
        private DelegateCommand cmdEncode;
        public DelegateCommand CmdEncode => cmdEncode ?? (cmdEncode = new DelegateCommand(() =>
        {
            var modules = _moduleCatalog.Modules.Where(p => p.ModuleType == typeof(DecodeModule).AssemblyQualifiedName).FirstOrDefault();
            _eventAggregator.GetEvent<CmdSentEvent>().Publish(modules);
        }));


        private DelegateCommand cmdDecode;
        public DelegateCommand CmdDecode => cmdDecode ?? (cmdDecode = new DelegateCommand(() =>
        {
            var modules = _moduleCatalog.Modules.Where(p => p.ModuleType == typeof(EncodeModule).AssemblyQualifiedName).FirstOrDefault();
            _eventAggregator.GetEvent<CmdSentEvent>().Publish(modules);
        }));
    }
}
