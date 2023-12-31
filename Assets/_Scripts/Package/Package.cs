using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

namespace Game
{
    public static class PackageMaster
    {
        private static TextAsset _textAsset;
        private static string[] splitNewRow = new string[] { "\r\n", "\r", "\n" };
        private static char[] splitInRow = new char[] { ',' };

        private static string GetRandomPackageInfo()
        {
            string[] strings = NOOD.NoodyCustomCode.GetRows(_textAsset);
            return strings[new System.Random().Next(0, strings.Length)];
        }
        public static Package GetRandomPackage()
        {
            if(_textAsset == null)
                _textAsset = Resources.Load<TextAsset>("packages");

            string packageInfo = GetRandomPackageInfo();
            string[] packageInfoSplitted = NOOD.NoodyCustomCode.GetWordsInRow(packageInfo);
            return new Package(packageInfoSplitted[0], float.Parse(packageInfoSplitted[1]), packageInfoSplitted[2], float.Parse(packageInfoSplitted[3]));
        }
    }

    [System.Serializable]
    public class Package
    {
        public string _name;
        public float _weight = 0f;
        public string _destination = "town1";
        public float _money = 0f;

        public Package(string name, float weight, string destination, float money)
        {
            this._name = name;
            this._weight = weight;
            this._destination = destination;
            this._money = money;
        }
    }

}
