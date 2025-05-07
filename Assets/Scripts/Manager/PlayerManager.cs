using System.Collections.Generic;
using System.Linq;
using Game;
using Game.GameRule;
using Game.Player;

namespace Manager
{
    public class PlayerManager
    {
        private List<BasePlayer> _players = new List<BasePlayer>();
        private int _humanPlayerId;

        private int GetNewPlayerId()
        {
            return _players.Count;
        }

        public void Init(IGameRule rule)
        {
            var player = new HumanPlayer(GetNewPlayerId(),rule);
            _humanPlayerId = player.Id;
            _players.Add(player);
        }

        private BasePlayer GetHumanPlayer()
        {
            return _players.Find(p => p.Id == _humanPlayerId);
        }

        public BasePlayer NextPlayer() // TODO:
        {
            return GetHumanPlayer();
        }

        public BasePlayer GetCurrentPlayer() // TODO:
        {
            return GetHumanPlayer();
        }

        public bool IsPlayerRound()
        {
            return GetCurrentPlayer().Id == _humanPlayerId;
        }

        public void InitTiles()
        {
            foreach (BasePlayer player in _players)
            {
                player.InitTiles();
            }
        }
        // private int _currentPlayerIndex;
    }
}