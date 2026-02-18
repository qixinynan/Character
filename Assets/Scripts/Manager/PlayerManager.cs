using System.Collections.Generic;
using System.Linq;
using Game;
using Game.GameRule;
using Game.Player;

namespace Manager
{
    public class PlayerManager
    {
        private readonly List<BasePlayer> _players = new List<BasePlayer>();
        private int _humanPlayerId;
        private int _currentPlayerIndex = -1;

        private int GetNewPlayerId()
        {
            return _players.Count;
        }

        public void Init(IGameRule rule)
        {
            _players.Clear();
            var player = new HumanPlayer(GetNewPlayerId(),rule);
            _humanPlayerId = player.Id;
            _players.Add(player);
            _currentPlayerIndex = -1;
        }

        public void InitVsAI(IGameRule rule)
        {
            Init(rule);
            var aiPlayer = new AIPlayer(GetNewPlayerId(), rule);
            _players.Add(aiPlayer);
        }

        private BasePlayer GetHumanPlayer()
        {
            return _players.Find(p => p.Id == _humanPlayerId);
        }

        public BasePlayer NextPlayer()
        {
            if (_players.Count == 0)
            {
                return null;
            }

            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            return _players[_currentPlayerIndex];
        }

        public BasePlayer GetCurrentPlayer()
        {
            if (_players.Count == 0)
            {
                return null;
            }

            if (_currentPlayerIndex < 0)
            {
                return _players[0];
            }
            return _players[_currentPlayerIndex];
        }

        public bool IsHumanPlayerRound()
        {
            var currentPlayer = GetCurrentPlayer();
            return currentPlayer != null && currentPlayer.Id == _humanPlayerId;
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
