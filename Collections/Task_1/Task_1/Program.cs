using System;
using System.Collections.Generic;

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

        public IEnumerable<DbEntity> GetSequence()
        {
            yield return new DbEntity
            {
                FirstName = _firstNames[_random.Next(_firstNames.Length - 1)],
                LastName = _lastNames[_random.Next(_lastNames.Length - 1)],
                Age = _random.Next(18, 60)
            };
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

        public override bool Equals(object other) =>
            this._firstName.Equals(other) && this._lastName.Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + _firstName.GetHashCode();
                var hash1 = hash * 22 + _lastName.GetHashCode();
                return hash1;
            }
        }
    }

        public struct AgeKey
        {
            private readonly int _age;
            public AgeKey(int age)
            {
                this._age = age;
            }

            public override bool Equals(object obj)=>
                this._age.Equals(obj);


            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + _age.GetHashCode();
                    return hash;
                }
            }
        }

    public class Database
    {
        private readonly List<DbEntity> _entities = new List<DbEntity>();
        private readonly Dictionary<FirstLastKey, List<DbEntity>> _fnDict = new Dictionary<FirstLastKey, List<DbEntity>>();
        private readonly Dictionary<AgeKey, List<DbEntity>> _ageDict = new Dictionary<AgeKey, List<DbEntity>>();

        public void AddRange(IEnumerable<DbEntity> entities)
        {
            _entities.AddRange(entities);
            foreach (var entity in _entities)
            {
                var key = new FirstLastKey(entity.FirstName, entity.LastName);
                if (!_fnDict.ContainsKey(key)) continue;
                _fnDict[key].Add(entity);
            }
        }

        public IList<DbEntity> FindBy(string firstName, string lastName)
        {
            return new DbEntity[] { };
        }

        public IList<DbEntity> FindBy(int age)
        {
            // TODO #2 Add AgeKey struct, dictionary and implement FinbBy method.
            return new DbEntity[] { };
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