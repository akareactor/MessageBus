using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Reflection;

namespace Kulibin.Space.MessageBus {

    public abstract class AbstractGameMessage : ScriptableObject {

        public static AbstractGameMessage[] GetAllInstances () {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(AbstractGameMessage).Name); //FindAssets uses tags check documentation for more info
            AbstractGameMessage[] a = new AbstractGameMessage[guids.Length];
            for (int i = 0; i < guids.Length; i++) {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = (AbstractGameMessage)AssetDatabase.LoadAssetAtPath(path, typeof(AbstractGameMessage));
            }
        return a;
        }

    }

} 