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
        public Action<List<string>> actionMethod;
        public ActionType actionType;

        public GameAction(List<string> words, Action<List<string>> action, ActionType type = ActionType.ANY)
        {
            actionWords = words;
            actionType = type;
            actionMethod = action;
        }
    }
}
