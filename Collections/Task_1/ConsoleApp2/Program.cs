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
            return GetSequence(100);
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

        public override bool Equals(object obj)
        {
            bool flag = obj is FirstLastKey;

            if (flag)
            {
                var _obj = (FirstLastKey)obj;

                return _obj._firstName.Equals(this._firstName)
                    && _obj._lastName.Equals(this._lastName);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (this._firstName.GetHashCode())
                 & (this._lastName.GetHashCode());
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
            bool flag = obj is AgeKey;

            if (flag)
            {
                var _obj = (AgeKey)obj;
                return _obj._age.Equals(this._age);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this._age.GetHashCode();
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
            AddRangeToFirstLastDictionary(entities);
            AddRangeToAgeDictionary(entities);
        }

        private void AddRangeToFirstLastDictionary(IEnumerable<DbEntity> entities)
        {
            foreach (var entity in entities)
            {
                FirstLastKey key = new FirstLastKey(entity.FirstName, entity.LastName);

                if (_fnDict.ContainsKey(key))
                {
                    _fnDict[key].Add(entity);

                    continue;
                }

                _fnDict.Add(key, new List<DbEntity>() { entity });
            }
        }

        private void AddRangeToAgeDictionary(IEnumerable<DbEntity> entities)
        {
            foreach (var entity in entities)
            {
                AgeKey key = new AgeKey(entity.Age);

                if (_ageDict.ContainsKey(key))
                {
                    _ageDict[key].Add(entity);

                    continue;
                }

                _ageDict.Add(key, new List<DbEntity>() { entity });
            }
        }

        public IList<DbEntity> FindBy(string firstName, string lastName)
        {
            return _fnDict[new FirstLastKey(firstName, lastName)];
        }

        public IList<DbEntity> FindBy(int age)
        {
            return _ageDict[new AgeKey(age)];
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
    }
}