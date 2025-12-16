using System.Collections.Generic;

namespace ClumsySanta.ViewModel
{
    public class GameDecorationViewModelComparer : IComparer<GameDecorationViewModel>
    {
        public int Compare(GameDecorationViewModel x, GameDecorationViewModel y)
        {
            return x.LevelPriority - y.LevelPriority;
        }
    }
}