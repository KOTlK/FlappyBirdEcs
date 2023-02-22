using System;
using UnityEngine;

namespace Game.Runtime.Services.UI.Elements
{
    public class Button : UIElement, IButton
    {
        [SerializeField] private UnityEngine.UI.Button _origin;

        private void OnEnable()
        {
            _origin.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _origin.onClick.RemoveListener(OnClick);
        }

        public bool Clicked { get; set; }
        
        public void Reset()
        {
            Clicked = false;
        }

        private void OnClick()
        {
            Clicked = true;
        }
    }
}