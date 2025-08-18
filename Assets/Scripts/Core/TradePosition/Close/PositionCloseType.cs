namespace Core.TradePosition.Close
{
    public enum PositionCloseType
    {
        None = 0,
        Manual = 1,             // Игрок закрыл позицию вручную
        PositionLiquidated = 2, // Позиция ликвидирована (ROI <= -100%)
        CoinBankrupt = 3        // Монета обанкротилась (цена = 0)
    }
} 