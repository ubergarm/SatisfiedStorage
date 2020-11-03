using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Multiplayer.API;
using Verse;
using RimWorld.Planet;

namespace SatisfiedStorage
{
    
    
    public class SatisfiedStorageMod : Mod
    {

        public SatisfiedStorageMod(ModContentPack content) : base(content)
        {
            Log.Message("SatisfiedStorage loading");

            if (MP.enabled)
            {
                MP.RegisterAll();
            }

            Harmony harmonyInstance = new Harmony("SatisfiedStorageMod");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());              

        }
    }    

}
