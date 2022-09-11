using System.Reflection;

namespace _tryconsole
{
    public class _gateway
	{
		List<Object> _instanceobjects = new List<Object>();

		public _gateway() { }

		public static void Main(string[] args)
		{
			_gateway _gateway = new _gateway();

			_gateway._createmodules();

			_gateway._miscfunction();
		}

		public void _createmodules()
		{
			try
			{
				_gateway _gateway = this;

				_moduleconfiguration _samplemoduleconfiguration = _gateway._getsamplemoduleconfiguration();

				_builddynamicmodule _dynamicmodule = new _builddynamicmodule(_samplemoduleconfiguration);
				Object? _newinstanceobject = _dynamicmodule._haveaninstance();
				
				if (_newinstanceobject != null) {
					_gateway._instanceobjects.Add(_newinstanceobject);
				}

				_gateway._outputmodulebehaviourminimal();
			}
			catch(Exception _exception)
			{
				Console.WriteLine("EXCEPTION: " + _exception.Message);
			}
		}

		public _moduleconfiguration _getsamplemoduleconfiguration()
		{
			_moduleconfiguration _samplemoduleconfiguration = new _moduleconfiguration(
				"_student", 
				new List<_propertyconfiguration>() {
					new _propertyconfiguration("int", "_id"),
					new _propertyconfiguration("string", "_fullname"),
					new _propertyconfiguration("string", "_address"),
					new _propertyconfiguration("bool", "_isdied")
				}
			);
			return _samplemoduleconfiguration;
		}

		private void _outputmodulebehaviourminimal()
		{
			// output the models found in this._models
			if(this._instanceobjects != null)
			{
				int _modulecount = 0;
				foreach(Object _instanceobject in this._instanceobjects)
				{
					if (_instanceobject != null)
					{
						Type _instance = _instanceobject.GetType();

						string _message = String.Empty + "[ module " + ++_modulecount + ". ] ";
						_message += _instance.Name + " ;name || " + _instance.ToString() + " ;type {\n";

						int _propertycount = 0;
						foreach(PropertyInfo _property in _instance.GetProperties())
						{
							_message += "\t[ property " + ++_propertycount + ". ] ";
							_message += _property.Name + " ;name || " + _property?.ToString()?.Split(" ").FirstOrDefault() + " ;type\n";
						}

						_message += "}\n";

						Console.Write(_message);
					}
				}
			}
		}

		private void _readmoduleconfiguration()
		{
			// TODO:
			/* Console.WriteLine("Enter a string: ");
			Console.ReadLine(); */
		}

		private void _inputintoamoduleproperties(Object _instanceobject)
		{
			// TODO:
			/*  */
		}

		private void _outputamoduleproperties(Object _instanceobject)
		{
			if (_instanceobject != null)
			{
				Type _instance = _instanceobject.GetType();

				// TODO:
				/* var _studentaddress = _instance.GetProperty("_address");
				Console.Write(_studentaddress?.Name + " = ");

				_studentaddress?.SetValue(_instanceobject, "Sector 52, Gurgaon", null);
				Console.WriteLine( _studentaddress?.GetValue(_instanceobject, null)?.ToString()); */
			}
		}

		public void _miscfunction()
		{
			Console.WriteLine("\n[ List of available types of properties: ]");
			Console.WriteLine("int : " + typeof(int).Name);
			Console.WriteLine("float : " + typeof(float).Name);
			Console.WriteLine("double : " + typeof(double).Name);
			Console.WriteLine("string : " + typeof(string).Name);
			Console.WriteLine("char : " + typeof(char).Name);
			Console.WriteLine("bool : " + typeof(bool).Name);
			Console.WriteLine("Int16 : " + typeof(Int16).Name);
			Console.WriteLine("Int32 : " + typeof(Int32).Name);
			Console.WriteLine("Int64 : " + typeof(Int64).Name);
			Console.WriteLine("String : " + typeof(String).Name);
		}
	}
}
