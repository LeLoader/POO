using System;
using System.Collections.Generic;

namespace TU_Challenge.Heritage
{
    public abstract class Animal
    {
        protected enum FeedingState { HUNGRY, FED, ATE_FISH }
        protected FeedingState state = FeedingState.HUNGRY;

        protected abstract Dictionary<FeedingState, string> feedingStrings { get; set; }
        public bool IsAlive { get; set; } = true;
        public string Name { get; set; }
        public virtual int Pattes { get; }
        public bool IsFed => state != FeedingState.HUNGRY;
        public string ActualNotFedMessage { protected get; set; }
        public string ActualFedMessage { protected get; set; }

        public event Action OnDie;

        public Animal(string name)
        {
            Name = name;
        }

        public string Crier()
        {
            if (feedingStrings.TryGetValue(state, out string cri))
            {
                return cri;
            }
            return "*Cet animal est muet*";
        }

        public void Die()
        {
            IsAlive = false;
            OnDie?.Invoke();
        }

        public void Feed()
        {
            state = FeedingState.FED;
        }

        public void FeedWithFish()
        {
            state = FeedingState.ATE_FISH;
        }

        public abstract void OnNewAnimal(Animal newAnimal);
    }
}
