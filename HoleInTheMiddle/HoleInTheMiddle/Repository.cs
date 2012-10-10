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

        public string GetColorOfFirstFruitOfFruit(Fruit fruit)
        {
            string result;
            try
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Type";
                    parameter.Value = fruit.Type;
                    command.Parameters.Add(parameter);
                    command.CommandText = "SELECT TOP 1 [Color] FROM [Fruits] WHERE [Type] = @Type";
                    result = command.ExecuteScalar() as string;
                }
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }
        
        public int GetNumberOfFruitsOfFruit(Fruit fruit)
        {
            int? result;
            try
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Type";
                    parameter.Value = fruit.Type;
                    command.Parameters.Add(parameter);
                    command.CommandText = "SELECT COUNT * FROM [Fruits] WHERE [Type] = @Type";
                    result = command.ExecuteScalar() as int?;
                }
            }
            finally
            {
                _connection.Close();
            }
            return result.HasValue ? result.Value : 0;
        }
        
        public List<string> GetDistinctColorsOfFruit(Fruit fruit)
        {
            var result = new List<string>();
            try
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Type";
                    parameter.Value = fruit.Type;
                    command.Parameters.Add(parameter);
                    command.CommandText = "SELECT DISTINCT [Color] FROM [Fruits] WHERE [Type] = @Type";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       result.Add(reader.GetString(0)); 
                    }
                }
            }
            finally
            {
                _connection.Close();
            }
            return result;
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
        public Apple() : base("Apple")
        {
        }
    }

    public class Orange : Fruit
    {
        public Orange() : base("Orange")
        {
        }
    }
}
