using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    public class CardLayout : MonoBehaviour
    {
        public int LayoutId;
        public int CardsNumber;
        public bool FaceUp;
        
        [SerializeField] public Vector2 offset;
        [SerializeField] public Vector2 cardOffset;

        private void Update()
        {
            foreach (var element in CardGame.Instance.CardsDictionary.Where(x => x.Key.LayoutId == LayoutId))
            {
                var cardView = element.Value;
                var cardInst = element.Key;
                try
                {
                    Transform cardTransform = cardView.GetComponent<Transform>();
                    
                    switch (cardInst.Status)
                    {
                        case 0:
                        {
                            FaceUp = false;
                            cardTransform.localPosition = Calculate(cardView.CardPosition, 0);
                            cardView.Rotate(FaceUp);
                            break;
                        }
                        case 1:
                        {
                            FaceUp = true;
                            cardTransform.localPosition = Calculate(cardView.CardPosition, 1);
                            cardView.Rotate(FaceUp);
                            break;
                        }
                        case 2:
                        {
                            FaceUp = true;
                            cardTransform.position = CardGame.Instance.CenterLayout.transform.position;
                            cardView.Rotate(FaceUp);
                            break;
                        }
                        case 3:
                        {
                            FaceUp = true;
                            cardTransform.localPosition = Calculate(cardView.CardPosition, 3);
                            cardView.Rotate(FaceUp);
                            break;
                        }
                        
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }

            }
        }
        
        
        private Vector2 Calculate(int siblingIndex, int status)
        {
            // Coordinates count
            switch (status)
            {
                // deck
                case 0:
                    return new Vector2(siblingIndex * offset.x, 0);
                // discard
                case 1:
                    return new Vector2(siblingIndex * offset.x, cardOffset.y);
                // hand
                case 3:
                    return new Vector2(0, cardOffset.y * siblingIndex);
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }

}
