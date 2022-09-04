
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace _tryconsole
{
	public class _constructclass
	{
		AssemblyName _assemblyname;
		TypeBuilder? _typebuilder;

		public _constructclass(string _classname)
		{
			this._assemblyname = new AssemblyName(_classname);
		}

		private void _defineclass()
		{
			AssemblyBuilder _assemblybuilder = AssemblyBuilder.DefineDynamicAssembly(this._assemblyname, AssemblyBuilderAccess.Run);
			ModuleBuilder _modulebuilder = _assemblybuilder.DefineDynamicModule("_module");
			this._typebuilder = _modulebuilder.DefineType(this._assemblyname.FullName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, null);
			
			this._defineconstructor();
		}

		private void _defineconstructor()
		{
			this._typebuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
		}

		private void _defineproperty(string _propertyname, Type _propertytype)
		{
			FieldBuilder fieldBuilder = this._typebuilder.DefineField("_" + _propertyname, _propertytype, FieldAttributes.Private);
			PropertyBuilder propertyBuilder = this._typebuilder.DefineProperty(_propertyname, PropertyAttributes.HasDefault, _propertytype, null);
			MethodBuilder getPropMthdBldr = this._typebuilder.DefineMethod("get_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, _propertytype, Type.EmptyTypes);
			ILGenerator getIl = getPropMthdBldr.GetILGenerator();
			getIl.Emit(OpCodes.Ldarg_0);
			getIl.Emit(OpCodes.Ldfld, fieldBuilder);
			getIl.Emit(OpCodes.Ret);
			MethodBuilder setPropMthdBldr = this._typebuilder.DefineMethod("set_" + _propertyname, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[]{_propertytype});
			ILGenerator setIl = setPropMthdBldr.GetILGenerator();
			Label modifyProperty = setIl.DefineLabel();
			Label exitSet = setIl.DefineLabel();
			setIl.MarkLabel(modifyProperty);
			setIl.Emit(OpCodes.Ldarg_0);
			setIl.Emit(OpCodes.Ldarg_1);
			setIl.Emit(OpCodes.Stfld, fieldBuilder);
			setIl.Emit(OpCodes.Nop);
			setIl.MarkLabel(exitSet);
			setIl.Emit(OpCodes.Ret);
			propertyBuilder.SetGetMethod(getPropMthdBldr);
			propertyBuilder.SetSetMethod(setPropMthdBldr);
		}
		
		public object _config(string[] _propertynames, Type[] _propertytypes)
		{
			if (_propertynames.Length != _propertytypes.Length)
			{
				Console.WriteLine("The number of property names should match their corresopnding types number");
			}

			this._defineclass();
			for (int ind = 0; ind < _propertynames.Count(); ind++)
				_defineproperty(_propertynames[ind], _propertytypes[ind]);
			Type type = this._typebuilder.CreateType();
			return Activator.CreateInstance(type);
		}
	}
}