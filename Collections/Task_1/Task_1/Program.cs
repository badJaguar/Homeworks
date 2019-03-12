using System;
using System.Collections.Generic;
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

        public override bool Equals(object other)
        {
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

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
            var dbEntities = entities.ToList();
            if (_entities.Count.Equals(dbEntities.Count))
            {
                _entities.Clear();
            }

            _entities.AddRange(dbEntities);

            var fnKeys = (from e in dbEntities
                          let w = new FirstLastKey(e.FirstName, e.LastName)
                          select w);

            var ageKeys = (from e in dbEntities
                           let w = new AgeKey(e.Age)
                           select w);

            if (_fnDict.Count == 1 && _ageDict.Count == 1)
            {
                _fnDict.Clear();
                _ageDict.Clear();
            }

            foreach (var orderAgeKey in ageKeys.Select(key => key ))
                _ageDict.Add(orderAgeKey, _entities.Select(entity => entity).ToList());

            foreach (var orderFirstLastKey in fnKeys.Select(key =>key ))
                _fnDict.Add(orderFirstLastKey, _entities.Select(entity => entity).ToList());
        }


        public List<DbEntity> FindBy(string firstName, string lastName)
        {
            var list = new List<DbEntity>();

            var result = (from values in _fnDict.Values
                          from entities in values.FindAll(
                              entity =>
                                  entity.FirstName == firstName &&
                                  entity.LastName == lastName)
                          select entities).AsParallel().ToList();

            list.AddRange(result);
            return list;
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
        db.AddRange(dbGenerator.GetSequence(10000));

        var items = db.FindBy("Jack", "Jones");
        Console.WriteLine(items.Count);

        var items2 = db.FindBy(30);
        Console.WriteLine(items2.Count);
        Console.ReadKey();
    }
}