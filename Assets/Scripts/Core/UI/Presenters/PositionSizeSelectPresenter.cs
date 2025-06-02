using System;
using Common.Player;
using Core.UI.Providers;
using Zenject;

namespace Core.UI.Presenters
{
    public class PositionSizeSelectPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly CoinPricePresenter _coinPricePresenter;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly MoneyFacade _moneyFacade;
        
        private PositionSizeSelectProvider _positionSizeSelectProvider;
        public int PositionSize { get; private set; }

        public void Initialize()
        {
            _positionSizeSelectProvider = _mainPanelProvider.PositionSizeSelectProvider;

            _positionSizeSelectProvider.SubscribeSlider(UpdatePositionSize);
            ConfigurePositionSizeSelector();
        }

        public void Dispose()
        {
            _positionSizeSelectProvider.UnsubscribeSlider(UpdatePositionSize);
        }
        
        private void ConfigurePositionSizeSelector()
        {
            _positionSizeSelectProvider.ConfigureSlider(_moneyFacade.MoneyValue);
            UpdatePositionSize(0f);
        }
        
        private void UpdatePositionSize(float value)
        {
            PositionSize = (int)value;
            _positionSizeSelectProvider.SetPositionSizeValue(PositionSize);
        }
    }
}