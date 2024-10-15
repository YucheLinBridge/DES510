using UnityEngine;
using MoonSharp.Interpreter;
using Zenject;
using System.IO;

namespace MyMath {
    public class LuaManager
    {
        //public FormulaReader Formula;

        private static Script _luains;

        public static Script LuaIns
        {
            get
            {
                if (_luains == null)
                {
                    _luains = new Script();
                }
                return _luains;
            }
        }

        public static void PropertyInput(string name, int n)
        {
            LuaIns.Globals[name] = n;
        }

        public static void PropertyInput(string name, float n)
        {
            LuaIns.Globals[name] = n;
        }

        public static float LuaCaculate(string formula)
        {
            if (formula.Equals(string.Empty))
            {
                Debug.LogError("沒有輸入公式");
                return 0;
            }

            LuaIns.Globals["result"] = 0;
            string str = "result=" + formula;
            DynValue res = LuaIns.DoString(str);
            DynValue num = LuaIns.Globals.Get("result");
            return (float)num.Number;
        }

        //下面這個是當有外部Lua文件時才會使用
        /*private void loadFunction()
        {
    #if UNITY_IOS || UNITY_ANDROID
            TextAsset ta = Resources.Load<TextAsset>("luafunction");
    #else
            var FileContent = Application.streamingAssetsPath + "/" +  "luafunction.txt" ;

            var txt = File.ReadAllText(FileContent);

            TextAsset ta = new TextAsset(txt);
    #endif
            string str = ta.text;
            //Debug.Log(str);
            DynValue res = LuaIns.DoString(@str);
        }*/

    }
}

