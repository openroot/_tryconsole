using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace _tryconsole
{
    public class _gateway
	{
		Dictionary<Guid, List<Object>> _modulescontainer = new Dictionary<Guid, List<Object>>() {};

		public _gateway() {}

		public static async Task Main(string[] args)
		{
			_gateway _gateway = new _gateway();
			
			CancellationTokenSource _cancellationtokensource = new CancellationTokenSource();
      		CancellationToken _cancellationtoken = _cancellationtokensource.Token;
			Task _executeprimarymenutask = Task.Run(() => _gateway._executeprimarymenu(), _cancellationtoken);
      		_executeprimarymenutask.Wait();
			await Task.Yield();
			_cancellationtokensource.Cancel();

			try {
				await _executeprimarymenutask;
			}
			catch (AggregateException _exception)
			{
				Console.WriteLine("Exception messages: ");
				foreach (Exception _innerexception in _exception.InnerExceptions) {
					Console.WriteLine("   {0}: {1}", _innerexception.GetType().Name, _innerexception.Message);
				}

				Console.WriteLine(Environment.NewLine + "Task status: {0}", _executeprimarymenutask.Status);       
			}
			finally {
				_cancellationtokensource.Dispose();
			}
		}

		#region TryConsole App Menu Section

		private void _executeprimarymenu()
		{
			// new menu
			ConsoleKeyInfo _maydayprimarymenu = new ConsoleKeyInfo();
			bool _isonceenteredprimarymenumenu = false;

			// loop through menu
			while (true)
			{
				// show menu at top placing
				this._showamenu("menu", true, ConsoleColor.White, ConsoleColor.DarkMagenta);
				// take keyboard input for the top placing menu ,only when menu is showing for the first time in same iteration
				_maydayprimarymenu = !_isonceenteredprimarymenumenu ?
					this._improviseamayday() : this._improviseamayday(_maydayprimarymenu);
				
				switch (_maydayprimarymenu.Key)
				{
					case ConsoleKey.C:
						// clear the Console screen
						Console.Clear();
						break;
					
					case ConsoleKey.F:
						// new menu				
						ConsoleKeyInfo _maydayfunctionmenu = new ConsoleKeyInfo();
						bool _isonceenteredalreadyfunctionmenu = false;

						// loop through menu
						while (true)
						{
							// show menu at top placing
							this._showamenu("function menu", true, ConsoleColor.White, ConsoleColor.DarkCyan);
							// take keyboard input for the top placing menu ,only when menu is showing for the first time in same iteration
							_maydayfunctionmenu = !_isonceenteredalreadyfunctionmenu ?
								this._improviseamayday() : this._improviseamayday(_maydayfunctionmenu);
				
							switch (_maydayfunctionmenu.Key)
							{
								case ConsoleKey.A:
									// new menu
									ConsoleKeyInfo _maydaymoduleoperationmenu = new ConsoleKeyInfo();
									bool _isonceenteredalreadymoduleopeartionmenu = false;

									// loop through menu
									while (true)
									{
										// show menu at top placing
										this._showamenu("module menu", true, ConsoleColor.White, ConsoleColor.DarkCyan);
										// take keyboard input for the top placing menu ,only when menu is showing for the first time in same iteration
										_maydaymoduleoperationmenu = !_isonceenteredalreadymoduleopeartionmenu ?
											this._improviseamayday() : this._improviseamayday(_maydaymoduleoperationmenu);
				
										switch (_maydaymoduleoperationmenu.Key)
										{
											case ConsoleKey.S:
												this._showfunctiondivider(true);
												this._createsamplemodule();
												this._showfunctiondivider();
												break;
											case ConsoleKey.C:
												this._showfunctiondivider(true);
												this._traversemodules('c');
												this._showfunctiondivider();
												break;
											case ConsoleKey.O:
												this._showfunctiondivider(true);
												this._traversemodules('o');
												this._showfunctiondivider();
												break;
											case ConsoleKey.M:
												this._showfunctiondivider(true);
												this._createmodulemanually();
												this._showfunctiondivider();
												break;
											case ConsoleKey.I:
												this._showfunctiondivider(true);
												this._traversemodules('i');
												this._showfunctiondivider();
												break;
											default:
												this._showwrongmayday();
												break;
										}

										// break the loop
										if (_maydaymoduleoperationmenu.Key == ConsoleKey.Escape)
										{
											Console.Clear();
											break;
										}
										else
										{
											// show menu at bottom placing
											this._showamenu("module menu", false, ConsoleColor.White, ConsoleColor.DarkCyan);
											// take keyboard input for the bottom placing menu
											_maydaymoduleoperationmenu = this._improviseamayday();

											_isonceenteredalreadymoduleopeartionmenu = true;
										}
									}
									break;
								case ConsoleKey.B:
									this._showfunctiondivider(true);
									this._currentdatetimefunction();
									this._showfunctiondivider();
									break;
								case ConsoleKey.C:
									this._showfunctiondivider(true);
									this._miscfunction1();
									this._showfunctiondivider();
									break;
								case ConsoleKey.D:
									this._showfunctiondivider(true);
									this._miscfunction2();
									this._showfunctiondivider();
									break;
								default:
									this._showwrongmayday();
									break;
							}

							// break the loop
							if (_maydayfunctionmenu.Key == ConsoleKey.Escape)
							{
								Console.Clear();
								break;
							}
							else
							{
								// show menu at bottom placing
								this._showamenu("function menu", false, ConsoleColor.White, ConsoleColor.DarkCyan);
								// take keyboard input for the bottom placing menu
								_maydayfunctionmenu = this._improviseamayday();

								_isonceenteredalreadyfunctionmenu = true;
							}
						}
						break;
					default:
						this._showwrongmayday();
						break;
				}

				// break the loop
				if (_maydayprimarymenu.Key == ConsoleKey.Escape)
				{
					Console.Clear();
					break;
				}
				else
				{
					// show menu at bottom placing
					this._showamenu("menu", false, ConsoleColor.White, ConsoleColor.DarkMagenta);
					// take keyboard input for the bottom placing menu
					_maydayprimarymenu = this._improviseamayday();

					_isonceenteredprimarymenumenu = true;
				}
			}
		}

		private Dictionary<string, List<KeyValuePair<string, string>>> _getmenubase()
		{
			Dictionary<string, List<KeyValuePair<string, string>>> _menubase = new Dictionary<string, List<KeyValuePair<string, string>>>();

			_menubase.Add("menu" , new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("esc", "for Exit App"),
				new KeyValuePair<string, string>("c", "for Clearing Screen"),
				new KeyValuePair<string, string>("f", "for Opening Function Menu **(sub-menu)")
			});
			_menubase.Add("function menu" , new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("a", "for Module Creation & Operations **(sub-menu)"),
				new KeyValuePair<string, string>("b", "for Current DateTime Function."),
				new KeyValuePair<string, string>("c", "for Misc Function 1."),
				new KeyValuePair<string, string>("d", "for Misc Function 2.")
			});
			_menubase.Add("module menu" , new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("s", "for Creating a Sample Module & It's Instance Prefilled"),
				new KeyValuePair<string, string>("c", "for Output Cultural Behavior for All Module Instances (properties)"),
				new KeyValuePair<string, string>("o", "for Output All Module Instances (properties)"),
				new KeyValuePair<string, string>("m", "for Creating Manual Module & It's Instance(s)"),
				new KeyValuePair<string, string>("i", "for New Input for All Module Instances (properties)")
			});

			return _menubase;
		}

		private void _showamenu(string _menukey, bool _iscreatefreshmenu, ConsoleColor _consoleforeground, ConsoleColor _consolebackground)
		{
			this._changeconsolecolor(_consoleforeground, _consolebackground);
			if (_iscreatefreshmenu) {
				Console.Clear();
			}
			
			this._showappthreaddata();

			Dictionary<string, List<KeyValuePair<string, string>>> _menubase = this._getmenubase();
			if (_menubase != null)
			{
				foreach (KeyValuePair<string, List<KeyValuePair<string, string>>> _menugroup in _menubase)
				{
					if (_menugroup.Key.Equals(_menukey)) {
						// output formatted string of provided menu group
						Console.Write(this._getformattedmenugroupstring(_menugroup));
					}
				}
			}
		}

		private string _getformattedmenugroupstring(KeyValuePair<string, List<KeyValuePair<string, string>>> _menugroup)
		{
			// formatted string of a menu group
			string _formattedmenugroup = string.Empty;

			char[] _menuseparatorcharacter = { '[', ']', '*' };
			int _count = 0, _maxlengthofmenudescription = 0;

			foreach (KeyValuePair<string, string> _menu in _menugroup.Value)
			{
				string _formattedmenuline = ++_count + ". Press < " + _menu.Key.ToUpper() + " > " + _menu.Value + Environment.NewLine;
				_formattedmenugroup += _formattedmenuline;
				_maxlengthofmenudescription = _maxlengthofmenudescription < _formattedmenuline.Length ? _formattedmenuline.Length : _maxlengthofmenudescription;
			}
			--_maxlengthofmenudescription;

			_formattedmenugroup = _menuseparatorcharacter[0] + " " + _menugroup.Key.ToUpper() + " " + _menuseparatorcharacter[1]
				+ new String(_menuseparatorcharacter[2], _maxlengthofmenudescription - _menugroup.Key.Length - 4)
				+ Environment.NewLine + _formattedmenugroup;
			_formattedmenugroup += new String(_menuseparatorcharacter[2], _maxlengthofmenudescription);

			return _formattedmenugroup;
		}

		private ConsoleKeyInfo _improviseamayday([Optional]ConsoleKeyInfo _maydayexistingifany)
		{
			ConsoleKeyInfo _mayday = new ConsoleKeyInfo();

			string _message = string.Empty;

			_message += Environment.NewLine + "Press < ESC > for get back to Previous Menu.";
			_message += Environment.NewLine + "********************************************";
			Console.Write(_message);

			// read keyboard input only when key any existance not passed
			_mayday = _maydayexistingifany == default(ConsoleKeyInfo) ? Console.ReadKey(true) : _maydayexistingifany;
		
			_message = Environment.NewLine + Environment.NewLine + "\\\\\\ OPTION SELECTED \\\\\\ < " + _mayday.Key.ToString() + " >";
			Console.Write(_message);
			
			return _mayday;
		}

		private void _showfunctiondivider([Optional]bool _istopdivider)
		{
			// TODO: set arrangement for color considerations for functionalities here
			//this._consolecolorchanger(ConsoleColor.White, ConsoleColor.Magenta);	
			string _message = string.Empty;
			_message += _istopdivider ? Environment.NewLine + Environment.NewLine : Environment.NewLine;
			_message += "==================================================";
			_message += !_istopdivider ? Environment.NewLine + Environment.NewLine : string.Empty;
			Console.Write(_message);
			// if (_istopdivider) {
			// 	this._consolecolorchanger(ConsoleColor.Black, ConsoleColor.Yellow);		
			// }
		}

		private void _showwrongmayday()
		{
			string _message = string.Empty;
			_message += " (!alert) Wrong Input!" + Environment.NewLine + Environment.NewLine;
			Console.Write(_message);
		}

		private void _showappthreaddata()
		{
			Console.WriteLine("TryConsole App Thread --ID " + Thread.CurrentThread.ManagedThreadId);
		}
		
		private void _changeconsolecolor(ConsoleColor _consoleforeground , ConsoleColor _consolebackground)
		{
			Console.ForegroundColor = _consoleforeground;
			Console.BackgroundColor = _consolebackground;
		}

		#endregion

		#region TryConsole App Functionalities

		#region Module Operations

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

					_message += _modulecount > 0 ? Environment.NewLine : string.Empty;

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

				string _message = string.Empty;

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
					_message = string.Empty;

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
							this._outputpropertyculturalbehaviour(_property);
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

				Console.Write(Environment.NewLine + "Sample Module Has Been Created. Congrats!");
			}
			catch (Exception _exception) {
				Console.WriteLine("EXCEPTION: " + _exception.Message);
			}
		}

		private void _setpropertyvalue(PropertyInfo? _property, Object? _instanceobject, Object? _value)
		{
			if (_property != null && _instanceobject != null)
			{
				try {
					_property.SetValue(_instanceobject, _value, null);
				}
				catch (Exception _exception) {
					throw new Exception("EXCEPTION: " + _exception.Message);
				}
			}
		}

		private Object? _getpropertyvalue(PropertyInfo? _property, Object? _instanceobject)
		{
			Object? _value = null;
			if (_property != null && _instanceobject != null)
			{
				try {
					_value = _property?.GetValue(_instanceobject, null);
				}
				catch (Exception _exception) {
					throw new Exception("EXCEPTION: " + _exception.Message);
				}
			}
			return _value;
		}

		private void _inputmoduleconfiguration()
		{
			// TODO: Have code here
		}

		public void _createmodulemanually()
		{
			// TODO: Make modules manually
		}

		private void _outputpropertyculturalbehaviour(PropertyInfo? _property)
		{
			if (_property != null)
			{
				// TODO: Might add more details on Property Culture
			}
		}

		#endregion

		#region Miscellaneous Functions

		public void _showappabout()
		{
			string _message = string.Empty;

			// TODO: prepare the app about

			Console.Write(_message);
		}

		public void _currentdatetimefunction()
		{
			Console.Write(Environment.NewLine);
			DateTime _datetime = DateTime.Now;
			Console.WriteLine("The time: {0:d} at {0:T}", _datetime);
			TimeZoneInfo _timezone = TimeZoneInfo.Local;
			Console.Write("The time zone: {0}", _timezone.IsDaylightSavingTime(_datetime) ? _timezone.DaylightName : _timezone.StandardName);
		}

		public void _miscfunction1()
		{
			string _message = Environment.NewLine;
			_message += "[ List of available types of properties: ]" + Environment.NewLine;
			_message += "int : " + typeof(int).Name + Environment.NewLine;
			_message += "float : " + typeof(float).Name + Environment.NewLine;
			_message += "double : " + typeof(double).Name + Environment.NewLine;
			_message += "char : " + typeof(char).Name + Environment.NewLine;
			_message += "bool : " + typeof(bool).Name + Environment.NewLine;
			_message += "string : " + typeof(string).Name + Environment.NewLine;
			_message += "Int16 : " + typeof(Int16).Name + Environment.NewLine;
			_message += "Int32 : " + typeof(Int32).Name + Environment.NewLine;
			_message += "Int64 : " + typeof(Int64).Name + Environment.NewLine;
			_message += "String : " + typeof(String).Name;
			Console.Write(_message);
		}

		public void _miscfunction2()
		{
			// TODO: update menu string into List<>
			// this._consolecolorchanger(ConsoleColor.White, ConsoleColor.DarkMagenta);
			// if (_iscreatefreshmenu) {
			// 	Console.Clear();
			// }


			string _message = string.Empty;
			_message += "[ MODULE MENU ]******************************************************************" + Environment.NewLine;
			_message += "1. Press < S > for Creating a Sample Module & It's Instance Prefilled" + Environment.NewLine;
			_message += "2. Press < C > for Output Cultural Behavior for All Module Instances (properties)" + Environment.NewLine;
			_message += "3. Press < O > for Output All Module Instances (properties)" + Environment.NewLine;
			_message += "4. Press < M > for Creating Manual Module & It's Instance(s)" + Environment.NewLine;
			_message += "5. Press < I > for New Input for All Module Instances (properties)" + Environment.NewLine;
			_message += "*********************************************************************************";


			Console.Write(Environment.NewLine + Environment.NewLine + "^^Formatting Menu" + Environment.NewLine + Environment.NewLine +_message);
		}

		#endregion

		#endregion
	}
}
