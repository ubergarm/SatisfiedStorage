using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using HarmonyLib;
using Verse;

namespace SatisfiedStorage
{
    public class StorageSettings_Mapping
    {
        private static Dictionary<StorageSettings, StorageSettings_Hysteresis> mapping = new Dictionary<StorageSettings, StorageSettings_Hysteresis>();

        public static MethodInfo TryNotifyChangedInfo = AccessTools.Method(typeof(StorageSettings), "TryNotifyChanged");

        public static StorageSettings_Hysteresis Get(StorageSettings storage)
        {
            bool flag = mapping.ContainsKey(storage);
            StorageSettings_Hysteresis result;
            if (flag)
            {
                result = mapping[storage];
            }
            else
            {
                result = new StorageSettings_Hysteresis();
            }

            return result;
        }

        public static void Set(StorageSettings storage, StorageSettings_Hysteresis value)
        {
            bool flag = mapping.ContainsKey(storage);
            if (flag)
            {
                mapping[storage] = value;
            }
            else
            {
                mapping.Add(storage, value);
            }
            // Signal any haulers of the stockpile settings update
            Log.Message("[DEBUG][MP] StorageSettings_Mapping.Set(): StorageSettings owner=" + storage.owner);
            Log.Message("[DEBUG][MP] StorageSettings_Mapping.Set(): FillPercent=" + value.FillPercent);
            TryNotifyChangedInfo.Invoke(storage, null);
        }
    }
}
