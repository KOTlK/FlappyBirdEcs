using Game.Runtime.Services.UI.Elements;
using UnityEngine;

namespace Game.Runtime.Services.UI.InGame
{
    public class InGameUI : UIElement, IInGame
    {
        [SerializeField] private Text _score;

        public IText Score => _score;
    }
}