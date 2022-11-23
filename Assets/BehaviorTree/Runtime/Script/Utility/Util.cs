using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public static class Util
    {

        public static T UnpackPropertyJson<T>(string json) where T : SerializableProperty
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string PackUserData(SerializableProperty nodeProperty)
        {
            String json = JsonUtility.ToJson(nodeProperty);
            return json;
        }
    }
}
