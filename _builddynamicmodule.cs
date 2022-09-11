using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace _tryconsole
{
	public class _builddynamicmodule
	{
		_moduleconfiguration _moduleconfiguration { get; set; }
		AssemblyName _assemblyname { get; set; }
		TypeBuilder? _typebuilder { get; set; }

		//public const enum _predefinedpropertytypes;

		/// <summary>
		/// Build a dynamic module
		/// </summary>
		/// <param name="_moduleconfiguration">Module configuration</param>
		public _builddynamicmodule(_moduleconfiguration _moduleconfiguration)
		{
			this._moduleconfiguration = _moduleconfiguration;
			this._assemblyname = new AssemblyName(this._moduleconfiguration._modulename);
			
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
								String.IsNullOrEmpty(_property._type)
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

							foreach (_propertyconfiguration _property in this._moduleconfiguration._properties)
							{
								// Define this property
								this._definemoduleproperty(this._getpropertytypefromstring(_property._type), _property._name);
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
			AssemblyBuilder _assemblybuilder = AssemblyBuilder.DefineDynamicAssembly(this._assemblyname, AssemblyBuilderAccess.Run);
			ModuleBuilder _module = _assemblybuilder.DefineDynamicModule("_module_" + this._moduleconfiguration._modulename);
			this._typebuilder = _module.DefineType(this._assemblyname.FullName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, null);
			
			// Define module constructor
			this._definemoduleconstructor();
		}

		private void _definemoduleproperty(Type _propertytype, string _propertyname)
		{
			if (this._typebuilder != null)
			{
				FieldBuilder _field = this._typebuilder.DefineField(_propertyname, _propertytype, FieldAttributes.Private);
				
				PropertyBuilder _property = this._typebuilder.DefineProperty(_propertyname, PropertyAttributes.HasDefault, _propertytype, null);
				
				MethodBuilder _getpropertymethodbuilder = this._typebuilder.DefineMethod("_get_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, _propertytype, Type.EmptyTypes);
				ILGenerator _getilgenerator = _getpropertymethodbuilder.GetILGenerator();
				_getilgenerator.Emit(OpCodes.Ldarg_0);
				_getilgenerator.Emit(OpCodes.Ldfld, _field);
				_getilgenerator.Emit(OpCodes.Ret);
				
				MethodBuilder _setpropertymethodbuilder = this._typebuilder.DefineMethod("_set_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[]{_propertytype});
				ILGenerator _setilgenerator = _setpropertymethodbuilder.GetILGenerator();
				Label _modifyproperty = _setilgenerator.DefineLabel();
				Label _exitset = _setilgenerator.DefineLabel();
				_setilgenerator.MarkLabel(_modifyproperty);
				_setilgenerator.Emit(OpCodes.Ldarg_0);
				_setilgenerator.Emit(OpCodes.Ldarg_1);
				_setilgenerator.Emit(OpCodes.Stfld, _field);
				_setilgenerator.Emit(OpCodes.Nop);
				_setilgenerator.MarkLabel(_exitset);
				_setilgenerator.Emit(OpCodes.Ret);

				_property.SetGetMethod(_getpropertymethodbuilder);
				_property.SetSetMethod(_setpropertymethodbuilder);
			}
		}

		private void _definemoduleconstructor()
		{
			this._typebuilder?.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
		}

		/// <summary>
		/// Get strongly-typed Type from type in string format 
		/// </summary>
		/// <param name="_propertytypeinstringformat"></param>
		/// <returns>Type</returns>
		public Type _getpropertytypefromstring(string _propertytypeinstringformat)
		{
			Type _type = typeof(Nullable);
			switch (_propertytypeinstringformat)
			{
				case "int":
					_type = typeof(int);
					break;
				case "string":
					_type = typeof(string);
					break;
				case "bool": case "Boolean":
					_type = typeof(bool);
					break;
			}
			return _type;
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
				_instanceobject = Activator.CreateInstance(this._typebuilder?.CreateType());
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
		public string _type { get; set; }
		public string _name { get; set; }

		/// <summary>
		/// Property configuration file
		/// </summary>
		/// <param name="_type">Type of the property in string format</param>
		/// <param name="_name">Property name</param>
		public _propertyconfiguration(string _type, string _name)
		{
			this._type = _type;
			this._name = _name;
		}
	}

}