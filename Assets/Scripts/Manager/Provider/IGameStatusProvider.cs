public interface IGameStatusProvider
{
    public bool IsBossDead { get; set; }
    public bool IsChallengeBoss { get; set; }
    public bool IsGamePaused { get; set; }
}