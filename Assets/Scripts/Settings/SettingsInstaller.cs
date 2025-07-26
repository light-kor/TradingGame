using TriInspector;
using UnityEngine;
using Zenject;

namespace Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [Required]
        [SerializeField] 
        private GameSettings gameSettings;
        
        [Required]
        [SerializeField] 
        private AnimationSettings animationSettings;
        
        [Required]
        [SerializeField] 
        private PriceMovePatternsRepository priceMovePatternsRepository;
        
        [Required]
        [SerializeField] 
        private NewsCandlesRepository newsCandlesRepository;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();
            Container.BindInstance(animationSettings).AsSingle();
            Container.BindInstance(priceMovePatternsRepository).AsSingle();
            Container.BindInstance(newsCandlesRepository).AsSingle();
        }
    }
}