using System;

namespace ClumsySanta
{
    public interface IGameEngineSceneItem
    {
        void Appear(EventHandler completed);
    }
}