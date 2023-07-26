using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class GameAction
    {
        public enum ActionType
        {
            ANY,
            ATTACK,
            NOTATTACK
        }
        public List<string> actionWords = new List<string>();
        public Action actionMethod;
        public ActionType actionType;

        public GameAction(List<string> words, ActionType type, Action action)
        {
            actionWords = words;
            actionType = type;
            actionMethod = action;
        }
    }
}
