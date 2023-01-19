using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

/* Missile.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.12.06: Created
 * Liam Stanziani & Nathan Garrity, 2022.12.06: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class Missile : DrawableGameComponent
    {
        public SpriteBatch _spriteBatch;
        public Vector2 _position;
        public float speed;
        public Texture2D _tex;
        public Texture2D expTex;
        private HitExplosion exp;
        public Vector2 scale;
        private List<Asteroid> _asteroids = new List<Asteroid>();
        public Vector2 origin;
        public float _rotation;
        public Vector2 _direction;
        SoundEffect _hitSound;
        private Rectangle rect;
        public HighScore hs;
        public int storedCurrentScore;
        public static int _highScoreCounter;
        public const int HIGH_SCORE_INCREASE = 50;

        /// <summary>
        /// A constructor for the Missile class
        /// </summary>
        /// <param name="game">A variable that is for the Game class</param>
        /// <param name="spriteBatch">A variable that is a SpriteBatch</param>
        /// <param name="position">A variable that is a Vector</param>
        /// <param name="tex">A variable that is a Texture2D</param>
        /// <param name="rotation">A variable that is a float</param>
        /// <param name="direction">A variable that is a Vector</param>
        /// <param name="asteroids">A variable that is a l=List</param>
        /// <param name="hitSound">A variable that is a SoundEffect</param>
        /// <param name="expTex">A variable that is a Texture2D</param>
        public Missile(Game game,
            SpriteBatch spriteBatch,
            Vector2 position,
            Texture2D tex, float rotation, Vector2 direction, List<Asteroid> asteroids, SoundEffect hitSound, Texture2D expTex) : base(game)
        {
            _spriteBatch = spriteBatch;
            _position = position;
            speed = 400F;
            _tex = tex;
            _rotation = rotation;
            _direction = direction;
            _asteroids = asteroids;
            _hitSound = hitSound;
            scale = new Vector2(1, 1);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            rect = new Rectangle(0, 0, tex.Width, tex.Height);
            _hitSound = hitSound;
            this.expTex = expTex;
        }
        

        public override void Update(GameTime gameTime)
        {
            _position += _direction * 30;


            for (int i = 0; i < _asteroids.Count; i++)
            {
                Rectangle AsteroidRect = _asteroids[i].getBounds();
                Rectangle missileRect = getBounds();

                if (_asteroids[i].Enabled == true)
                {
                    if (AsteroidRect.Intersects(missileRect))
                    {
                        this.Enabled = false;
                        _asteroids[i].Enabled = false;
                        this.Visible = false;
                        _asteroids[i].Visible = false;
                        _hitSound.Play();

                       
                        _highScoreCounter += HIGH_SCORE_INCREASE;


                        hs = new HighScore(_highScoreCounter);

                        Vector2 position = this._position;

                        Vector2 aOrigin = _asteroids[i].origin;

                        exp = new HitExplosion(Game, _spriteBatch, expTex, position, 2, aOrigin);
                        Game.Components.Add(exp);
                        exp.restart();
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_tex, _position, rect, Color.White, _rotation, origin, scale, SpriteEffects.None, 1f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _tex.Width , _tex.Height);
        }
    }
}
