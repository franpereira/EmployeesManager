using System.Data;

namespace Employees.Model.Sql
{
    public static class SqlExtensions
    {
        /// <summary>
        /// Adds a parameter with the specified name and value to the IDbCommand object.
        /// </summary>
        /// <param name="cmd">The IDbCommand to add the parameter to.</param>
        /// <param name="name">The name of the parameter. E.g. "@id"</param>
        /// <param name="value">The value of the parameter.</param>
        public static void AddParameter(this IDbCommand cmd, string name, object value)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            cmd.Parameters.Add(param);
        }
    }
}