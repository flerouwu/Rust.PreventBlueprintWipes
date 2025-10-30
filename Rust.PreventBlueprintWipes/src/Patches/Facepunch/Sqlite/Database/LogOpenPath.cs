using HarmonyLib;
using UnityEngine;

namespace Rust.PreventBlueprintWipes.Patches.Facepunch.Sqlite.Database;

using Database = global::Facepunch.Sqlite.Database;

[HarmonyPatch]
public static class LogOpenPath {
    #if DEBUG

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Database), nameof(Database.Open))]
    public static void Open(string path) {
        Debug.Log($"Opening database from {path}.");
    }

    #endif
}