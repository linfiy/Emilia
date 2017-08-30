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
    public class UniRxObservableWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(UniRx.Observable), L, translator, 0, 0, 0, 0);
			
			
			
			
			
			Utils.EndObjectRegister(typeof(UniRx.Observable), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(UniRx.Observable), L, __CreateInstance, 24, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "WhenAll", _m_WhenAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Return", _m_Return_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReturnUnit", _m_ReturnUnit_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Range", _m_Range_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Start", _m_Start_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToAsync", _m_ToAsync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromEvent", _m_FromEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromAsyncPattern", _m_FromAsyncPattern_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Interval", _m_Interval_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Timer", _m_Timer_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromCoroutine", _m_FromCoroutine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromMicroCoroutine", _m_FromMicroCoroutine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EveryUpdate", _m_EveryUpdate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EveryFixedUpdate", _m_EveryFixedUpdate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EveryEndOfFrame", _m_EveryEndOfFrame_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EveryGameObjectUpdate", _m_EveryGameObjectUpdate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EveryLateUpdate", _m_EveryLateUpdate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NextFrame", _m_NextFrame_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IntervalFrame", _m_IntervalFrame_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TimerFrame", _m_TimerFrame_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EveryApplicationPause", _m_EveryApplicationPause_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EveryApplicationFocus", _m_EveryApplicationFocus_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OnceApplicationQuit", _m_OnceApplicationQuit_xlua_st_);
            
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(UniRx.Observable));
			
			
			Utils.EndClassRegister(typeof(UniRx.Observable), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "UniRx.Observable does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WhenAll_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    UniRx.IObservable<UniRx.Unit>[] sources = translator.GetParams<UniRx.IObservable<UniRx.Unit>>(L, 1);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.WhenAll( sources );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Return_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)) 
                {
                    bool value = LuaAPI.lua_toboolean(L, 1);
                    
                        UniRx.IObservable<bool> __cl_gen_ret = UniRx.Observable.Return( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& translator.Assignable<UniRx.Unit>(L, 1)) 
                {
                    UniRx.Unit value;translator.Get(L, 1, out value);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.Return( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.Return!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReturnUnit_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.ReturnUnit(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Range_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int start = LuaAPI.xlua_tointeger(L, 1);
                    int count = LuaAPI.xlua_tointeger(L, 2);
                    
                        UniRx.IObservable<int> __cl_gen_ret = UniRx.Observable.Range( start, count );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniRx.IScheduler>(L, 3)) 
                {
                    int start = LuaAPI.xlua_tointeger(L, 1);
                    int count = LuaAPI.xlua_tointeger(L, 2);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 3, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<int> __cl_gen_ret = UniRx.Observable.Range( start, count, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.Range!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Start_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1&& translator.Assignable<System.Action>(L, 1)) 
                {
                    System.Action action = translator.GetDelegate<System.Action>(L, 1);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.Start( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Action>(L, 1)&& translator.Assignable<System.TimeSpan>(L, 2)) 
                {
                    System.Action action = translator.GetDelegate<System.Action>(L, 1);
                    System.TimeSpan timeSpan;translator.Get(L, 2, out timeSpan);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.Start( action, timeSpan );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Action>(L, 1)&& translator.Assignable<UniRx.IScheduler>(L, 2)) 
                {
                    System.Action action = translator.GetDelegate<System.Action>(L, 1);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 2, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.Start( action, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Action>(L, 1)&& translator.Assignable<System.TimeSpan>(L, 2)&& translator.Assignable<UniRx.IScheduler>(L, 3)) 
                {
                    System.Action action = translator.GetDelegate<System.Action>(L, 1);
                    System.TimeSpan timeSpan;translator.Get(L, 2, out timeSpan);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 3, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.Start( action, timeSpan, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.Start!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToAsync_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1&& translator.Assignable<System.Action>(L, 1)) 
                {
                    System.Action action = translator.GetDelegate<System.Action>(L, 1);
                    
                        System.Func<UniRx.IObservable<UniRx.Unit>> __cl_gen_ret = UniRx.Observable.ToAsync( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Action>(L, 1)&& translator.Assignable<UniRx.IScheduler>(L, 2)) 
                {
                    System.Action action = translator.GetDelegate<System.Action>(L, 1);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 2, typeof(UniRx.IScheduler));
                    
                        System.Func<UniRx.IObservable<UniRx.Unit>> __cl_gen_ret = UniRx.Observable.ToAsync( action, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.ToAsync!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromEvent_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    System.Action<System.Action> addHandler = translator.GetDelegate<System.Action<System.Action>>(L, 1);
                    System.Action<System.Action> removeHandler = translator.GetDelegate<System.Action<System.Action>>(L, 2);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromEvent( addHandler, removeHandler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromAsyncPattern_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    System.Func<System.AsyncCallback, object, System.IAsyncResult> begin = translator.GetDelegate<System.Func<System.AsyncCallback, object, System.IAsyncResult>>(L, 1);
                    System.Action<System.IAsyncResult> end = translator.GetDelegate<System.Action<System.IAsyncResult>>(L, 2);
                    
                        System.Func<UniRx.IObservable<UniRx.Unit>> __cl_gen_ret = UniRx.Observable.FromAsyncPattern( begin, end );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Interval_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1&& translator.Assignable<System.TimeSpan>(L, 1)) 
                {
                    System.TimeSpan period;translator.Get(L, 1, out period);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Interval( period );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.TimeSpan>(L, 1)&& translator.Assignable<UniRx.IScheduler>(L, 2)) 
                {
                    System.TimeSpan period;translator.Get(L, 1, out period);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 2, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Interval( period, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.Interval!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Timer_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1&& translator.Assignable<System.TimeSpan>(L, 1)) 
                {
                    System.TimeSpan dueTime;translator.Get(L, 1, out dueTime);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& translator.Assignable<System.DateTimeOffset>(L, 1)) 
                {
                    System.DateTimeOffset dueTime;translator.Get(L, 1, out dueTime);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.TimeSpan>(L, 1)&& translator.Assignable<System.TimeSpan>(L, 2)) 
                {
                    System.TimeSpan dueTime;translator.Get(L, 1, out dueTime);
                    System.TimeSpan period;translator.Get(L, 2, out period);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime, period );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.DateTimeOffset>(L, 1)&& translator.Assignable<System.TimeSpan>(L, 2)) 
                {
                    System.DateTimeOffset dueTime;translator.Get(L, 1, out dueTime);
                    System.TimeSpan period;translator.Get(L, 2, out period);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime, period );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.TimeSpan>(L, 1)&& translator.Assignable<UniRx.IScheduler>(L, 2)) 
                {
                    System.TimeSpan dueTime;translator.Get(L, 1, out dueTime);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 2, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.DateTimeOffset>(L, 1)&& translator.Assignable<UniRx.IScheduler>(L, 2)) 
                {
                    System.DateTimeOffset dueTime;translator.Get(L, 1, out dueTime);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 2, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.TimeSpan>(L, 1)&& translator.Assignable<System.TimeSpan>(L, 2)&& translator.Assignable<UniRx.IScheduler>(L, 3)) 
                {
                    System.TimeSpan dueTime;translator.Get(L, 1, out dueTime);
                    System.TimeSpan period;translator.Get(L, 2, out period);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 3, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime, period, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.DateTimeOffset>(L, 1)&& translator.Assignable<System.TimeSpan>(L, 2)&& translator.Assignable<UniRx.IScheduler>(L, 3)) 
                {
                    System.DateTimeOffset dueTime;translator.Get(L, 1, out dueTime);
                    System.TimeSpan period;translator.Get(L, 2, out period);
                    UniRx.IScheduler scheduler = (UniRx.IScheduler)translator.GetObject(L, 3, typeof(UniRx.IScheduler));
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.Timer( dueTime, period, scheduler );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.Timer!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromCoroutine_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 2&& translator.Assignable<System.Func<System.Collections.IEnumerator>>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    System.Func<System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<System.Collections.IEnumerator>>(L, 1);
                    bool publishEveryYield = LuaAPI.lua_toboolean(L, 2);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromCoroutine( coroutine, publishEveryYield );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& translator.Assignable<System.Func<System.Collections.IEnumerator>>(L, 1)) 
                {
                    System.Func<System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<System.Collections.IEnumerator>>(L, 1);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromCoroutine( coroutine );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    System.Func<UniRx.CancellationToken, System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1);
                    bool publishEveryYield = LuaAPI.lua_toboolean(L, 2);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromCoroutine( coroutine, publishEveryYield );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& translator.Assignable<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1)) 
                {
                    System.Func<UniRx.CancellationToken, System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromCoroutine( coroutine );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.FromCoroutine!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromMicroCoroutine_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 3&& translator.Assignable<System.Func<System.Collections.IEnumerator>>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniRx.FrameCountType>(L, 3)) 
                {
                    System.Func<System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<System.Collections.IEnumerator>>(L, 1);
                    bool publishEveryYield = LuaAPI.lua_toboolean(L, 2);
                    UniRx.FrameCountType frameCountType;translator.Get(L, 3, out frameCountType);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromMicroCoroutine( coroutine, publishEveryYield, frameCountType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Func<System.Collections.IEnumerator>>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    System.Func<System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<System.Collections.IEnumerator>>(L, 1);
                    bool publishEveryYield = LuaAPI.lua_toboolean(L, 2);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromMicroCoroutine( coroutine, publishEveryYield );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& translator.Assignable<System.Func<System.Collections.IEnumerator>>(L, 1)) 
                {
                    System.Func<System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<System.Collections.IEnumerator>>(L, 1);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromMicroCoroutine( coroutine );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniRx.FrameCountType>(L, 3)) 
                {
                    System.Func<UniRx.CancellationToken, System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1);
                    bool publishEveryYield = LuaAPI.lua_toboolean(L, 2);
                    UniRx.FrameCountType frameCountType;translator.Get(L, 3, out frameCountType);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromMicroCoroutine( coroutine, publishEveryYield, frameCountType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    System.Func<UniRx.CancellationToken, System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1);
                    bool publishEveryYield = LuaAPI.lua_toboolean(L, 2);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromMicroCoroutine( coroutine, publishEveryYield );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& translator.Assignable<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1)) 
                {
                    System.Func<UniRx.CancellationToken, System.Collections.IEnumerator> coroutine = translator.GetDelegate<System.Func<UniRx.CancellationToken, System.Collections.IEnumerator>>(L, 1);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.FromMicroCoroutine( coroutine );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.FromMicroCoroutine!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EveryUpdate_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.EveryUpdate(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EveryFixedUpdate_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.EveryFixedUpdate(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EveryEndOfFrame_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.EveryEndOfFrame(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EveryGameObjectUpdate_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.EveryGameObjectUpdate(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EveryLateUpdate_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.EveryLateUpdate(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NextFrame_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1&& translator.Assignable<UniRx.FrameCountType>(L, 1)) 
                {
                    UniRx.FrameCountType frameCountType;translator.Get(L, 1, out frameCountType);
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.NextFrame( frameCountType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 0) 
                {
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.NextFrame(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.NextFrame!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IntervalFrame_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<UniRx.FrameCountType>(L, 2)) 
                {
                    int intervalFrameCount = LuaAPI.xlua_tointeger(L, 1);
                    UniRx.FrameCountType frameCountType;translator.Get(L, 2, out frameCountType);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.IntervalFrame( intervalFrameCount, frameCountType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    int intervalFrameCount = LuaAPI.xlua_tointeger(L, 1);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.IntervalFrame( intervalFrameCount );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.IntervalFrame!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TimerFrame_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<UniRx.FrameCountType>(L, 2)) 
                {
                    int dueTimeFrameCount = LuaAPI.xlua_tointeger(L, 1);
                    UniRx.FrameCountType frameCountType;translator.Get(L, 2, out frameCountType);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.TimerFrame( dueTimeFrameCount, frameCountType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    int dueTimeFrameCount = LuaAPI.xlua_tointeger(L, 1);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.TimerFrame( dueTimeFrameCount );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniRx.FrameCountType>(L, 3)) 
                {
                    int dueTimeFrameCount = LuaAPI.xlua_tointeger(L, 1);
                    int periodFrameCount = LuaAPI.xlua_tointeger(L, 2);
                    UniRx.FrameCountType frameCountType;translator.Get(L, 3, out frameCountType);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.TimerFrame( dueTimeFrameCount, periodFrameCount, frameCountType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int dueTimeFrameCount = LuaAPI.xlua_tointeger(L, 1);
                    int periodFrameCount = LuaAPI.xlua_tointeger(L, 2);
                    
                        UniRx.IObservable<long> __cl_gen_ret = UniRx.Observable.TimerFrame( dueTimeFrameCount, periodFrameCount );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniRx.Observable.TimerFrame!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EveryApplicationPause_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<bool> __cl_gen_ret = UniRx.Observable.EveryApplicationPause(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EveryApplicationFocus_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<bool> __cl_gen_ret = UniRx.Observable.EveryApplicationFocus(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnceApplicationQuit_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        UniRx.IObservable<UniRx.Unit> __cl_gen_ret = UniRx.Observable.OnceApplicationQuit(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
