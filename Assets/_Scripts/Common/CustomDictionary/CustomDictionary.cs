using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

namespace NOOD
{
    [Serializable]
    struct DictionaryStruct<Tk, Tv>
    {
        public Tk key;
        public Tv value;

        public DictionaryStruct(Tk key, Tv value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [Serializable]
    public class CustomDictionary<Tk, Tv> 
    {
        [SerializeField] private DictionaryStruct<Tk, Tv>[] dicElement;

        private Dictionary<Tk, Tv> mainDic = null;

        public Dictionary<Tk, Tv> Dictionary
        {
            get
            {
                ConvertToDic();
                return mainDic;
            }
        }

        public int Count => Dictionary.Count;
        public void Add(Tk key, Tv value) => Dictionary.Add(key, value);
        public void Remove(Tk key) => Dictionary.Remove(key);
        public void Remove(Tk key, out Tv value) => Dictionary.Remove(key, out value);
        public void Clear() => Dictionary.Clear();
        public bool ContainsKey(Tk key) => Dictionary.ContainsKey(key);
        public bool ContainsValue(Tv value) => Dictionary.ContainsValue(value);
        public bool TryAdd(Tk key, Tv value) => Dictionary.TryAdd(key, value);
        public bool TryGetValue(Tk key, out Tv value) => Dictionary.TryGetValue(key, out value);
        public Tv Get(Tk key) => Dictionary[key];
        public Tv this[Tk key] => Dictionary[key];
        public Dictionary<Tk, Tv>.KeyCollection Keys => Dictionary.Keys;
        public Dictionary<Tk, Tv>.ValueCollection Values => Dictionary.Values; 
        

        private void ConvertToDic()
        {
            if(mainDic != null) return;
            mainDic = new Dictionary<Tk, Tv>();
            foreach(var dic in dicElement)
            {
                if(mainDic.Keys.Contains(dic.key)) continue;
                mainDic.Add(dic.key, dic.value);
            }
        }
    }
}
