#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class SystemReflectionMethodBaseWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(System.Reflection.MethodBase), L, translator, 0, 5, 19, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMethodImplementationFlags", _m_GetMethodImplementationFlags);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetParameters", _m_GetParameters);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Invoke", _m_Invoke);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGenericArguments", _m_GetGenericArguments);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMethodBody", _m_GetMethodBody);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "MethodHandle", _g_get_MethodHandle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Attributes", _g_get_Attributes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CallingConvention", _g_get_CallingConvention);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsPublic", _g_get_IsPublic);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsPrivate", _g_get_IsPrivate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsFamily", _g_get_IsFamily);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsAssembly", _g_get_IsAssembly);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsFamilyAndAssembly", _g_get_IsFamilyAndAssembly);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsFamilyOrAssembly", _g_get_IsFamilyOrAssembly);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsStatic", _g_get_IsStatic);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsFinal", _g_get_IsFinal);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsVirtual", _g_get_IsVirtual);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsHideBySig", _g_get_IsHideBySig);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsAbstract", _g_get_IsAbstract);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSpecialName", _g_get_IsSpecialName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsConstructor", _g_get_IsConstructor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ContainsGenericParameters", _g_get_ContainsGenericParameters);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsGenericMethodDefinition", _g_get_IsGenericMethodDefinition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsGenericMethod", _g_get_IsGenericMethod);
            
			
			Utils.EndObjectRegister(typeof(System.Reflection.MethodBase), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(System.Reflection.MethodBase), L, __CreateInstance, 3, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCurrentMethod", _m_GetCurrentMethod_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMethodFromHandle", _m_GetMethodFromHandle_xlua_st_);
            
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(System.Reflection.MethodBase));
			
			
			Utils.EndClassRegister(typeof(System.Reflection.MethodBase), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.Reflection.MethodBase does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurrentMethod_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        System.Reflection.MethodBase __cl_gen_ret = System.Reflection.MethodBase.GetCurrentMethod(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMethodFromHandle_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1&& translator.Assignable<System.RuntimeMethodHandle>(L, 1)) 
                {
                    System.RuntimeMethodHandle handle;translator.Get(L, 1, out handle);
                    
                        System.Reflection.MethodBase __cl_gen_ret = System.Reflection.MethodBase.GetMethodFromHandle( handle );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.RuntimeMethodHandle>(L, 1)&& translator.Assignable<System.RuntimeTypeHandle>(L, 2)) 
                {
                    System.RuntimeMethodHandle handle;translator.Get(L, 1, out handle);
                    System.RuntimeTypeHandle declaringType;translator.Get(L, 2, out declaringType);
                    
                        System.Reflection.MethodBase __cl_gen_ret = System.Reflection.MethodBase.GetMethodFromHandle( handle, declaringType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Reflection.MethodBase.GetMethodFromHandle!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMethodImplementationFlags(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        System.Reflection.MethodImplAttributes __cl_gen_ret = __cl_gen_to_be_invoked.GetMethodImplementationFlags(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetParameters(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        System.Reflection.ParameterInfo[] __cl_gen_ret = __cl_gen_to_be_invoked.GetParameters(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Invoke(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 3&& translator.Assignable<object>(L, 2)&& translator.Assignable<object[]>(L, 3)) 
                {
                    object obj = translator.GetObject(L, 2, typeof(object));
                    object[] parameters = (object[])translator.GetObject(L, 3, typeof(object[]));
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.Invoke( obj, parameters );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 6&& translator.Assignable<object>(L, 2)&& translator.Assignable<System.Reflection.BindingFlags>(L, 3)&& translator.Assignable<System.Reflection.Binder>(L, 4)&& translator.Assignable<object[]>(L, 5)&& translator.Assignable<System.Globalization.CultureInfo>(L, 6)) 
                {
                    object obj = translator.GetObject(L, 2, typeof(object));
                    System.Reflection.BindingFlags invokeAttr;translator.Get(L, 3, out invokeAttr);
                    System.Reflection.Binder binder = (System.Reflection.Binder)translator.GetObject(L, 4, typeof(System.Reflection.Binder));
                    object[] parameters = (object[])translator.GetObject(L, 5, typeof(object[]));
                    System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)translator.GetObject(L, 6, typeof(System.Globalization.CultureInfo));
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.Invoke( obj, invokeAttr, binder, parameters, culture );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Reflection.MethodBase.Invoke!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGenericArguments(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        System.Type[] __cl_gen_ret = __cl_gen_to_be_invoked.GetGenericArguments(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMethodBody(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        System.Reflection.MethodBody __cl_gen_ret = __cl_gen_to_be_invoked.GetMethodBody(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MethodHandle(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.MethodHandle);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Attributes(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Attributes);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CallingConvention(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.CallingConvention);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsPublic(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsPublic);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsPrivate(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsPrivate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsFamily(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsFamily);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsAssembly(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsAssembly);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsFamilyAndAssembly(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsFamilyAndAssembly);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsFamilyOrAssembly(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsFamilyOrAssembly);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsStatic(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsStatic);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsFinal(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsFinal);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsVirtual(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsVirtual);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsHideBySig(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsHideBySig);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsAbstract(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsAbstract);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSpecialName(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsSpecialName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsConstructor(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsConstructor);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ContainsGenericParameters(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.ContainsGenericParameters);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsGenericMethodDefinition(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsGenericMethodDefinition);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsGenericMethod(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodBase __cl_gen_to_be_invoked = (System.Reflection.MethodBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsGenericMethod);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
