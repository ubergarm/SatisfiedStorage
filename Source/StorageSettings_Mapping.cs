using System.Collections.Generic;
using RimWorld;
using Multiplayer.API;

namespace SatisfiedStorage
{
    public class StorageSettings_Mapping
    {
        private static Dictionary<StorageSettings, StorageSettings_Hysteresis> mapping = new Dictionary<StorageSettings, StorageSettings_Hysteresis>();

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

        [SyncMethod]
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
        }

        // MP API must already have a SyncWorker for handling RimWorld core type: `StorageSettings`
        // so only add marshalling function to handle StorageSettings_Hysteresis type
        [SyncWorker(shouldConstruct = true)]
        static void SyncStorageSettings_Hysteresis(SyncWorker sync, ref StorageSettings_Hysteresis type)
        {
            sync.Bind(ref type.FillPercent);
        }
    }
}
