using TMPro;
using UnityEngine;

namespace Game.Runtime.Services.UI.Elements
{
    public class Text : UIElement, IText
    {
        [SerializeField] private TMP_Text _text;
        
        public void SetString(string value)
        {
            _text.text = value;
        }
    }
}