#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;

namespace KulibinSpace.MessageBus {

    public static class EventItemRegistry {

        public static EventItem Create(Type payloadType) {

            foreach(var type in TypeCache.GetTypesDerivedFrom<EventItem>()) {

                Type baseType = type.BaseType;

                if(baseType == null)
                    continue;

                if(!baseType.IsGenericType)
                    continue;

                if(baseType.GetGenericTypeDefinition() != typeof(EventItem<>))
                    continue;

                Type genericArg =
                    baseType.GetGenericArguments()[0];

                if(genericArg == payloadType) {
                    return (EventItem)Activator.CreateInstance(type);
                }
            }

            Debug.LogError(
                $"No EventItem found for {payloadType}");

            return null;
        }
    }
}

#endif
