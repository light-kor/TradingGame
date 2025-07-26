using System;
using Core.UI.Common;
using Core.UI.Providers;
using Zenject;

namespace Core.UI.Presenters
{
    public class RiskInfoPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly TooltipProvider _tooltipProvider;
        
        private ButtonProvider _riskInfoButton;        
        
        public void Initialize()
        {
            _riskInfoButton = _mainPanelProvider.RiskInfoButton;
            _riskInfoButton.OnButtonClicked += ShowTooltip;
        }
        
        public void Dispose()
        {
            _riskInfoButton.OnButtonClicked -= ShowTooltip;
        }

        private void ShowTooltip()
        {
            _tooltipProvider.ShowTooltip("AAAAAAA РИСК, это риск");
        }
    }
}