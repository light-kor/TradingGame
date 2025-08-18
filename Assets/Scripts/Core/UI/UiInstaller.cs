using Core.GameOver;
using Core.TradePosition;
using Core.TradePosition.Close;
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
        
        [Required]
        [SerializeField]
        private TooltipProvider tooltipPanel;
        
        [Required]
        [SerializeField]
        private NewsPopupProvider newsPopupProvider;
        
        [Required]
        [SerializeField]
        private GameOverProvider gameOverProvider;
        
        [Required]
        [SerializeField]
        private PositionCloseProvider positionCloseProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<CoreMainPanelProvider>()
                .FromInstance(mainPanelProvider)
                .AsSingle();
            
            Container.Bind<TooltipProvider>()
                .FromInstance(tooltipPanel)
                .AsSingle();
            
            Container.Bind<NewsPopupProvider>()
                .FromInstance(newsPopupProvider)
                .AsSingle();
            
            Container.Bind<GameOverProvider>()
                .FromInstance(gameOverProvider)
                .AsSingle();
            
            Container.Bind<PositionCloseProvider>()
                .FromInstance(positionCloseProvider)
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
            
            Container.BindInterfacesAndSelfTo<RiskInfoPresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<NewsPopupPresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameOverPresenter>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<PositionClosePresenter>()
                .AsSingle();
        }
    }
}
