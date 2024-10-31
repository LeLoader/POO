using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Animalerie
    {
        List<Animal> animals = new();

        public event Action<Animal> OnAddAnimal;

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
            OnAddAnimal?.Invoke(animal);
            OnAddAnimal += animal.OnNewAnimal;
        }

        public bool Contains(Animal animal)
        {
            return animals.Contains(animal);
        }

        public Animal GetAnimal(int index)
        {
            return animals[index];
        }

        public void FeedAll()
        {
            foreach (Animal animal in animals)
            {
                animal.Feed();
            }
        }
    }
}
