using DevUtils;
using UnityEngine;
using Zenject;

namespace Core
{
    public class RandomUtils
    {
        [Inject] private readonly DevTestSettings _devTestSettings;
        
        public bool IsLong()
        {
            if (_devTestSettings.IsPriceOverrideActive())
                return _devTestSettings.GetForcedCandleDirection();
            
            int value = Random.Range(0, 2);
            return value == 0;
        }
    }
}