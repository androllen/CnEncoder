using Cn.Module.Encode.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Cn.Module.Encode
{
    public class EncodeModule : IModule
    {
        public EncodeModule()
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //get main region manage controller
            var regionManager = containerProvider.Resolve<RegionManager>();
            //set local view register to region
            regionManager.RegisterViewWithRegion("EncodeContentRegion", typeof(ViewA));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}