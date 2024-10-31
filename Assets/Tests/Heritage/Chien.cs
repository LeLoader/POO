using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Chien : Animal
    {
        public override int Pattes { get; } = 4;
        protected override Dictionary<FeedingState, string> feedingStrings { get; set; } = new Dictionary<FeedingState, string>()
        {
            { FeedingState.HUNGRY, "Ouaf (j'ai faim)" },
            { FeedingState.FED, "Ouaf (viens on joue ?)" },
        };

        public Chien(string name) :base(name)
        {
            Name = name;
        }

        public override void OnNewAnimal(Animal newAnimal)
        {
            
        }
    }
}
