using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

namespace Slothsoft.UnityExtensions {
    public static class TilemapExtensions {
        static readonly ConstructorInfo constructor = typeof(ITilemap)
            .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault();
        static readonly MethodInfo SetTilemapInstance = typeof(ITilemap)
            .GetMethod(nameof(SetTilemapInstance), BindingFlags.Instance | BindingFlags.NonPublic);
        /// <summary>
        /// Helper function to make sense of <a href="https://github.com/Unity-Technologies/UnityCsReference/blob/master/Modules/Tilemap/Managed/ITilemap.cs">ITilemap</a>.
        /// </summary>
        /// <param name="tilemap">The tilemap component instance</param>
        /// <returns>The tilemap interface instance</returns>
        public static ITilemap GetInterface(this Tilemap tilemap) {
            Assert.IsNotNull(constructor, "Constructor not found! Did ITilemap's implementation change?");
            Assert.IsNotNull(SetTilemapInstance, "Method 'SetTilemapInstance' not found! Did ITilemap's implementation change?");

            var result = constructor.Invoke(Array.Empty<object>()) as ITilemap;
            SetTilemapInstance.Invoke(result, new object[] { tilemap });
            return result;
        }

        static readonly FieldInfo m_Tilemap = typeof(ITilemap)
            .GetField(nameof(m_Tilemap), BindingFlags.Instance | BindingFlags.NonPublic);
        /// <summary>
        /// Helper function to make sense of <a href="https://github.com/Unity-Technologies/UnityCsReference/blob/master/Modules/Tilemap/Managed/ITilemap.cs">ITilemap</a>.
        /// </summary>
        /// <param name="tilemap">The tilemap interface instance</param>
        /// <returns>The tilemap component instance</returns>
        public static Tilemap GetImplementation(this ITilemap tilemap) {
            Assert.IsNotNull(m_Tilemap, "Field 'm_Tilemap' not found! Did ITilemap's implementation change?");

            return m_Tilemap.GetValue(tilemap) as Tilemap;
        }

        public static void ClearTile(this Tilemap tilemap, Vector3Int position) {
            tilemap.SetTile(position, null);
        }
        public static IEnumerable<(Vector3Int, TileBase)> GetUsedTiles(this ITilemap tilemap) {
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                var tile = tilemap.GetTile(position);
                if (tile) {
                    yield return (position, tile);
                }
            }
        }
        public static IEnumerable<(Vector3Int, TileBase)> GetUsedTiles(this Tilemap tilemap) {
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                var tile = tilemap.GetTile(position);
                if (tile) {
                    yield return (position, tile);
                }
            }
        }
        public static IEnumerable<(Vector3Int, TTile)> GetUsedTiles<TTile>(this ITilemap tilemap) where TTile : TileBase {
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                var tile = tilemap.GetTile<TTile>(position);
                if (tile) {
                    yield return (position, tile);
                }
            }
        }
        public static IEnumerable<(Vector3Int, TTile)> GetUsedTiles<TTile>(this Tilemap tilemap) where TTile : TileBase {
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                var tile = tilemap.GetTile<TTile>(position);
                if (tile) {
                    yield return (position, tile);
                }
            }
        }
        public static bool IsTile(this ITilemap tilemap, Vector3Int position, TileBase tile) {
            return tilemap.GetTile(position) == tile;
        }
        public static bool IsTile(this Tilemap tilemap, Vector3Int position, TileBase tile) {
            return tilemap.GetTile(position) == tile;
        }
        public delegate bool TileComparer(TileBase tileA, TileBase tileB);
        public static bool IsTile(this ITilemap tilemap, Vector3Int position, TileBase tile, TileComparer comparer) {
            return comparer(tilemap.GetTile(position), tile);
        }
        public static bool IsTile(this Tilemap tilemap, Vector3Int position, TileBase tile, TileComparer comparer) {
            return comparer(tilemap.GetTile(position), tile);
        }
    }
}