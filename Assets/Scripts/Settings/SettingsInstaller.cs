using UnityEngine;
using Zenject;

namespace Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] 
        private GameSettings gameSettings;
        
        [SerializeField] 
        private AnimationSettings animationSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();
            Container.BindInstance(animationSettings).AsSingle();
        }
    }
}