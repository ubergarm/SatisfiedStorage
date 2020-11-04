using System;
using Multiplayer.API;
using Verse;

namespace SatisfiedStorage
{
    [StaticConstructorOnStartup]
    static class Multiplayer
    {
        static Multiplayer()
        {
            if (!MP.enabled) return;

            // Similar to placing [SyncMethod] over the target method
            MP.RegisterSyncMethod(typeof(StorageSettings_Mapping), nameof(StorageSettings_Mapping.Set)).SetContext(SyncContext.CurrentMap);

            // Similar to [SyncWorker(shouldConstruct = true)] over the Sync worker method
            MP.RegisterSyncWorker<StorageSettings_Hysteresis>(SyncStorageSettings_Hysteresis, typeof(StorageSettings_Hysteresis), false, true);
        }

        static void SyncStorageSettings_Hysteresis(SyncWorker sync, ref StorageSettings_Hysteresis type)
        {
            sync.Bind(ref type.FillPercent);
            //Log.Message("[DEBUG][MP] SyncStorageSettings_Hysteresis(): FillPercent=" + type.FillPercent);
        }

    }
}
