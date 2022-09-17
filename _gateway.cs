using System;
using System.Collections.Generic;
using System.Reflection;

namespace _tryconsole
{
    public class _gateway
	{
		Dictionary<Guid, List<Object>> _modulescontainer = new Dictionary<Guid, List<Object>>() {};

		public _gateway() {}

		public static int Main(string[] args)
		{
			_gateway _gateway = new _gateway();

			try
			{
				_gateway._primarymenu();
			}
			catch (Exception _exception)
			{
				Console.WriteLine("ERROR: " + _exception.Message);
			}

			return 0;
		}

		private void _primarymenu()
		{
			string _message = String.Empty;
			ConsoleKeyInfo _mayday;

			do
			{
				_message += Environment.NewLine;
				_message += "[ MENU ]******************************************" + Environment.NewLine;
				_message += "1. Press <esc> for Exit App." + Environment.NewLine;
				_message += "2. Press <c> for Clearing Screen." + Environment.NewLine;
				_message += "3. Press <f> for Opening Function Menu." + Environment.NewLine;
				_message += "**************************************************" + Environment.NewLine;
				Console.Write(_message);

				_mayday = this._improviseamayday();

				switch (_mayday.Key)
				{
					case ConsoleKey.Escape:
						Environment.Exit(0);
						break;
					case ConsoleKey.C:
						Console.Clear();
						break;
					case ConsoleKey.F:
						do
						{
							Console.Write(this._functionmenu());
							_mayday = this._improviseamayday();

							if (_mayday.Key != ConsoleKey.Escape) {
								Console.WriteLine("|||||||||||||||||||||||||||||||||||||");
							}
							switch (_mayday.Key)
							{
								case ConsoleKey.A:
									do
									{
										Console.Write(this._moduleoperationmenu());
										_mayday = this._improviseamayday();
										switch (_mayday.Key)
										{
											case ConsoleKey.S:
												this._createsamplemodule();
												break;
											case ConsoleKey.C:
												this._traversemodules('c');
												break;
											case ConsoleKey.O:
												this._traversemodules('o');
												break;
											case ConsoleKey.M:
												this._createmodulemanually();
												break;
											case ConsoleKey.I:
												this._traversemodules('i');
												break;
										}
									}
									while (_mayday.Key != ConsoleKey.Escape); // TODO: update mechanism with previous menu
									break;
								case ConsoleKey.B:
									this._miscfunction1();
									break;
								case ConsoleKey.C:
									this._miscfunction2();
									break;
							}
							if (_mayday.Key != ConsoleKey.Escape) {
								Console.WriteLine("|||||||||||||||||||||||||||||||||||||");
							}
						}
						while (_mayday.Key != ConsoleKey.Escape); // TODO: update mechanism with previous menu
						break;
				}
			}
			while (true);
		}

		private string _functionmenu()
		{
			string _message = String.Empty;
			
			_message += "[ FUNCTION MENU ]*********************************" + Environment.NewLine;
			_message += "1. Press <a> for Module Creation & Operations." + Environment.NewLine;
			_message += "2. Press <b> for Misc Function 1." + Environment.NewLine;
			_message += "3. Press <c> for Misc Function 2." + Environment.NewLine;
			_message += "**************************************************" + Environment.NewLine;
			
			return _message;
		}

		private string _moduleoperationmenu()
		{
			string _message = String.Empty;

			_message += "[ MODULE MENU ]***********************************" + Environment.NewLine;
			_message += "1. Press <s> for Creating a Sample Module & It's Instance Prefilled" + Environment.NewLine;
			_message += "2. Press <c> for Output Cultural Behavior for All Module Instances (properties)" + Environment.NewLine;
			_message += "3. Press <o> for Output All Module Instances (properties)" + Environment.NewLine;
			_message += "4. Press <m> for Creating Manual Module & It's Instance(s)" + Environment.NewLine;
			_message += "5. Press <i> for New Input for All Module Instances (properties)" + Environment.NewLine;
			_message += "**************************************************" + Environment.NewLine;

			return _message;
		}

		public void _createmodulemanually()
		{
			
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

					_message += _modulecount > 0 ? Environment.NewLine : String.Empty;

					// output module order number
					_message += Environment.NewLine + "--(**module (" + ++_modulecount + ".))" + Environment.NewLine;
					// output module description
					_message += _module.FullName + " [" + _modulecontainer.Key + "]" + Environment.NewLine;
					_message += "[[[";
					Console.Write(_message);

					int _instancecount = 0; // module counter

					// loop through each instances for the module (if any)
					foreach (Object _instanceobject in _modulecontainer.Value)
					{
						// output instance order number
						_message = Environment.NewLine + "\t(**instance (" + ++_instancecount + ".))" + Environment.NewLine;
						// output instance description
						_message += "\t" + _instanceobject.GetType().Name + " [" + _modulecontainer.Key + "]"; // TODO: add instance GUID here
						Console.Write(_message);
				
						// pass the instance object to action propagator
						int _objectindentcount = 0;
						this._instanceactions(_instanceobject, _instanceoperation, _objectindentcount, _modulecontainer.Key);
					}

					_message = Environment.NewLine + "]]]";
					Console.Write(_message);
				}
			}
		}

		private void _instanceactions(Object? _instanceobject, char _instanceoperation, int _objectindentcount, Guid _moduleguid)
		{
			if (_instanceobject != null)
			{
				// set object indent
				++_objectindentcount; // it denotes this instances current level
				string _objectindent = string.Empty;
				for (int _index = 0; _index < _objectindentcount; _index++) {
					_objectindent += "\t";
				}
				
				Type _instance = _instanceobject.GetType();

				string _message = String.Empty;

				// output START
				// add indent to START only if instance level is first
				_message += (_objectindentcount == 1) ? _objectindent : string.Empty;
				switch (_instanceoperation)
				{
					case 'c':
						_message += " {";
						break;
					case 'o':
						_message += " {";
						break;
					case 'i':
						_message += " (Enter values for each property:) {";
						break;
				}
				Console.Write(_message);

				int _propertycount = 0; // property counter

				// loop through each property of instance (if any)
				foreach (PropertyInfo _property in _instance.GetProperties())
				{
					_message = String.Empty;

					// get if property is system default type
					bool _ispropertysystemdefaulttype = _propertyconfiguration._ispropertysystemdefaulttype(_property.PropertyType);

					// output property order number
					_message += Environment.NewLine + _objectindent + "\t(" + ++_propertycount + ".) ";
					// output property description
					// TODO: update GUID plated here with 'original' module GUID
					_message +=  _property.Name + " [" + (_ispropertysystemdefaulttype ? _property?.PropertyType.FullName : _moduleguid) + "]";

					switch (_instanceoperation)
					{
						case 'c':
							this._outputpropertyminimalbehaviour(_property);
							break;
						case 'o':
							string _propertyvalueinstring = this._getpropertyvalue(_property, _instanceobject)?.ToString() ?? "N/A (null)";
							_message += " = " + (!_propertyvalueinstring.Equals("N/A (null)") ? _ispropertysystemdefaulttype ? _propertyvalueinstring : string.Empty : _propertyvalueinstring);
							break;
						case 'i':
							// TODO: take input from console
							Object? _propertyvalue = new Object();
							this._setpropertyvalue(_property, _instanceobject, _propertyvalue);
							break;
					}
					
					Console.Write(_message);
		
					// call a recursive here if it's (property) not a system default type
					if (_property != null && !_ispropertysystemdefaulttype)
					{
						int _objectindentcountnext = _objectindentcount;
						this._instanceactions(this._getpropertyvalue(_property, _instanceobject), _instanceoperation, _objectindentcountnext, _moduleguid);
					}
				}

				// output END
				Console.Write(Environment.NewLine + _objectindent + "}");
			}
		}

		private void _outputpropertyminimalbehaviour(PropertyInfo? _property)
		{
			if (_property != null) { }
		}

		private void _setpropertyvalue(PropertyInfo? _property, Object? _instanceobject, Object? _value)
		{
			if (_property != null && _instanceobject != null)
			{
				try
				{
					_property.SetValue(_instanceobject, _value, null);
				}
				catch (Exception _exception)
				{
					throw new Exception("EXCEPTION: " + _exception.Message);
				}
			}
		}

		private Object? _getpropertyvalue(PropertyInfo? _property, Object? _instanceobject)
		{
			Object? _value = null;
			if (_property != null && _instanceobject != null)
			{
				try
				{
					_value = _property?.GetValue(_instanceobject, null);
				}
				catch (Exception _exception)
				{
					throw new Exception("EXCEPTION: " + _exception.Message);
				}
			}
			return _value;
		}

		private void _inputmoduleconfiguration()
		{
			// TODO:
			/* Console.WriteLine("Enter a string: ");
			Console.ReadLine(); */
		}

		private void _createsamplemodule()
		{
			// creating sample module configuration
			_moduleconfiguration _samplemoduleconfiguration = new _moduleconfiguration(
				"_student", 
				new List<_propertyconfiguration>() {
					new _propertyconfiguration("Int32", "_id"),
					new _propertyconfiguration("String", "_fullname"),
					new _propertyconfiguration("String", "_address"),
					new _propertyconfiguration("Boolean", "_isdied")
				}
			);

			try
			{
				// creating sample dynamic module
				_builddynamicmodule _module_student = new _builddynamicmodule(_samplemoduleconfiguration);
				
				// creating an arbitrary instance of newly created module
				Object? _instanceobject_student = _module_student._haveaninstance();

				// adding the newly created instance to module container
				this._addinstanceobjecttomodulescontainer(_instanceobject_student);

				// assigning sample values to the properties of the newly created instance
				Type _instance_student = _instanceobject_student?.GetType() ?? typeof(Nullable);
				if (_instanceobject_student != null && _instance_student != typeof(Nullable))
				{
					this._setpropertyvalue(_instance_student.GetProperty("_id"), _instanceobject_student, 796);
					this._setpropertyvalue(_instance_student.GetProperty("_fullname"), _instanceobject_student, "Debaprasad Tapader");
					this._setpropertyvalue(_instance_student.GetProperty("_address"), _instanceobject_student, "Deoghar, JH, IN");
					this._setpropertyvalue(_instance_student.GetProperty("_isdied"), _instanceobject_student, true);
				}


				// Sampling dynamic module inside another dynamic module
				_moduleconfiguration _samplemoduleconfiguration_scholar = new _moduleconfiguration(
					"_scholar", 
					new List<_propertyconfiguration>() {
						new _propertyconfiguration(_instance_student, "_student"),
						new _propertyconfiguration("String", "_sector"),
						new _propertyconfiguration("Int32", "_year"),
						new _propertyconfiguration("Boolean", "_isactive")
					}
				);
				_builddynamicmodule _module_scholar = new _builddynamicmodule(_samplemoduleconfiguration_scholar);
				Object? _instanceobject_scholar1 = _module_scholar._haveaninstance();
				this._addinstanceobjecttomodulescontainer(_instanceobject_scholar1);
				// assigning sample values to the properties of the newly created instance
				Type _instance_scholar1 = _instanceobject_scholar1?.GetType() ?? typeof(Nullable);
				if (_instanceobject_scholar1 != null && _instance_scholar1 != typeof(Nullable) && _instanceobject_student != null)
				{
					this._setpropertyvalue(_instance_scholar1.GetProperty("_student"), _instanceobject_scholar1, _instanceobject_student);
					this._setpropertyvalue(_instance_scholar1.GetProperty("_sector"), _instanceobject_scholar1, "Matter Design");
					this._setpropertyvalue(_instance_scholar1.GetProperty("_year"), _instanceobject_scholar1, 2003);
					this._setpropertyvalue(_instance_scholar1.GetProperty("_isactive"), _instanceobject_scholar1, true);
				}
				Object? _instanceobject_scholar2 = _module_scholar._haveaninstance();
				this._addinstanceobjecttomodulescontainer(_instanceobject_scholar2);
				// assigning sample values to the properties of the newly created instance
				Type _instance_scholar2 = _instanceobject_scholar2?.GetType() ?? typeof(Nullable);
				if (_instanceobject_scholar2 != null && _instance_scholar2 != typeof(Nullable))
				{
					this._setpropertyvalue(_instance_scholar2.GetProperty("_student"), _instanceobject_scholar2, null);
					this._setpropertyvalue(_instance_scholar2.GetProperty("_sector"), _instanceobject_scholar2, "Classic Culture");
					this._setpropertyvalue(_instance_scholar2.GetProperty("_year"), _instanceobject_scholar2, 2005);
					this._setpropertyvalue(_instance_scholar2.GetProperty("_isactive"), _instanceobject_scholar2, true);
				}
			}
			catch (Exception _exception)
			{
				Console.WriteLine("EXCEPTION: " + _exception.Message);
			}
		}

		private ConsoleKeyInfo _improviseamayday()
		{
			Console.WriteLine("Press <esc> for get back to Previous Menu.");
			ConsoleKeyInfo _mayday = Console.ReadKey(true);
			Console.WriteLine(">>> Option Selected: <" + _mayday.Key.ToString() + ">");
			return _mayday;
		}

		public void _miscfunction1()
		{
			Console.WriteLine("[ List of available types of properties: ]");
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
			DateTime _datetime = DateTime.Now;
			Console.WriteLine("The time: {0:d} at {0:T}", _datetime);
			TimeZoneInfo _timezone = TimeZoneInfo.Local;
			Console.WriteLine("The time zone: {0}", _timezone.IsDaylightSavingTime(_datetime) ? _timezone.DaylightName : _timezone.StandardName);
		}
	}
}
