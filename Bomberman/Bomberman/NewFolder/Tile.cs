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
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }
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
            //spriteBatch.Draw(
            //             texture,
            //             Position,
            //             null,
            //             Color.White,
            //             0f,
            //             new Vector2(texture.Width / 2, texture.Height / 2),
            //             Vector2.One,
            //             SpriteEffects.None,
            //             0f);
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
