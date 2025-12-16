using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;

namespace ClumsySanta
{
    internal class BatchAppearManager
    {
        public void BatchAppear(IEnumerable<IGameEngineSceneItem> sceneItems, Action batchAppearCompleted)
        {
            var itemsList = sceneItems.ToList();
            var itemsCount = itemsList.Count;

            EventHandler storyCompleted = null;
            storyCompleted = (dStory, dea) =>
            {
                ((Storyboard)dStory).Completed -= storyCompleted;
                itemsCount--;
                if (itemsCount > 0)
                    return;

                batchAppearCompleted();
            };
            itemsList.ForEach(p => p.Appear(storyCompleted));
        }
    }
}