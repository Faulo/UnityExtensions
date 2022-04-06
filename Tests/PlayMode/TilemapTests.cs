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

        [Test]
        public void TestGetInterface() {
            Assert.IsNotNull(tilemap.GetInterface());
            Assert.AreEqual(tilemap, tilemap.GetInterface().GetComponent<Tilemap>());
        }

        [Test]
        public void TestGetImplementation() {
            Assert.IsNotNull(tilemap.GetInterface().GetImplementation());
            Assert.AreEqual(tilemap, tilemap.GetInterface().GetImplementation());
        }

        static int[] childCounts = new[] {
            0,
            1,
            10
        };
        static bool[] booleans = new[] {
            false,
            true
        };

        [Test]
        public void TestGetUsedTilesNonGeneric(
            [ValueSource(nameof(childCounts))] int childCount,
            [ValueSource(nameof(booleans))] bool useInterface) {

            var expectedChildren = new Dictionary<Vector3Int, TileBase>();
            for (int i = 0; i < childCount; i++) {
                var position = new Vector3Int(i, 0, 0);
                tilemap.SetTile(position, tileA);
                expectedChildren[position] = tileA;

                position = new Vector3Int(0, i, 0);
                tilemap.SetTile(position, tileB);
                expectedChildren[position] = tileB;
            }

            var actualChildren = useInterface
                ? tilemap.GetInterface().GetUsedTiles()
                : tilemap.GetUsedTiles();

            CollectionAssert.AreEquivalent(expectedChildren.Select(tile => (tile.Key, tile.Value)), actualChildren);
        }

        [Test]
        public void TestGetUsedTilesGeneric(
            [ValueSource(nameof(childCounts))] int childCount,
            [ValueSource(nameof(booleans))] bool useInterface) {

            var expectedChildren = new Dictionary<Vector3Int, TileB>();
            for (int i = 0; i < childCount; i++) {
                var position = new Vector3Int(i, 0, 0);
                tilemap.SetTile(position, tileA);

                position = new Vector3Int(0, i, 0);
                tilemap.SetTile(position, tileB);
                expectedChildren[position] = tileB;
            }

            var actualChildren = useInterface
                ? tilemap.GetInterface().GetUsedTiles<TileB>()
                : tilemap.GetUsedTiles<TileB>();

            CollectionAssert.AreEquivalent(expectedChildren.Select(tile => (tile.Key, tile.Value)), actualChildren);
        }
    }
}