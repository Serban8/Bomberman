using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Bomberman.NewFolder
{
    public enum TileType
    {
        Path,
        UnbreakableWall,
        BreakableWall,
        PathWithBomb
    }
    internal class Tile
    {
        private Texture2D texture;
        public Vector2 Position { get; set; }
        public TileType Type { get; set; }
        public Tile(Texture2D tileTexture, Vector2 tilePosition, TileType tileType)
        {
            texture = tileTexture;
            Position = tilePosition;
            Type = tileType;
        } 
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
        public void Destroy()
        {
            Type = TileType.Path;
        }
        public void AddBomb()
        {
            Type = TileType.PathWithBomb;
        }
    }
}
