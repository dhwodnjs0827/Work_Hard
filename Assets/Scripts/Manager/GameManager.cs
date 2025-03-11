public class GameManager : Singleton<GameManager>
{
    public Player Player { get; private set; }
    
    public void InitPlayer(Player player)
    {
        if (Player == null)
        {
            Player = player;
        }
    }
}