using Core.UI.Presenters;
using Core.UI.Providers;
using TriInspector;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    public class UiInstaller : MonoInstaller
    {
        [Required]
        [SerializeField] 
        private CoreMainPanelProvider mainPanelProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<CoreMainPanelProvider>()
                .FromInstance(mainPanelProvider)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BalancePresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CoinPricePresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CurrentPriceLinePresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnterPriceLinePresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<PositionSizeSelectPresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CurrentPositionPresenter>()
                .AsSingle();
        }
    }
}
