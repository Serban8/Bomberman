using BombermanBase.Entities;
using BombermanBase;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace BombermanMONO.LogicExtensions
{
    public static class BombermanExtensions
    {
        public static void Update(this IBomberman game)
        {
            UIHelpers.Keyboard.GetState();

            if (UIHelpers.Keyboard.IsKeyPressed(Keys.Left))
            {
                game.MovePlayer(-1, 0);

                PlayerExtensions.effects = SpriteEffects.FlipHorizontally;
            }
            else if (UIHelpers.Keyboard.IsKeyPressed(Keys.Right))
            {
                game.MovePlayer(1, 0);

                PlayerExtensions.effects = SpriteEffects.None;
            }

            if (UIHelpers.Keyboard.IsKeyPressed(Keys.Up))
            {
                game.MovePlayer(0, -1);

            }
            else if (UIHelpers.Keyboard.IsKeyPressed(Keys.Down))
            {
                game.MovePlayer(0, 1);
            }
            else if (UIHelpers.Keyboard.IsKeyPressed(Keys.Space))
            {
                game.PlaceBomb();
            }
            else if (UIHelpers.Keyboard.IsKeyPressed(Keys.K))
            {
                game.PauseEnemies();
            }
        }

        public static void Draw(this IBomberman game, SpriteBatch spriteBatch)
        {
            game.CrtLevel.Draw(spriteBatch);

            var screenCoords = game.Player.GetScreenCoords();
            PlayerExtensions.Draw(screenCoords, spriteBatch);

            foreach (var enemy in game.Enemies)
            {
                var screenCoordsEnemy = enemy.GetScreenCoords();
                EnemyExtensions.Draw(screenCoordsEnemy, spriteBatch);
            }
        }
    }
}
