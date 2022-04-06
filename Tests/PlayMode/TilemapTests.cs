using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Tests.PlayMode {
    public class TilemapTests {
        class TileA : TileBase { }
        class TileB : TileBase { }

        [UnitySetUp]
        public IEnumerator CreateTilemap() {
            var gridObj = new GameObject();
            grid = gridObj.AddComponent<Grid>();

            yield return null;

            var tilemapObj = new GameObject();
            tilemapObj.transform.parent = grid.transform;
            tilemap = tilemapObj.AddComponent<Tilemap>();

            yield return null;

            tileA = ScriptableObject.CreateInstance<TileA>();
            tileB = ScriptableObject.CreateInstance<TileB>();
        }
        [UnityTearDown]
        public IEnumerator DestroyTilemap() {
            if (grid) {
                UnityObject.Destroy(grid.gameObject);
            }
            yield return null;
        }


        Grid grid;
        Tilemap tilemap;
        TileA tileA;
        TileB tileB;

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [Test]
        public void TestGetUsedTilesNonGeneric(int childCount) {
            var expectedChildren = new Dictionary<Vector3Int, TileBase>();
            for (int i = 0; i < childCount; i++) {
                var position = new Vector3Int(i, 0, 0);
                tilemap.SetTile(position, tileA);
                expectedChildren[position] = tileA;

                position = new Vector3Int(0, i, 0);
                tilemap.SetTile(position, tileB);
                expectedChildren[position] = tileB;
            }

            var actualChildren = tilemap.GetUsedTiles();

            CollectionAssert.AreEquivalent(expectedChildren.Select(tile => (tile.Key, tile.Value)), actualChildren);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [Test]
        public void TestGetUsedTilesGeneric(int childCount) {
            var expectedChildren = new Dictionary<Vector3Int, TileB>();
            for (int i = 0; i < childCount; i++) {
                var position = new Vector3Int(i, 0, 0);
                tilemap.SetTile(position, tileA);

                position = new Vector3Int(0, i, 0);
                tilemap.SetTile(position, tileB);
                expectedChildren[position] = tileB;
            }

            var actualChildren = tilemap.GetUsedTiles<TileB>();

            CollectionAssert.AreEquivalent(expectedChildren.Select(tile => (tile.Key, tile.Value)), actualChildren);
        }
    }
}