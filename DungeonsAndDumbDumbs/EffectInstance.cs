using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class EffectInstance
    {
        public Program.Effect effectType;
        public float effectDuration;
        public bool isIndefinite = false;
        public EffectInstance(Program.Effect effect, float duration = 0)
        {
            effectType = effect;
            if (duration == 0) isIndefinite = true;
            effectDuration = duration;
        }
    }
}
