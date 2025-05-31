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
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();
            Container.BindInstance(animationSettings).AsSingle();
        }
    }
}