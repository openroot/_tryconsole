using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace _tryconsole
{
	public class _constructclass
	{
		Object? _constructedclass;
		_classconfiguration _classconfiguration;
		AssemblyName _assemblyname;
		TypeBuilder? _typebuilder;

		/// <summary>
		/// Construct a class
		/// </summary>
		/// <param name="_classconfiguration">Configuration for class</param>
		public _constructclass(_classconfiguration _classconfiguration)
		{
			this._constructedclass = new Object();
			this._classconfiguration = _classconfiguration;
			this._assemblyname = new AssemblyName(this._classconfiguration._classname);

			this._construct();
		}

		private void _construct()
		{
			// Check if configuration file not null
			if (this._classconfiguration != null) 
			{
				// Check if name of the class not null
				if (!string.IsNullOrEmpty(this._classconfiguration._classname))
				{
					bool _isfieldcompliant = true;
					
					// Check if fields not null
					if (this._classconfiguration._properties != null) 
					{
						// Check if fields are compliant
						foreach (_propertyconfiguration _property in this._classconfiguration._properties)
						{
							if (
								String.IsNullOrEmpty(_property._name) || 
								String.IsNullOrEmpty(_property._type)
							)
							{
								_isfieldcompliant = false;
							}
						}

						// Prepare the class here
						if (_isfieldcompliant)
						{
							// Define initial structure of class
							this._defineclass();

							if (this._typebuilder != null)
							{
								foreach (_propertyconfiguration _property in this._classconfiguration._properties)
								{
									// Define this property
									this._defineproperty(this._gettype(_property._type), _property._name);
								}
								try
								{
									// Instantiate the type provided by TypeBuilder
									_constructedclass = Activator.CreateInstance(this._typebuilder.CreateType());
								}
								catch(Exception _exception)
								{
									throw new Exception("Could not instantiate type provided by TypeBuilder", _exception);
								}
							}
						}
					}
					else if (!_isfieldcompliant)
					{
						throw new Exception("Field(s) of class is/are not compliant in class configuration file.");
					}
					else
					{
						throw new Exception("Field(s) of class returned null in class configuration file.");
					}
				}
				else
				{
					throw new Exception("Name of class returned null or empty in class configuration file.");
				}
			}
			else
			{
				throw new Exception("returned null in class configuration file.");
			}
		}

		private void _defineclass()
		{
			AssemblyBuilder _assemblybuilder = AssemblyBuilder.DefineDynamicAssembly(this._assemblyname, AssemblyBuilderAccess.Run);
			ModuleBuilder _modulebuilder = _assemblybuilder.DefineDynamicModule("_module");
			this._typebuilder = _modulebuilder.DefineType(this._assemblyname.FullName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, null);
			
			// Define class constructor
			this._defineconstructor();
		}

		private void _defineconstructor()
		{
			this._typebuilder?.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
		}

		private void _defineproperty(Type _propertytype, string _propertyname)
		{
			if (this._typebuilder != null)
			{
				FieldBuilder _fieldbuilder = this._typebuilder.DefineField("_" + _propertyname, _propertytype, FieldAttributes.Private);
				PropertyBuilder _propertybuilder = this._typebuilder.DefineProperty(_propertyname, PropertyAttributes.HasDefault, _propertytype, null);
				MethodBuilder _getpropertymethodbuilder = this._typebuilder.DefineMethod("get_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, _propertytype, Type.EmptyTypes);
				ILGenerator _getilgenerator = _getpropertymethodbuilder.GetILGenerator();
				_getilgenerator.Emit(OpCodes.Ldarg_0);
				_getilgenerator.Emit(OpCodes.Ldfld, _fieldbuilder);
				_getilgenerator.Emit(OpCodes.Ret);
				MethodBuilder _setpropertymethodbuilder = this._typebuilder.DefineMethod("set_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[]{_propertytype});
				ILGenerator _setilgenerator = _setpropertymethodbuilder.GetILGenerator();
				Label _modifyproperty = _setilgenerator.DefineLabel();
				Label _exitset = _setilgenerator.DefineLabel();
				_setilgenerator.MarkLabel(_modifyproperty);
				_setilgenerator.Emit(OpCodes.Ldarg_0);
				_setilgenerator.Emit(OpCodes.Ldarg_1);
				_setilgenerator.Emit(OpCodes.Stfld, _fieldbuilder);
				_setilgenerator.Emit(OpCodes.Nop);
				_setilgenerator.MarkLabel(_exitset);
				_setilgenerator.Emit(OpCodes.Ret);
				_propertybuilder.SetGetMethod(_getpropertymethodbuilder);
				_propertybuilder.SetSetMethod(_setpropertymethodbuilder);
			}
		}

		public Object? _getconstructedclass()
		{
			return this._constructedclass;
		}

		public Type _gettype(string _typeinflatstring)
		{
			Type _type = typeof(Nullable);
			switch (_typeinflatstring)
			{
				case "int":
					_type = typeof(int);
					break;
				case "string":
					_type = typeof(string);
					break;
			}
			return _type;
		}
	}

	public class _classconfiguration
	{
		public string _classname = string.Empty;
		public List<_propertyconfiguration> _properties = new List<_propertyconfiguration>();

		/// <summary>
		/// Configuration file for class
		/// </summary>
		/// <param name="_classname">Name of class</param>
		/// <param name="_properties">Properties of class</param>
		public _classconfiguration(string _classname, List<_propertyconfiguration> _properties)
		{
			this._classname = _classname;
			this._properties = _properties;
		}
	}

	public class _propertyconfiguration
	{
		public string _type = string.Empty;
		public string _name = string.Empty;

		/// <summary>
		/// Configuration file for a property for class
		/// </summary>
		/// <param name="_type">Property type in flat string</param>
		/// <param name="_name">Name of property</param>
		public _propertyconfiguration(string _type, string _name)
		{
			this._type = _type;
			this._name = _name;
		}
	}

}