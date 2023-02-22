using Game.Runtime.Services.UI.Elements;
using UnityEngine;

namespace Game.Runtime.Services.UI.Lose
{
    public class LoseScreen : UIElement, ILoseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _currentScore;
        [SerializeField] private Text _highScore;

        public IText CurrentScore => _currentScore;
        public IText HighScore => _highScore;
        public IButton Restart => _button;
    }
}