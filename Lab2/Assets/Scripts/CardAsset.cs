using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "Asset", menuName = "CardAsset")]
    public class CardAsset : ScriptableObject
    {
        [SerializeField]public string cardName;
        [SerializeField] public string color;
     
        [SerializeField] internal Sprite onSprite;
        [SerializeField] internal Sprite offSprite;
    }
}

