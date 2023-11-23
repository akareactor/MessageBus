using System;
using System.Reflection;
using System.Collections.Generic;
using Codice.Client.Common;
using Codice.Client.Common.GameUI;
using UnityEngine;
using UnityEngine.EventSystems;
using Kulibin.Space.MessageBus;

namespace UnityEditor.EventSystems {

    [CustomEditor(typeof(Listener), true)]
    public class ListenerEditor : Editor {
        private SerializedProperty bus_prop;
        private SerializedProperty listeners_prop;
        GUIContent m_IconToolbarMinus;
        GUIContent m_EventIDName;
        GUIContent[] m_EventTypes;
        SerializedObject newserobj;
        SerializedObject entriesobj;
        Listener listenerComponent;
        AbstractGameMessage[] agm;

        GUIContent m_AddButonContent;

        protected virtual void OnEnable() {
            listeners_prop = serializedObject.FindProperty("_entries");
            listenerComponent = (Listener)target;
            m_AddButonContent = new GUIContent("Add New Message Type");
            m_EventIDName = new GUIContent("");
            // Have to create a copy since otherwise the tooltip will be overwritten.
            m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
            m_IconToolbarMinus.tooltip = "Remove all events in this list.";
            // заполнение списка типов сообщений делать лучше только один раз, чтобы сохранять упорядоченность в дальнейшей работе 
            // одновременно с оптимизацией создания списка m_EventTypes
            agm = AbstractGameMessage.GetAllInstances();
            m_EventTypes = new GUIContent[agm.Length];
            for (int i = 0; i < agm.Length; ++i) {
                m_EventTypes[i] = new GUIContent(agm[i].name);
            }
        }

        public override void OnInspectorGUI() {
            this.serializedObject.Update(); //serializedObject.UpdateIfRequiredOrScript();
            // отладочный список
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("_entries"), new GUIContent("Entries"));
            // список слушателей
            int toBeRemovedEntry = -1;
            EditorGUILayout.Space();
            Vector2 removeButtonSize = GUIStyle.none.CalcSize(m_IconToolbarMinus);
            for (int i = 0; i < listeners_prop.arraySize; ++i) {
                SerializedProperty delegateProperty = listeners_prop.GetArrayElementAtIndex(i);
                EditorGUILayout.LabelField(new GUIContent("Слушатель <" + delegateProperty.type + ">"));
                SerializedProperty eventItemProperty = delegateProperty.FindPropertyRelative("eventItem");
                if (eventItemProperty != null) {
                    SerializedProperty callbacksProperty = eventItemProperty.FindPropertyRelative("broadcast");
                    if (callbacksProperty != null) {
                        m_EventIDName.text = agm[i].name;
                        EditorGUILayout.PropertyField(callbacksProperty, m_EventIDName);
                    } else {
                        EditorGUILayout.LabelField(new GUIContent("callbacksProperty null"));
                    }
                } else {
                    EditorGUILayout.LabelField(new GUIContent("eventItemProperty null " + delegateProperty.propertyPath));
                }
                Rect callbackRect = GUILayoutUtility.GetLastRect();
                Rect removeButtonPos = new Rect(callbackRect.xMax - removeButtonSize.x - 8, callbackRect.y + 1, removeButtonSize.x, removeButtonSize.y);
                if (GUI.Button(removeButtonPos, m_IconToolbarMinus, GUIStyle.none)) {
                    toBeRemovedEntry = i;
                }
                EditorGUILayout.Space();
            }
            if (toBeRemovedEntry > -1) {
                RemoveEntry(toBeRemovedEntry);
            }
            // кнопка меню добавления
            Rect btPosition = GUILayoutUtility.GetRect(m_AddButonContent, GUI.skin.button);
            const float addButonWidth = 200f;
            btPosition.x = btPosition.x + (btPosition.width - addButonWidth) / 2;
            btPosition.width = addButonWidth;
            if (GUI.Button(btPosition, m_AddButonContent)) {
                ShowAddTriggermenu();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void RemoveEntry(int toBeRemovedEntry) {
            listeners_prop.DeleteArrayElementAtIndex(toBeRemovedEntry);
        }

        void ShowAddTriggermenu () {
            // Now create the menu, add items and show it
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < m_EventTypes.Length; ++i) {
                menu.AddItem(m_EventTypes[i], false, OnAddNewSelected, i); // та самая оптимизация, не создавать каждый раз экземпляр элемента меню
            }
            menu.ShowAsContext();
            Event.current.Use();
        }

        private void OnAddNewSelected(object index) {
            int selected = (int)index;
            listenerComponent.AddEntry(agm[selected]); // по индексу выбираем тип сообщения!
            serializedObject.ApplyModifiedProperties();
        }

    }

}
