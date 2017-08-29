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
    public class SystemReflectionMethodInfoWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(System.Reflection.MethodInfo), L, translator, 0, 4, 7, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBaseDefinition", _m_GetBaseDefinition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGenericMethodDefinition", _m_GetGenericMethodDefinition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MakeGenericMethod", _m_MakeGenericMethod);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGenericArguments", _m_GetGenericArguments);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "MemberType", _g_get_MemberType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReturnType", _g_get_ReturnType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReturnTypeCustomAttributes", _g_get_ReturnTypeCustomAttributes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsGenericMethod", _g_get_IsGenericMethod);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsGenericMethodDefinition", _g_get_IsGenericMethodDefinition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ContainsGenericParameters", _g_get_ContainsGenericParameters);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReturnParameter", _g_get_ReturnParameter);
            
			
			Utils.EndObjectRegister(typeof(System.Reflection.MethodInfo), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(System.Reflection.MethodInfo), L, __CreateInstance, 1, 0, 0);
			
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(System.Reflection.MethodInfo));
			
			
			Utils.EndClassRegister(typeof(System.Reflection.MethodInfo), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.Reflection.MethodInfo does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBaseDefinition(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        System.Reflection.MethodInfo __cl_gen_ret = __cl_gen_to_be_invoked.GetBaseDefinition(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGenericMethodDefinition(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        System.Reflection.MethodInfo __cl_gen_ret = __cl_gen_to_be_invoked.GetGenericMethodDefinition(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MakeGenericMethod(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    System.Type[] typeArguments = translator.GetParams<System.Type>(L, 2);
                    
                        System.Reflection.MethodInfo __cl_gen_ret = __cl_gen_to_be_invoked.MakeGenericMethod( typeArguments );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGenericArguments(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
            
            
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
        static int _g_get_MemberType(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.MemberType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReturnType(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ReturnType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReturnTypeCustomAttributes(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ReturnTypeCustomAttributes);
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
			
                System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsGenericMethod);
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
			
                System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsGenericMethodDefinition);
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
			
                System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.ContainsGenericParameters);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReturnParameter(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                System.Reflection.MethodInfo __cl_gen_to_be_invoked = (System.Reflection.MethodInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ReturnParameter);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
