using System.Collections.Generic;

namespace Scripts.Levels.MatrixGame
{
    public class CodeTargetView : CodeView
    {
        public void Init(IReadOnlyList<string> codeParts)
        {
            ResetQueueItems();

            for (int i = 0; i < codeParts.Count; i++)
            {
                Items[i].text = codeParts[i];
            }
        }

        public void Select() => SelectNext();
    }
}
