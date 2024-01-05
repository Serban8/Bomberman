using BombermanBase;
using BombermanBase.Entities;

namespace BombergameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEntitiesInitialization()
        {
            var game = BombermanFactory.CreateGame("Denis");

            IEntity player = new PlayerFactory().CreateEntity("Denis");
            List<IEntity> enemies = new List<IEntity>() { new EnemyFactory().CreateEntity("YoloBOMB"), new EnemyFactory().CreateEntity("enemy2") };

            Assert.AreEqual(player,game.Player);
            
            for (int i = 0; i < game.Enemies.Count; i++) 
            {
                Assert.AreEqual(enemies[i], game.Enemies[i]);
            }
            
        }
        [TestMethod]
        public void TestPlayerMoveOutOfMap()
        {
            var game = BombermanFactory.CreateGame("Denis");
            var map = TileMapFactory.CreateTileMap((24, 12),"C:\\Users\\Legion\\Desktop\\Portofolii\\an3\\IS\\Bomberman repo\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt");
 
            //verify upper left corner
            game.Player.Move(map, -1, 0);
            Assert.AreEqual((0, 0), game.Player.Position);
            game.Player.Move(map, 0, -1);
            Assert.AreEqual((0, 0), game.Player.Position);

            //move to upper right corner if possible
            if (map.GetTile((23,0)).Type == TileType.Path)
            {
                game.Player.Move(map, 23, 0);

                //verify upper right corner
                game.Player.Move(map, 1, 0);
                Assert.AreEqual((23, 0), game.Player.Position);
                game.Player.Move(map, 0, -1);
                Assert.AreEqual((23, 0), game.Player.Position);

                //move back
                game.Player.Move(map,-23,0);
            }
            
            //move to bottom right corner if possible
            if (map.GetTile((23, 11)).Type == TileType.Path)
            {
                game.Player.Move(map, 23, 11);

                //verify bottom right corner
                game.Player.Move(map, 1, 0);
                Assert.AreEqual((23, 11), game.Player.Position);
                game.Player.Move(map, 0, 1);
                Assert.AreEqual((23, 11), game.Player.Position);

                //move back
                game.Player.Move(map, -23, -11);
            }

            //move to bottom left corner if possible
            if (map.GetTile((0, 11)).Type == TileType.Path)
            {
                game.Player.Move(map, 0, 11);

                //verify bottom right corner
                game.Player.Move(map, -1, 0);
                Assert.AreEqual((0, 11), game.Player.Position);
                game.Player.Move(map, 0, 1);
                Assert.AreEqual((0, 11), game.Player.Position);
            }
        }
        [TestMethod]
        public void TestPlacingBomb()
        {
            var game = BombermanFactory.CreateGame("Denis");

            int exNoOfBombs = 3;

            while (game.Player.NoOfBombs > 0)
            {
                game.Player.RemoveBomb();
                Assert.AreEqual(exNoOfBombs, game.Player.NoOfBombs);
                exNoOfBombs--;
            }
            game.Player.RemoveBomb();
            Assert.AreEqual(0, game.Player.NoOfBombs);
        }
        //[TestMethod]
        //public void TestDestroy()
        //{
        //    //BombermanBase.ITile tileP = new BombermanBase.Tile((1, 1), BombermanBase.TileType.Path);
        //    //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
        //    //BombermanBase.Tile tileUW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.UnbreakableWall);
        //    //BombermanBase.Tile tileBW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.BreakableWall);

        //    //tileP.Destroy();
        //    //tilePB.Destroy();
        //    //tileUW.Destroy();
        //    //tileBW.Destroy();

        //    //Assert.AreEqual(BombermanBase.TileType.Path, tileP.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tilePB.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.Path, tileBW.Type);
        //}
        //[TestMethod]
        //public void TestAddBomb()
        //{
        //    //BombermanBase.Tile tileP = new BombermanBase.Tile((1, 1), BombermanBase.TileType.Path);
        //    //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
        //    //BombermanBase.Tile tileUW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.UnbreakableWall);
        //    //BombermanBase.Tile tileBW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.BreakableWall);

        //    //tileP.AddBomb();
        //    //tilePB.AddBomb();
        //    //tileUW.AddBomb();
        //    //tileBW.AddBomb();

        //    //Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tileP.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tilePB.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.BreakableWall, tileBW.Type);
        //}
        //[TestMethod]
        //public void TestExplode()
        //{
        //    //BombermanBase.Tile tileP = new BombermanBase.Tile((1, 1), BombermanBase.TileType.Path);
        //    //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
        //    //BombermanBase.Tile tileUW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.UnbreakableWall);
        //    //BombermanBase.Tile tileBW = new BombermanBase.Tile((1, 1), BombermanBase.TileType.BreakableWall);

        //    //tileP.Explode();
        //    //tilePB.Explode();
        //    //tileUW.Explode();
        //    //tileBW.Explode();

        //    //Assert.AreEqual(BombermanBase.TileType.Path, tileP.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.Path, tilePB.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
        //    //Assert.AreEqual(BombermanBase.TileType.Path, tileBW.Type);
        //}
        //[TestMethod]
        //public void TestWalkability()
        //{
        //    //BombermanBase.Tile tileP = new BombermanBase.Tile((1,1),BombermanBase.TileType.Path);
        //    //BombermanBase.Tile tilePB = new BombermanBase.Tile((1, 1), BombermanBase.TileType.PathWithBomb);
        //    //BombermanBase.Tile tileUW = new BombermanBase.Tile((1,1),BombermanBase.TileType.UnbreakableWall);
        //    //BombermanBase.Tile tileBW = new BombermanBase.Tile((1,1),BombermanBase.TileType.BreakableWall);

        //    //Assert.IsTrue(tileP.IsWalkable());
        //    //Assert.IsTrue(tilePB.IsWalkable());
        //    //Assert.IsFalse(tileUW.IsWalkable());
        //    //Assert.IsFalse(tileBW.IsWalkable());
        //    //Assert.IsFalse(tileP.IsWalkable());
        //}
    }
}