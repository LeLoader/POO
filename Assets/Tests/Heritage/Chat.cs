using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Chat : Animal
    {
        public override int Pattes { get; } = 4;
        protected override Dictionary<FeedingState, string> feedingStrings { get; set; } = new Dictionary<FeedingState, string>()
        {
            { FeedingState.HUNGRY, "Miaou (j'ai faim)" },
            { FeedingState.FED, "Miaou (c'est bon laisse moi tranquille humain)" },
            { FeedingState.ATE_FISH, "Miaou (Le poisson etait bon)" },
        };

        public Chat(string name) : base(name)
        {
            Name = name;
        }

        public override void OnNewAnimal(Animal newAnimal)
        {
            if(newAnimal is Poisson)
            {
                newAnimal.Die();
                FeedWithFish();
            }
        }
    }
}
