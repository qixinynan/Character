using System.Collections.Generic;
using Game.GameRule;

namespace Game.Player
{
    public class HumanPlayer: BasePlayer
    {
        
        public override bool IsHumanPlayer => true;
        public HumanPlayer(int id,IGameRule gameRule) : base(id, gameRule)
        {
        }

        public override void StartRound()
        {
            base.StartRound();
            DrawTile();
        }
    }
}