using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Program
{
    public class DbEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, age {Age}";
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
            for (var i = 0; i < count; i++)
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
            _age = age;
        }

        public override bool Equals(object p) =>
            this._age.Equals(p);

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

        private readonly Dictionary<FirstLastKey, List<DbEntity>> _fnDict =
            new Dictionary<FirstLastKey, List<DbEntity>>();

        private readonly Dictionary<AgeKey, List<DbEntity>> _ageDict =
            new Dictionary<AgeKey, List<DbEntity>>();

        public void AddRange(IEnumerable<DbEntity> entities)
        {
            _entities.AddRange(entities);
            foreach (var en in _entities)
            {
                var entFn = new FirstLastKey(en.FirstName,en.LastName);
                var entAge = new AgeKey(en.Age);

                if (_fnDict.ContainsKey(entFn) && _ageDict.ContainsKey(entAge)) continue;

                _fnDict.Add(entFn, new List<DbEntity>(_entities));
                _ageDict.Add(entAge, new List<DbEntity>(_entities));
                break;
            }
        }

        public List<DbEntity> FindBy(string firstName, string lastName)
        {
            return _entities.Any(orderEntity => 
                _fnDict.ContainsKey(new FirstLastKey(orderEntity.FirstName, orderEntity.LastName))) 
                ? _entities : null;
        }

        public IList<DbEntity> FindBy(int age)
        {
            var list = new List<DbEntity>();

            var result = (from ages in _ageDict.Values
                          from entities in ages.FindAll(
                              entity =>
                                  entity.Age == age)
                          select entities).AsParallel();

            list.AddRange(result);
            return list;
        }

    }

    public static void Main()
    {
        var dbGenerator = new DbGenerator();
        var db = new Database();

        var orderDbEntities = from d in dbGenerator.GetSequence(1000)
                              select d;
        db.AddRange(orderDbEntities);
        var items = db.FindBy("Jack", "Johnson");

        var ages = db.FindBy(30);

        var orderDbEntities1 = from d in dbGenerator.GetSequence()
                               select d;

        db.AddRange(orderDbEntities1);
        var items1 = db.FindBy("Charlie", "Smith");
        Console.WriteLine($"items: {items.Count}\titems1: {items1.Count}");

        var ages1 = db.FindBy(30);
        Console.WriteLine($"ages:  {ages.Count}\tages1:  {ages1.Count}");
    }
}
