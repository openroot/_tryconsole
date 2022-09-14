using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace _tryconsole
{
	public class _builddynamicmodule
	{
		_moduleconfiguration _moduleconfiguration { get; set; }
		TypeBuilder? _type { get; set; }

		/// <summary>
		/// Build a dynamic module
		/// </summary>
		/// <param name="_moduleconfiguration">Module configuration</param>
		public _builddynamicmodule(_moduleconfiguration _moduleconfiguration)
		{
			this._moduleconfiguration = _moduleconfiguration;
			
			this._preparestructure();
		}

		private void _preparestructure()
		{
			// Check if configuration file not null
			if (this._moduleconfiguration != null) 
			{
				// Check if name of the module not null
				if (!string.IsNullOrEmpty(this._moduleconfiguration._modulename))
				{
					bool _ispropertiescompliant = true;
					
					// Check if properties not null
					if (this._moduleconfiguration._properties != null) 
					{
						// Check if properties are compliant
						foreach (_propertyconfiguration _property in this._moduleconfiguration._properties)
						{
							if (
								String.IsNullOrEmpty(_property._name) || 
								String.IsNullOrEmpty(_property._type.ToString())
							)
							{
								_ispropertiescompliant = false;
							}
						}

						// Preparing the module
						if (_ispropertiescompliant)
						{
							// Define initial structure of module
							this._definemodule();

							// Define module constructor
							this._definemoduleconstructor();

							foreach (_propertyconfiguration _property in this._moduleconfiguration._properties)
							{
								// Define this property
								this._definemoduleproperty(_property._type, _property._name);
							}
						}
					}
					else if (!_ispropertiescompliant)
					{
						throw new Exception("Property(s) of module is/are not compliant in module configuration file.");
					}
					else
					{
						throw new Exception("Property(s) of module returned null in module configuration file.");
					}
				}
				else
				{
					throw new Exception("Name of module returned null or empty in module configuration file.");
				}
			}
			else
			{
				throw new Exception("returned null in module configuration file.");
			}
		}

		private void _definemodule()
		{
			AssemblyName _assemblyname = new AssemblyName(this._moduleconfiguration._modulename);
			AssemblyBuilder _assembly = AssemblyBuilder.DefineDynamicAssembly(_assemblyname, AssemblyBuilderAccess.Run);
			
			ModuleBuilder _module = _assembly.DefineDynamicModule("_module_" + this._moduleconfiguration._modulename);
			this._type = _module.DefineType(_assemblyname.FullName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, null);
		}

		private void _definemoduleproperty(Type _propertytype, string _propertyname)
		{
			if (this._type != null)
			{
				// basic field
				FieldBuilder _field = this._type.DefineField("_field_" + _propertyname, _propertytype, FieldAttributes.Private);
				
				MethodBuilder _get_method = this._type.DefineMethod("_get_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, _propertytype, Type.EmptyTypes);
				ILGenerator _get_immediatelanguage = _get_method.GetILGenerator();
				_get_immediatelanguage.Emit(OpCodes.Ldarg_0);
				_get_immediatelanguage.Emit(OpCodes.Ldfld, _field);
				_get_immediatelanguage.Emit(OpCodes.Ret);
				
				MethodBuilder _set_method = this._type.DefineMethod("_set_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[]{_propertytype});
				ILGenerator _set_immediatelanguage = _set_method.GetILGenerator();
				Label _modifyproperty = _set_immediatelanguage.DefineLabel();
				Label _exitset = _set_immediatelanguage.DefineLabel();
				_set_immediatelanguage.MarkLabel(_modifyproperty);
				_set_immediatelanguage.Emit(OpCodes.Ldarg_0);
				_set_immediatelanguage.Emit(OpCodes.Ldarg_1);
				_set_immediatelanguage.Emit(OpCodes.Stfld, _field);
				_set_immediatelanguage.Emit(OpCodes.Nop);
				_set_immediatelanguage.MarkLabel(_exitset);
				_set_immediatelanguage.Emit(OpCodes.Ret);

				// { get; set; } method for property on thy same field
				PropertyBuilder _property = this._type.DefineProperty(_propertyname, PropertyAttributes.HasDefault, _propertytype, null);
				_property.SetGetMethod(_get_method);
				_property.SetSetMethod(_set_method);
			}
		}

		private void _definemoduleconstructor()
		{
			this._type?.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
		}

		/// <summary>
		/// Create a new instance of the dynamic module
		/// </summary>
		/// <returns>Object (nullable)</returns>
		public Object? _haveaninstance()
		{
			Object? _instanceobject = null;
			try
			{
				// Create an instance of the TypeBuilder
				Type _type = this._type?.CreateType() ?? typeof(Nullable);
				if (_type != typeof(Nullable)) {
					_instanceobject = Activator.CreateInstance(_type);
				}
			}
			catch (Exception _exception)
			{
				throw new Exception("Could not instantiate TypeBuilder", _exception);
			}
			return _instanceobject;
		}
	}

	public class _moduleconfiguration
	{
		public string _modulename { get; set; }
		public List<_propertyconfiguration> _properties = new List<_propertyconfiguration>() {};

		/// <summary>
		/// Module configuration file
		/// </summary>
		/// <param name="_modulename">Module name</param>
		/// <param name="_properties">Module properties</param>
		public _moduleconfiguration(string _modulename, List<_propertyconfiguration> _properties)
		{
			this._modulename = _modulename;
			this._properties = _properties;
		}
	}

	public class _propertyconfiguration
	{
		public Type _type { get; set; }
		public string _name { get; set; }
		public enum _systemdefaulttype { Int16, Int32, Int64, UInt16, UInt32, UInt64, Single, Double, Char, Boolean, String };

		/// <summary>
		/// Property configuration file
		/// </summary>
		/// <param name="_type">Type of the property</param>
		/// <param name="_name">Property name</param>
		public _propertyconfiguration(Type _type, string _name)
		{
			this._type = _type;
			this._name = _name;
		}

		/// <summary>
		/// Property configuration file
		/// </summary>
		/// <param name="_type">Type of the property system default</param>
		/// <param name="_name">Property name</param>
		public _propertyconfiguration(_systemdefaulttype _type, string _name)
		{
			this._type = this._getsystemtype(_type);
			this._name = _name;
		}

		/// <summary>
		/// Property configuration file
		/// </summary>
		/// <param name="_type">Type of the property system default in plain string</param>
		/// <param name="_name">Property name</param>
		public _propertyconfiguration(string _type, string _name)
		{
			this._type = this._getsystemtypebystring(_type);
			this._name = _name;
		}

		private Type _getsystemtype(_systemdefaulttype _systemdefaulttype)
		{
			Type _type = typeof(System.Nullable);

			string _typeunformatted = "System." + _systemdefaulttype.ToString();
			_type = Type.GetType(_typeunformatted) ?? _type;

			return _type;
		}

		private Type _getsystemtypebystring(string _systemdefaulttype)
		{
			Type _type = typeof(System.Nullable);

			string _typeunformatted = "System." + _systemdefaulttype.ToString();
			_type = Type.GetType(_typeunformatted) ?? _type;

			return _type;
		}

	}

}