using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    class Character
    {
        public string CharacterName { get; set; }
        public int ID { get; set; } 
        public CharStats Stats { get; set; }
        public CharSkills Skills { get; set; }
        public CharFeats Feats { get; set; }


    }
}
