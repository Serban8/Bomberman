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

            IEntity player = new PlayerFactory().CreateEntity("Denis", (0,0));
            List<IEntity> enemies = new List<IEntity>() { new EnemyFactory().CreateEntity("YoloBOMB", (0, 0)), new EnemyFactory().CreateEntity("enemy2", (0, 0)) };

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
            //var map = TileMapFactory.CreateTileMap((24, 12),"C:\\Users\\Legion\\Desktop\\Portofolii\\an3\\IS\\Bomberman repo\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt");
            var map = TileMapFactory.CreateTileMap((24, 12), "C:\\Users\\mihai\\OneDrive\\Materiale cursuri\\Anul3\\IS\\proiect\\Bomberman\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt");
 
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
        [TestMethod]
        public void LosingLife()
        {
            var game = BombermanFactory.CreateGame("Denis");

            int exNoOfLives = 2;

            while (game.Player.NoOfLives > 0)
            {
                game.Player.RemoveLife();
                Assert.AreEqual(exNoOfLives, game.Player.NoOfLives);
                exNoOfLives--;
            }
            game.Player.RemoveLife();
            Assert.AreEqual(0, game.Player.NoOfLives);
        }
        [TestMethod]
        public void MakePathFromBWall()
        {
            var game = BombermanFactory.CreateGame("Denis");
            var map = TileMapFactory.CreateTileMap((24, 12), "C:\\Users\\Legion\\Desktop\\Portofolii\\an3\\IS\\Bomberman repo\\Bomberman\\Bomberman\\Content\\Level\\Level1.txt");

            game.Player.Move(map, 4, 5);

            //try to move right but is a breakable wall so it will stay in position
            game.Player.Move(map, 1, 0);
            Assert.AreEqual((4, 5), game.Player.Position);

            //place bomb
            map.Explode(map.GetTile(game.Player.Position));

            //try to move player again and this time it should work because the wall is broken
            game.Player.Move(map, 1, 0);
            Assert.AreEqual((5, 5), game.Player.Position);

        }
    
        [TestMethod]
        public void TestDestroyTile()
        {
            BombermanBase.ITile tileP = TileFactory.CreateTile((1, 1), TileType.Path);
            BombermanBase.ITile tilePB = TileFactory.CreateTile((1, 1), TileType.PathWithBomb);
            BombermanBase.ITile tileUW = TileFactory.CreateTile((1, 1), TileType.UnbreakableWall);
            BombermanBase.ITile tileBW = TileFactory.CreateTile((1, 1), TileType.BreakableWall);

            tileP.Destroy();
            tilePB.Destroy();
            tileUW.Destroy();
            tileBW.Destroy();

            Assert.AreEqual(BombermanBase.TileType.Path, tileP.Type);
            Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tilePB.Type);
            Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
            Assert.AreEqual(BombermanBase.TileType.Path, tileBW.Type);
        }
        [TestMethod]
        public void TestAddBomb()
        {
            BombermanBase.ITile tileP = TileFactory.CreateTile((1, 1), TileType.Path);
            BombermanBase.ITile tilePB = TileFactory.CreateTile((1, 1), TileType.PathWithBomb);
            BombermanBase.ITile tileUW = TileFactory.CreateTile((1, 1), TileType.UnbreakableWall);
            BombermanBase.ITile tileBW = TileFactory.CreateTile((1, 1), TileType.BreakableWall);

            tileP.AddBomb();
            tilePB.AddBomb();
            tileUW.AddBomb();
            tileBW.AddBomb();

            Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tileP.Type);
            Assert.AreEqual(BombermanBase.TileType.PathWithBomb, tilePB.Type);
            Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
            Assert.AreEqual(BombermanBase.TileType.BreakableWall, tileBW.Type);
        }
        [TestMethod]
        public void TestExplode()
        {
            BombermanBase.ITile tileP = TileFactory.CreateTile((1, 1), TileType.Path);
            BombermanBase.ITile tilePB = TileFactory.CreateTile((1, 1), TileType.PathWithBomb);
            BombermanBase.ITile tileUW = TileFactory.CreateTile((1, 1), TileType.UnbreakableWall);
            BombermanBase.ITile tileBW = TileFactory.CreateTile((1, 1), TileType.BreakableWall);

            tileP.Explode();
            tilePB.Explode();
            tileUW.Explode();
            tileBW.Explode();

            Assert.AreEqual(BombermanBase.TileType.Path, tileP.Type);
            Assert.AreEqual(BombermanBase.TileType.Path, tilePB.Type);
            Assert.AreEqual(BombermanBase.TileType.UnbreakableWall, tileUW.Type);
            Assert.AreEqual(BombermanBase.TileType.Path, tileBW.Type);
        }
        [TestMethod]
        public void TestWalkability()
        {
            BombermanBase.ITile tileP = TileFactory.CreateTile((1, 1), TileType.Path);
            BombermanBase.ITile tilePB = TileFactory.CreateTile((1, 1), TileType.PathWithBomb);
            BombermanBase.ITile tileUW = TileFactory.CreateTile((1, 1), TileType.UnbreakableWall);
            BombermanBase.ITile tileBW = TileFactory.CreateTile((1, 1), TileType.BreakableWall);

            Assert.IsTrue(tileP.IsWalkable());
            Assert.IsTrue(tilePB.IsWalkable());
            Assert.IsFalse(tileUW.IsWalkable());
            Assert.IsFalse(tileBW.IsWalkable());
        }
    }
}