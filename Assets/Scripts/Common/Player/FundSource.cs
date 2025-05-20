using System;
using UnityEngine;

namespace Common.Player
{
    [Serializable]
    public class FundSource
    {
        [field: SerializeField]
        public int Value { get; private set; }
        
        [field: SerializeField]
        public string Header { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }
    }
}