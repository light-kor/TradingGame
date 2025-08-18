using System;
using Core.UI.Providers;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.TradePosition
{
    public class CurrentPositionPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly GameSettings _settings;
        
        private CurrentPositionProvider _currentPositionProvider;
        
        public event Action<bool> OnOpenPositionClicked = delegate { };
        public event Action OnClosePositionClicked = delegate { };
        
        public void Initialize()
        {
            _mainPanelProvider.LongButton.OnButtonClicked += HandleOpenLongPositionClicked;
            _mainPanelProvider.ShortButton.OnButtonClicked += HandleOpenShortPositionClicked;
            _mainPanelProvider.ClosePositionButton.OnButtonClicked += HandleClosePositionClicked;
            
            _currentPositionProvider = _mainPanelProvider.CurrentPositionProvider;
            _currentPositionProvider.SetActive(false);
        }

        public void Dispose()
        {
            _mainPanelProvider.LongButton.OnButtonClicked -= HandleOpenLongPositionClicked;
            _mainPanelProvider.ShortButton.OnButtonClicked -= HandleOpenShortPositionClicked;
            _mainPanelProvider.ClosePositionButton.OnButtonClicked -= HandleClosePositionClicked;
        }
        
        private void HandleOpenLongPositionClicked()
        {
            OnOpenPositionClicked.Invoke(true);
        }

        private void HandleOpenShortPositionClicked()
        {
            OnOpenPositionClicked.Invoke(false);
        }

        private void HandleClosePositionClicked()
        {
            OnClosePositionClicked.Invoke();
        }
        
        public void OpenPosition(int margin)
        {
            _currentPositionProvider.SetPositionMargin(margin);
            _currentPositionProvider.SetActive(true);
        }

        public void UpdatePosition(int pnl, float roi)
        {
            Color roiColor = roi < 0 ? _settings.ShortColor : _settings.LongColor;
            
            _currentPositionProvider.UpdateCurrentPnl(pnl);
            _currentPositionProvider.UpdateCurrentRoi(roi, roiColor);
        }

        public void ClosePosition()
        {
            _currentPositionProvider.SetActive(false);
        }
    }
}