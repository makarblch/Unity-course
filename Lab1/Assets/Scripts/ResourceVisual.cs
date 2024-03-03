using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class ResourceVisual : MonoBehaviour
    {
        [SerializeField] ResourceBank _bank;
        [SerializeField] List<TMP_Text> _resourceTexts;

        private readonly List<GameResource> _gameResources = new()
            { GameResource.Food, GameResource.Gold, GameResource.Humans, GameResource.Stone, GameResource.Wood };
        
        private void Awake()
        {
            foreach (GameResource res in _gameResources)
            {
                _bank.GetResource(res).OnValueChanged += value =>
                    _resourceTexts[(int)res].text = $"{value}";
            }
        }
    }
}

