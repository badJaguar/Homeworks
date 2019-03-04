using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Program
{
    public class DbEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, age {2}", FirstName, LastName, Age);
        }
    }

    public class DbGenerator
    {
        private static Random _random = new Random((int)DateTime.Now.Ticks);

        private static string[] _firstNames = new string[]
        {
            "Oliver",
            "Harry",
            "Jack",
            "George",
            "Noah",
            "Charlie",
            "Jacob",
            "Alfie",
            "Freddie"
        };

        private static string[] _lastNames = new string[]
        {
            "Smith",
            "Johnson",
            "Williams",
            "Jones",
            "Brown",
            "Davis",
            "Miller"
        };

        // TODO mplement GetSequence method using yield.
        // Solution
        public IEnumerable<DbEntity> GetSequence(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new DbEntity
                {
                    FirstName = _firstNames[_random.Next(_firstNames.Length - 1)],
                    LastName = _lastNames[_random.Next(_lastNames.Length - 1)],
                    Age = _random.Next(18, 60)
                };
            }
        }
    }

    public struct FirstLastKey
    {
        private readonly string _firstName;
        private readonly string _lastName;

        public FirstLastKey(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        // Solution
        public bool Equals(FirstLastKey otherValue)
        {
            return _firstName == otherValue._firstName && _lastName == otherValue._lastName;
        }

        public override bool Equals(object obj)
        {
            // TODO #1 Implement Equals method.
            // Solution
            return obj is FirstLastKey && Equals((FirstLastKey)obj);
        }

        public override int GetHashCode()
        {
            // TODO #1 Implement GetHashCode method.
            // Solution
            return _firstName.GetHashCode() ^ _lastName.GetHashCode();
        }
    }


    public struct AgeKey
    {
        private readonly int _age;

        public AgeKey(int age)
        {
            _age = age;
        }

        // Solution
        public bool Equals(AgeKey otherValue)
        {
            return _age == otherValue._age;
        }

        public override bool Equals(object obj)
        {
            // TODO #1 Implement Equals method.
            // Solution
            return obj is AgeKey && Equals((AgeKey)obj);
        }

        public override int GetHashCode()
        {
            // TODO #1 Implement GetHashCode method.
            // Solution
            return _age.GetHashCode();
        }
    }


    public class Database
    {
        private readonly List<DbEntity> _entities = new List<DbEntity>();
        private readonly Dictionary<FirstLastKey, List<DbEntity>> _fnDict = new Dictionary<FirstLastKey, List<DbEntity>>();
        private readonly Dictionary<AgeKey, List<DbEntity>> _ageDict = new Dictionary<AgeKey, List<DbEntity>>();


        public void AddRange(IEnumerable<DbEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("The input data can not be null.");
            }

            _entities.AddRange(entities);

            // TODO #1 Set dictionary with key-value pairs.
            // Solution   //TODO if collection is not null but empty than _entities[0].FirstName - exception ??
            var fnKey = new FirstLastKey(_entities[0].FirstName, _entities[0].LastName);
            var ageKey = new AgeKey(_entities[0].Age);

            _fnDict.Add(fnKey, _entities);
            _ageDict.Add(ageKey, _entities);
        }

        public IList<DbEntity> FindBy(string firstName, string lastName)
        {
            // TODO #1 Implement FinbBy method.
            // Solution 
            List<DbEntity> soughtValues = new List<DbEntity>();

            foreach (var item in _fnDict.Values)
            {
                Find(e => e.FirstName == firstName && e.LastName == lastName, item, soughtValues);
            }

            return soughtValues;
        }

        public IList<DbEntity> FindBy(int age)
        {
            // TODO #2 Add AgeKey struct, dictionary and implement FinbBy method.
            // Solution
            List<DbEntity> soughtValues = new List<DbEntity>();

            foreach (var item in _ageDict.Values)
            {
                Find(e => e.Age == age, item, soughtValues);
            }

            return soughtValues;
        }

        private void Find(Predicate<DbEntity> predicate, List<DbEntity> items, List<DbEntity> soughtValues)
        {
            var soughtEntity = items.Find(predicate);
            if (soughtEntity != null)
            {
                soughtValues.Add(soughtEntity);
            }
        }
    }

    public static void Main()
    {
        var dbGenerator = new DbGenerator();
        var db = new Database();
        db.AddRange(dbGenerator.GetSequence(10000));

        var items = db.FindBy("Jack", "Jones");
        Console.WriteLine(items.Count);

        var items2 = db.FindBy(30);
        Console.WriteLine(items2.Count);
        Console.ReadKey();
    }
}