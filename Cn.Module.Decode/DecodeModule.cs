using Cn.Module.Decode.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Cn.Module.Decode
{
    public class DecodeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //get main region manage controller
            var regionManager = containerProvider.Resolve<RegionManager>();
            //set local view register to region
            regionManager.RegisterViewWithRegion("DecodeContentRegion", typeof(ViewA));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}