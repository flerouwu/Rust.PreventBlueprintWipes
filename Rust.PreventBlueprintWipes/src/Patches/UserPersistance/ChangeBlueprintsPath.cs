using System.Reflection.Emit;
using HarmonyLib;

namespace Rust.PreventBlueprintWipes.Patches.UserPersistance;

using UserPersistance = global::UserPersistance;
using Database = global::Facepunch.Sqlite.Database;

[HarmonyPatch]
public static class ChangeBlueprintsPath {
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(UserPersistance), MethodType.Constructor, typeof(string))]
    public static IEnumerable<CodeInstruction> Constructor(
        IEnumerable<CodeInstruction> instructions,
        ILGenerator generator
    ) {
        var matcher = new CodeMatcher(instructions, generator);

        matcher.MatchStartForward(
            new CodeMatch(OpCodes.Ldsfld, typeof(UserPersistance).Field(nameof(UserPersistance.blueprints))),
            new CodeMatch(OpCodes.Ldloc_1),
            CodeMatch.LoadsConstant(), // persistence version
            new CodeMatch(OpCodes.Stloc_2),
            new CodeMatch(it => it.opcode == OpCodes.Ldloca_S && it.operand is LocalBuilder { LocalIndex: 2 }),
            new CodeMatch(OpCodes.Call, typeof(int).Method(nameof(int.ToString))),
            new CodeMatch(OpCodes.Ldstr, ".db"),
            new CodeMatch(OpCodes.Call, typeof(string).Method(nameof(string.Concat), [
                typeof(string),
                typeof(string),
                typeof(string)
            ])),

            new CodeMatch(OpCodes.Ldc_I4_1),
            new CodeMatch(OpCodes.Callvirt, typeof(Database).Method(nameof(Database.Open)))
        );

        if (matcher.IsInvalid) {
            throw new Exception("Failed to find valid match.");
        }

        matcher.Advance(1);
        matcher.RemoveInstructions(7);

        // Same as original instructions excluding the persistence version.
        matcher.InsertAndAdvance(
            new CodeInstruction(OpCodes.Ldloc_1),
            new CodeInstruction(OpCodes.Ldstr, "db"),
            new CodeInstruction(OpCodes.Call, typeof(string).Method(nameof(string.Concat), [
                typeof(string),
                typeof(string)
            ]))
        );

        matcher.End();

        return matcher.Instructions();
    }
}