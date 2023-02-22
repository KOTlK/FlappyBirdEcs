using UnityEngine;

namespace Game.Runtime.Services.Score
{
    public class GameScore : IGameScore
    {
        private const string Tag = nameof(GameScore);
        
        public int Current { get; set; }

        public int High
        {
            get
            {
                if (PlayerPrefs.HasKey(Tag))
                {
                    return PlayerPrefs.GetInt(Tag);
                }
                else
                {
                    PlayerPrefs.SetInt(Tag, 0);
                    return 0;
                }
            } 
            set => PlayerPrefs.SetInt(Tag, value);
        }

        public void ClearHighScore()
        {
            PlayerPrefs.DeleteKey(Tag);
        }
    }
}