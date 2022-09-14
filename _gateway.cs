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

			char _instanceoperation = 'b'; // TODO: take operation choice from user
			_gateway._traversemodules(_instanceoperation);
			_gateway._traversemodules('i');
			_gateway._traversemodules('o');

			_gateway._miscfunction1();
		}

		public void _createmodules()
		{
			/* Console.Write("Create (a)sample module (b)manual ,press corresponding bullet symbol: ");
			var _action = Console.ReadLine();
			switch (_action)
			{ } */
			
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

		private void _traversemodules(char _instanceoperation)
		{
			// traverse the module container
			if (this._modulescontainer != null)
			{
				int _modulecount = 0; // module counter

				// loop through modules (if any) found in module container
				foreach (KeyValuePair<Guid, List<Object>> _modulecontainer in _modulescontainer)
				{
					string _message = string.Empty;

					// module type
					Type? _module = _modulecontainer.Value[0].GetType();

					// output module order number
					_message += "(module " + ++_modulecount + ".)" + Environment.NewLine;
					_message += "[GUID] " + _modulecontainer.Key + Environment.NewLine;
					// output module description
					_message += "[name] " +_module.Name + Environment.NewLine;
					_message += "[type] " + _module.ToString() + Environment.NewLine;
					_message += " {";
					Console.WriteLine(_message);

					int _instancecount = 0; // module counter

					// loop through each instances for the module (if any)
					foreach (Object _instanceobject in _modulecontainer.Value)
					{
						// output instance order number
						_message = "\t(instance " + ++_instancecount + ".)" + Environment.NewLine;
						// output instance description
						_message += "\t[name] " + _instanceobject.GetType().Name; // TODO: add instance GUID here
						Console.WriteLine(_message);
				
						// pass the instance object to action propagator
						this._instanceactions(_instanceobject, _instanceoperation);
					}

					_message = "}";
					Console.WriteLine(_message);
				}

			}
		}

		private void _instanceactions(Object _instanceobject, char _instanceoperation)
		{
			if (_instanceobject != null)
			{
				Type _instance = _instanceobject.GetType();

				string _message = String.Empty;

				switch (_instanceoperation)
				{
					case 'b':
						_message += "\tNominal behaviour:";
						break;
					case 'i':
						_message += "\tEnter values for each property: ";
						break;
					case 'o':
						_message += "\tValues for each properties are: ";
						break;
				}
				
				Console.WriteLine(_message);

				int _propertycount = 0; // property counter

				// loop through each property of instance (if any)
				foreach (PropertyInfo _property in _instance.GetProperties())
				{
					_message = String.Empty;

					// output property order number
					_message += "\t\t(property " + ++_propertycount + ".) ";
					// output property description
					_message +=  _property.Name + " [" + _property?.ToString()?.Split(" ").FirstOrDefault() + "]";
					Console.WriteLine(_message);

					switch (_instanceoperation)
					{
						case 'b':
							this._outputpropertyminimalbehaviour(_instanceobject);
							break;
						case 'i':
							this._setpropertyvalue(_property, _instanceobject, "101 Sector 52, Gurgaon");
							break;
						case 'o':
							this._getpropertyvalue(_property, _instanceobject);
							break;
					}
				}
			}
		}

		private void _outputpropertyminimalbehaviour(Object _instanceobject)
		{
			Type _instance = _instanceobject.GetType();

			string _message = String.Empty;

			int _propertycount = 0;
			foreach (PropertyInfo _property in _instance.GetProperties())
			{
				_message += "\t[ property " + ++_propertycount + ". ] ";
				_message += _property.Name + " ;name || " + _property?.ToString()?.Split(" ").FirstOrDefault() + " ;type\n";
			}
		}

		private void _setpropertyvalue(PropertyInfo? _property, Object _instanceobject, Object _value)
		{
			if (_property != null) {
				Console.WriteLine("Enter value for " + _property.Name + ": " + _value.ToString() + " (auto assigned)");
				_property.SetValue(_instanceobject, _value, null);
			}
		}

		private void _getpropertyvalue(PropertyInfo? _property, Object _instanceobject)
		{
			if (_property != null) {
				Console.WriteLine(_property.GetValue(_instanceobject, null)?.ToString());
			}
		}

		private void _readmoduleconfiguration()
		{
			// TODO:
			/* Console.WriteLine("Enter a string: ");
			Console.ReadLine(); */
		}

		public _moduleconfiguration _getsamplemoduleconfiguration()
		{
			_moduleconfiguration _samplemoduleconfiguration = new _moduleconfiguration(
				"_student", 
				new List<_propertyconfiguration>() {
					//new _propertyconfiguration("Int32", "_id"),
					new _propertyconfiguration("String", "_fullname"),
					new _propertyconfiguration("String", "_address"),
					//new _propertyconfiguration("Boolean", "_isdied")
				}
			);
			return _samplemoduleconfiguration;
		}

		public void _miscfunction1()
		{
			Console.WriteLine("\n[ List of available types of properties: ]");
			Console.WriteLine("int : " + typeof(int).Name);
			Console.WriteLine("float : " + typeof(float).Name);
			Console.WriteLine("double : " + typeof(double).Name);
			Console.WriteLine("char : " + typeof(char).Name);
			Console.WriteLine("bool : " + typeof(bool).Name);
			Console.WriteLine("string : " + typeof(string).Name);
			Console.WriteLine("Int16 : " + typeof(Int16).Name);
			Console.WriteLine("Int32 : " + typeof(Int32).Name);
			Console.WriteLine("Int64 : " + typeof(Int64).Name);
			Console.WriteLine("String : " + typeof(String).Name);
		}

		public void _miscfunction2()
		{
			do
			{
				DateTime _datetime = DateTime.Now;
				Console.WriteLine("The time: {0:d} at {0:t}", _datetime);
				TimeZoneInfo _timezone = TimeZoneInfo.Local;
				Console.WriteLine("The time zone: {0}\n",
									_timezone.IsDaylightSavingTime(_datetime) ? _timezone.DaylightName : _timezone.StandardName);
				Console.Write("Press <Enter> to exit... ");
			}
			while (Console.ReadLine() == ConsoleKey.K.ToString());
		}
	}
}
