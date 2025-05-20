using System.Collections.Generic;
using UnityEngine;

namespace Common.Player
{
    [CreateAssetMenu(fileName = "FundSourceRepository", menuName = "Settings/FundSourceRepository")]
    public class FundSourceRepository : ScriptableObject
    {
        [field: SerializeField]
        public List<FundSource> FundSources { get; private set; }

        public FundSource GetRandomFundSource()
        {
            int randomIndex = Random.Range(0, FundSources.Count);
            return FundSources[randomIndex];
        }
    }
}