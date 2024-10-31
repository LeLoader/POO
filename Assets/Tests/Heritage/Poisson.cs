using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Poisson : Animal
    {
        public override int Pattes { get; } = 0;
        protected override Dictionary<FeedingState, string> feedingStrings { get; set; } = new Dictionary<FeedingState, string>()
        {
        };
        public Poisson(string name) : base(AddSuffix(name)) { }

        static string AddSuffix(string name)
        {
            return name + " le poisson";
        }

        public override void OnNewAnimal(Animal newAnimal) { }
    }
}
