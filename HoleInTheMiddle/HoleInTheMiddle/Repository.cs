using System;
using System.Collections.Generic;
using System.Data.Common;

namespace HoleInTheMiddle
{
    public class Repository
    {
        private readonly DbConnection _connection;

        public Repository(DbConnection connection)
        {
            _connection = connection;
        }

        TResult RunQuery<TResult>(Func<DbCommand, TResult> doWork)
        {
            TResult result;
            try
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    result = doWork(command);
                }
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public string GetColorOfFirstFruitOfFruit(Fruit fruit)
        {
            return RunQuery(command =>
                                {
                                    command.AddParameter("@Type", fruit.Type);
                                    command.CommandText = "SELECT TOP 1 [Color] FROM [Fruits] WHERE [Type] = @Type";
                                    return command.ExecuteScalar() as string;
                                });
        }

        public int GetNumberOfFruitsOfFruit(Fruit fruit)
        {
            return RunQuery(command =>
                                {
                                    command.AddParameter("@Type", fruit.Type);
                                    command.CommandText = "SELECT COUNT * FROM [Fruits] WHERE [Type] = @Type";
                                    var result = command.ExecuteScalar() as int?;
                                    return result.HasValue ? result.Value : 0;
                                });
        }

        public List<string> GetDistinctColorsOfFruit(Fruit fruit)
        {
            return RunQuery(command =>
                                {
                                    var result = new List<string>();
                                    command.AddParameter("@Type", fruit.Type);
                                    command.CommandText = "SELECT DISTINCT [Color] FROM [Fruits] WHERE [Type] = @Type";
                                    var reader = command.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        result.Add(reader.GetString(0));
                                    }
                                    return result;
                                });
        }
    }

    public static class CommandExtensions
    {
        public static void AddParameter<T>(this DbCommand command, string name, T value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }

    public abstract class Fruit
    {
        public Guid Id { get; set; }
        public string Type { get; private set; }
        public string Color { get; set; }

        protected Fruit(string type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }
    }

    public class Apple : Fruit
    {
        public Apple()
            : base("Apple")
        {
        }
    }

    public class Orange : Fruit
    {
        public Orange()
            : base("Orange")
        {
        }
    }
}
