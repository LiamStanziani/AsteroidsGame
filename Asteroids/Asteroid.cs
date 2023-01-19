using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.Direct2D1.Effects;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

/* Asteroid.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.12.06: Created
 * Liam Stanziani & Nathan Garrity, 2022.12.06: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class Asteroid : DrawableGameComponent
    {
        public SpriteBatch _spriteBatch;

        public Vector2 _position;
        public float speed;
        public Texture2D _tex;
        public float size;
        public Vector2 scale;

        public Vector2 origin;
        public float _rotation;
        public Vector2 _direction;

        private Rectangle rect;

        /// <summary>
        /// A constructor for the Asteroid class
        /// </summary>
        /// <param name="game">A variable that is for the Game class</param>
        /// <param name="spriteBatch">A variable that is a SpriteBatch</param>
        /// <param name="direction">A variable that is a Vector</param>
        /// <param name="position">A variable that is a Vector</param>
        /// <param name="size">A variable that is a float</param>
        /// <param name="tex">A variable that is a Texture2D</param>
        public Asteroid(Game game,
            SpriteBatch spriteBatch, Vector2 direction,
        Vector2 position, float size,
            Texture2D tex) : base(game)
        {
            _spriteBatch = spriteBatch;
            _position = position;
            speed = 400F;
            _tex = tex;
            _direction = direction;
            this.size = size;
            scale = new Vector2(size, size);
            origin = new Vector2(tex.Width/2, tex.Height / 2);
            rect = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public override void Update(GameTime gameTime)
        {
            _position += _direction * 6;


            if (_position.X > 1250) { _position.X = -50; }
            if (_position.X < -50) { _position.X = 1250; }
            if (_position.Y > 950) { _position.Y = -50; }
            if (_position.Y < -50) { _position.Y = 950; }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_tex, _position, rect, Color.White, _rotation, origin, scale, SpriteEffects.None, 1f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// A method that gets the bounds of the current asteroid
        /// </summary>
        /// <returns>A rectangle that will be the bounds for the asteroid</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _tex.Width, _tex.Height);
        }
    }
}
