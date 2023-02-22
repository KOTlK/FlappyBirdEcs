namespace Game.Runtime.Services.Score
{
    public interface IGameScore
    {
        int Current { get; set; }
        int High { get; set; }
        void ClearHighScore();
    }
}