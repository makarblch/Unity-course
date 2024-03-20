using System.Collections;
using System.Collections.Generic;
using Cards;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardView : MonoBehaviour
    {
        private CardInstance _cardInstance;

        private Image _image;

        public int LayoutId
        {
            get => _cardInstance.CardPosition;
            set => _cardInstance.LayoutId = value;
        }
        
        public int Status
        {
            get => _cardInstance.Status;
            set => _cardInstance.Status = value;
        }
        public int CardPosition
        {
            get => _cardInstance.CardPosition;
            set => _cardInstance.CardPosition = value;
        }

        public void Init(CardInstance cardInstance, Image image)
        {
            _cardInstance = cardInstance;
            _image = image;
        }
        
        public void Rotate(bool up)
        {
            _image.sprite = up ? _cardInstance.Asset.onSprite : _cardInstance.Asset.offSprite;
        }

        public void PlayCard()
        {
            switch (_cardInstance.Status)
            {
                case 0:
                    
                case 1:
                    CardGame.Instance.MoveToCenter(_cardInstance);
                    _cardInstance.Status = 2;
                    break;
                case 2:
                    CardGame.Instance.MoveToDiscard(_cardInstance);
                    _cardInstance.Status = 3; 
                    break;
            }
        }
    }
}

