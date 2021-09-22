using UnityEngine;

namespace MKK.DoodleJumpe.Utility.Pool
{
    internal static class GameObjectPool
    {
        public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent)
            where T : Component
        {
            return Lean.Pool.LeanPool.Spawn(prefab, position, rotation, parent);
        }
        public static T Spawn<T>(T prefab, Transform parent = null, bool worldPositionStays = false)
            where T : Component
        {
            return Lean.Pool.LeanPool.Spawn(prefab, parent, worldPositionStays);
        }

        public static void Despawn(GameObject clone)
        {
            Lean.Pool.LeanPool.Despawn(clone);
        }

        public static void Clean()
        {
            Lean.Pool.LeanPool.Clean();
        }
    }
}
