using ApiTemplate.Model.EF;
using ApiTemplate.Model.Models;

namespace ApiTemplate.Business.ViewModels
{
    public class AnimalViewModel
    {
        public AnimalViewModel() { }
        public AnimalViewModel(Animal animal) 
        {
            Id = animal.Id;
            Name = animal.Name;
            Description = animal.Description;
            AnimalType = animal.AnimalType;
        }
        public AnimalViewModel(Animal animal, IEnumerable<PhotoViewModel> photos)
                : this(animal)
        {
            Photos = photos;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AnimalTypes AnimalType {  get; set; }
        public IEnumerable<PhotoViewModel> Photos { get; set; }
    }
}


//        internal IEnumerable<string> _actions;
//        public abstract TResult DoSomething<TResult>() where TResult : class;

//    public string DoSomething()
//        {
//            StringBuilder stringBuilder = new StringBuilder();
//            Random random = new Random();
//            int numOfActions = random.Next(1, 6);

//            for (int i = 0; i < numOfActions; i++)
//            {
//                if (i > 0)
//                    stringBuilder.Append($"and,{Environment.NewLine}");

//                //choose a random action and append it to the return payload
//                int index = random.Next(1, 4);
//                stringBuilder.Append(_actions.ElementAt(index - 1));

//                if (i == numOfActions - 1)
//                    stringBuilder.Append(".");
//            }

//            return stringBuilder.ToString();
//        }


//        protected string _woof() { return $"{Name} woofs"; }
//        protected string _fetch() { return $"{Name} finds a toy and brings it back"; }
//        protected string _lick() { return $"{Name} licks your face"; }
//        protected string _sniff() { return $"{Name} zeroes in on something interesting and sniffs"; }

//        public Dog(string name, string desc)
//        {
//            Name = name;
//            Description = desc;
//            _animalType = AnimalTypes.Dog;
//            _actions = new List<string>() { _woof(), _fetch(), _lick(), _sniff() };
//        }
//    }

//    public class Cat : AnimalViewModel
//    {

//        protected string _meow() { return $"{Name} meows"; }
//        protected string _pounce() { return $"{Name} hunkers down and pounces on a nearby toy"; }
//        protected string _climb() { return $"{Name} find the tallest thing in the room and climbs it"; }
//        protected string _hide() { return $"{Name} is done with people now and finds a place to hide"; }

//        public Cat(string name, string desc)
//        {
//            Name = name;
//            Description = desc;
//            _animalType = AnimalTypes.Cat;
//            _actions = new List<string>() { _meow(), _pounce(), _climb(), _hide() };
//        }
//    }
//}
