using System;
using System.Collections.Generic;
using System.Reflection;

namespace _tryconsole
{
    public class _gateway
	{
		Dictionary<Guid, List<Object>> _modulescontainer = new Dictionary<Guid, List<Object>>() {};

		public _gateway() {}

		public static void Main(string[] args)
		{
			_gateway _gateway = new _gateway();

			_gateway._createmodules();
			_gateway._outputmodulebehaviourminimal();

			_gateway._miscfunction();
		}

		public void _createmodules()
		{
			try
			{
				// TODO:
				_moduleconfiguration _samplemoduleconfiguration = this._getsamplemoduleconfiguration();
				_builddynamicmodule _dynamicmodule1 = new _builddynamicmodule(_samplemoduleconfiguration);
				
				Object? _newinstanceobject1 = _dynamicmodule1._haveaninstance();
				this._addinstanceobjecttomodulescontainer(_newinstanceobject1);

				// TODO:
				_samplemoduleconfiguration._modulename = "_student_another";
				_builddynamicmodule _dynamicmodule2 = new _builddynamicmodule(_samplemoduleconfiguration);
				
				Object? _newinstanceobject2 = _dynamicmodule2._haveaninstance();
				this._addinstanceobjecttomodulescontainer(_newinstanceobject2);
				Object? _newinstanceobject3 = _dynamicmodule2._haveaninstance();
				this._addinstanceobjecttomodulescontainer(_newinstanceobject3);
			}
			catch (Exception _exception)
			{
				Console.WriteLine("EXCEPTION: " + _exception.Message);
			}
		}

		private void _addinstanceobjecttomodulescontainer(Object? _instanceobject)
		{
			if (this._modulescontainer != null && _instanceobject != null)
			{
				Object _newinstanceobject = _instanceobject ?? new Object();

				Guid _moduleguid = _newinstanceobject.GetType().GUID;
				if (!_modulescontainer.ContainsKey(_moduleguid))
				{
					_modulescontainer.Add(_moduleguid, new List<Object>());
				}
				if (_modulescontainer.ContainsKey(_moduleguid))
				{
					_modulescontainer[_moduleguid].Add(_newinstanceobject);
				}
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
			// output the modules found in _modulescontainer
			if (this._modulescontainer != null)
			{
				string _message = string.Empty;

				int _modulecount = 0;
				foreach (KeyValuePair<Guid, List<Object>> _modulecontainer in _modulescontainer)
				{
					var _module = _modulecontainer.Value[0].GetType();

					_message += "[ module " + ++_modulecount + ". ] ";
					_message += _module.Name + " ;name | " + _module.ToString() + " ;type | " + _modulecontainer.Key + " ;GUID {\n";

					foreach (Object _instanceobject in _modulecontainer.Value)
					{
						Type _instance = _instanceobject.GetType();

						_message += "\tan instance " + "\n"; // TODO: add instance GUID here

						int _propertycount = 0;
						foreach (PropertyInfo _property in _instance.GetProperties())
						{
							_message += "\t[ property " + ++_propertycount + ". ] ";
							_message += _property.Name + " ;name || " + _property?.ToString()?.Split(" ").FirstOrDefault() + " ;type\n";
						}
					}

					_message += "}\n";
				}

				Console.WriteLine(_message);
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
