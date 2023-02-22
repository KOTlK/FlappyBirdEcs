using UnityEngine;

namespace Game.Runtime.Services.UI.Elements
{
    public class UIElement : MonoBehaviour, IElement
    {
        public bool Active
        {
            get => gameObject.activeSelf;
            set
            {
                if (gameObject.activeSelf != value)
                {
                    gameObject.SetActive(value);
                }
            }
        }
    }
}