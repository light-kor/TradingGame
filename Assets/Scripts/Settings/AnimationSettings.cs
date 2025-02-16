using DG.Tweening;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "AnimationSettings", menuName = "Settings/AnimationSettings")]
    public class AnimationSettings : ScriptableObject
    {
        [field: Header("Candle spawn")] 
        [field: SerializeField]
        public float CandleAnimationDuration { get; private set; }
        
        [field: SerializeField]
        public Ease CandleAnimationEase { get; private set; }
        
        [field: Header("Camera move")] 
        [field: SerializeField]
        public float CameraMoveDuration { get; private set; }
        
        [field: SerializeField]
        public Ease CameraMoveEase { get; private set; }
        
        [field: SerializeField]
        public float CameraMoveXOffset { get; private set; }
    }
}