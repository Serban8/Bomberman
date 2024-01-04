using BombermanBase;

namespace BombergameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDestroy()
        {
            //BombermanBase.ITile tileP = new BombermanBase.Tile((1, 1), BombermanBase.TileType.Path);
            //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
            //BombermanBase.Tile tileUW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.UnbreakableWall);
            //BombermanBase.Tile tileBW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.BreakableWall);

            //tileP.Destroy();
            //tilePB.Destroy();
            //tileUW.Destroy();
            //tileBW.Destroy();

            //Assert.AreEqual(BombermanBase.TileType.Path, tileP.Type);
            //Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tilePB.Type);
            //Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
            //Assert.AreEqual(BombermanBase.TileType.Path, tileBW.Type);
        }
        [TestMethod]
        public void TestAddBomb()
        {
            //BombermanBase.Tile tileP = new BombermanBase.Tile((1, 1), BombermanBase.TileType.Path);
            //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
            //BombermanBase.Tile tileUW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.UnbreakableWall);
            //BombermanBase.Tile tileBW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.BreakableWall);

            //tileP.AddBomb();
            //tilePB.AddBomb();
            //tileUW.AddBomb();
            //tileBW.AddBomb();

            //Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tileP.Type);
            //Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tilePB.Type);
            //Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
            //Assert.AreEqual(BombermanBase.TileType.BreakableWall, tileBW.Type);
        }
        [TestMethod]
        public void TestExplode()
        {
            //BombermanBase.Tile tileP = new BombermanBase.Tile((1, 1), BombermanBase.TileType.Path);
            //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
            //BombermanBase.Tile tileUW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.UnbreakableWall);
            //BombermanBase.Tile tileBW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.BreakableWall);

            //tileP.Explode();
            //tilePB.Explode();
            //tileUW.Explode();
            //tileBW.Explode();

            //Assert.AreEqual(BombermanBase.TileType.Path, tileP.Type);
            //Assert.AreEqual(BombermanBase.TileType.Path, tilePB.Type);
            //Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
            //Assert.AreEqual(BombermanBase.TileType.Path, tileBW.Type);
        }
        [TestMethod]
        public void TestWalkability()
        {
            //BombermanBase.Tile tileP = new BombermanBase.Tile((1,1),BombermanBase.TileType.Path);
            //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
            //BombermanBase.Tile tileUW = new BombermanBase.Tile((1,1),BombermanBase.TileType.UnbreakableWall);
            //BombermanBase.Tile tileBW = new BombermanBase.Tile((1,1),BombermanBase.TileType.BreakableWall);

            //Assert.IsTrue(tileP.IsWalkable());
            //Assert.IsTrue(tilePB.IsWalkable());
            //Assert.IsFalse(tileUW.IsWalkable());
            //Assert.IsFalse(tileBW.IsWalkable());
            //Assert.IsFalse(tileP.IsWalkable());
        }
    }
}