using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace _tryconsole
{
	public class _gateway
	{
		public static void Main(string[] args)
		{
			_constructclass _studentmodel = new _constructclass("_student");
			var _studentclass = _studentmodel._config(new string[3]{"_id", "_name", "_address"}, new Type[3]{typeof(int), typeof(string), typeof(string)});
			
			Type _student = _studentclass.GetType();
			foreach (PropertyInfo _property in _student.GetProperties())
			{
				Console.WriteLine("[[ type: " + _property.ToString() + "]] \t\t\t\t [[ name: " + _property.Name + "]]");
			}

			//Console.ReadLine();
		}
	}
}
