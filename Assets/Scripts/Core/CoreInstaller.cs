using Core.Candles;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] 
        private GameSettings gameSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandleFactory>()
                .AsSingle();
        }
    }
}
