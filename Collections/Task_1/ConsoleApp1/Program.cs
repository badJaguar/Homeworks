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
            return string.Format("{0} {1}, age {2}", FirstName, LastName, Age);
        }
    }

    public class DbGenerator
    {
        private static Random _random = new Random((int) DateTime.Now.Ticks);

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

        public IEnumerable<List<DbEntity>> GetSequence(int count)
        {
            var list = new List<DbEntity>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(new DbEntity
                {
                    FirstName = _firstNames[_random.Next(_firstNames.Length - 1)],
                    LastName = _lastNames[_random.Next(_lastNames.Length - 1)],
                    Age = _random.Next(18, 60)
                });
            }

            yield return list;
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
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
            return base.GetHashCode();
        }
    }

    public class Database
    {
        private readonly List<DbEntity> _entities = new List<DbEntity>();

        private readonly Dictionary<FirstLastKey, List<DbEntity>> _fnDict =
            new Dictionary<FirstLastKey, List<DbEntity>>();

        public void AddRange(IEnumerable<DbEntity> entities)
        {
            _entities.AddRange(entities);
            // TODO #1 Set dictionary with key-value pairs.

        }

        public List<DbEntity> FindBy(string firstName, string lastName)
        {
            var list = new List<DbEntity>();
            var e = from r in _entities
                where r.FirstName == firstName &
                      r.LastName == lastName
                select r;
            list.AddRange(e);
            return list;
        }

        public IList<DbEntity> FindBy(int age)
        {
            var list = new List<DbEntity>();
            var e = from r in _entities
                where r.Age == age
                select r;
            list.AddRange(e);
            return list;
        }
    }

    public static void Main()
    {
        var dbGenerator = new DbGenerator();
        var db = new Database();
        var orderDbEntities = from d in dbGenerator.GetSequence(1000)
            from s in d
            select s;
        db.AddRange(orderDbEntities);

        var items = db.FindBy("Jack", "Jones");
        Console.WriteLine(items.Count);

        var items2 = db.FindBy(30);
        Console.WriteLine(items2.Count);
    }
}