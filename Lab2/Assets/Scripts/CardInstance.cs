using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Cards
{
    public class CardInstance
    {
        internal CardAsset Asset;

        public CardInstance(CardAsset asset)
        {
            Asset = asset;
        }
        
        public int LayoutId { get; set; }
        public int CardPosition { get; set; }

        /* Status:
         0. In hand (face down)
         1. In hand (face up)
         2. In Center
         3. In Discard */
        public int Status = 0;

        public void MoveToLayout(int id)
        {
            int temp = LayoutId;
            LayoutId = id;
            CardPosition = CardGame.Instance.Layouts[id].CardsNumber++;
            CardGame.Instance.RecalculateLayout(id);

            // New parent for current card
            CardGame.Instance.CardsDictionary[this].transform.SetParent(CardGame.Instance.Layouts[id].transform);
            
            // Recalculate new layout
            CardGame.Instance.RecalculateLayout(id);
            
            // Recalculate old layout
            CardGame.Instance.RecalculateLayout(temp);
        }
    }
}

