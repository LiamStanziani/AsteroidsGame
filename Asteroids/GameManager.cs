using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using System.Collections;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework.Media;

/* GameManager.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.12.01: Created
 * Liam Stanziani & Nathan Garrity, 2022.12.01: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.01: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.01: Comments added
*/

namespace Asteroids
{
    public class GameManager : GameComponent
    {


        private SpriteBatch spriteBatch;
        private Texture2D asteroidTex;
        private Spaceship spaceship;
        private Asteroid a;
        private Missile m;
        private HitExplosion exp;
        private List<Asteroid> asteroids = new List<Asteroid>();
        private List<Missile> missiles = new List<Missile>();
        private List<Spaceship> spaceships = new List<Spaceship>();
        SoundEffect hitSound;
        private Texture2D missileTex;
        private Texture2D spaceshipTex;
        private Vector2 spaceshipPos;
        private Vector2 spaceshipSpeed;
        private Song movementSound;
        SoundEffect shootSound;
        private ActionScene actionScene;
        public int timer = 0;
        public int spawnTimer = 100;
        private Texture2D explosionTex;
        public bool isSpaceshipActive = false;
        public HighScore hs;
        public static int storedHighScore;
        public static int _HighScoreFinal;
        private GameEndScene gameEndScene;
        private bool stopAsteroids  = false;

        /// <summary>
        /// A constructor for the GameManager class
        /// </summary>
        /// <param name="game">A variable for the Game class</param>
        /// <param name="spriteBatch">A variable that is a SpriteBatch</param>
        /// <param name="asteroidTex">A variable that is a Texture2D</param>
        /// <param name="spaceship">A variable for the SpaceShip class</param>
        /// <param name="asteroid">A variable for the Asteroid class</param>
        /// <param name="hitSound">A variable that is a SoundEffect</param>
        /// <param name="missileTex">A variable that is a Texture2D</param>
        /// <param name="missile">A variable for the Missile class</param>
        /// <param name="shootSound">A variable that is a SoundEffect</param>
        /// <param name="explosionTex">A variable that is a Texture2D</param>
        /// <param name="spaceshipTex">A variable that is a Texture2D</param>
        /// <param name="spaceshipPos">A variable that is a Vector</param>
        /// <param name="spaceshipSpeed">A variable that is a Vector</param>
        /// <param name="movementSound">A variable that is a Song</param>
        /// <param name="gameEndScene">A variable for the GameEndScene class</param>
        /// <param name="actionScene">A variable for the ActionScene cass</param>
        public GameManager(Game game, SpriteBatch spriteBatch, Texture2D asteroidTex, Spaceship spaceship, Asteroid asteroid, SoundEffect hitSound, Texture2D missileTex,
            Missile missile, SoundEffect shootSound, Texture2D explosionTex, Texture2D spaceshipTex, Vector2 spaceshipPos, Vector2 spaceshipSpeed, Song movementSound, GameEndScene gameEndScene, ActionScene actionScene) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.asteroidTex = asteroidTex;
            this.spaceship = spaceship;
            this.a = asteroid;
            this.hitSound = hitSound;
            this.actionScene = actionScene;
            this.missileTex = missileTex;
            this.m = missile;
            this.shootSound = shootSound;
            this.explosionTex = explosionTex;
            this.spaceshipTex = spaceshipTex;
            this.spaceshipPos = spaceshipPos;
            this.spaceshipSpeed = spaceshipSpeed;
            this.movementSound = movementSound;
            this.gameEndScene = gameEndScene;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kS = Keyboard.GetState();
            if (actionScene.Enabled == true && kS.IsKeyDown(Keys.Enter) && isSpaceshipActive == false)
            {
                stopAsteroids = false;
                spaceship = new Spaceship(Game, spriteBatch, spaceshipPos, spaceshipSpeed, spaceshipTex,
                        movementSound, hitSound);
                Game.Components.Add(spaceship);
                isSpaceshipActive = true;
            }

            KeyboardState ks = Keyboard.GetState();
            if (spaceship.Enabled == true)
            {
                if (ks.IsKeyDown(Keys.Space) && timer <= 0)
                {
                    shootSound.Play();

                    m = new Missile(Game, spriteBatch, spaceship.position, missileTex, spaceship.rotation, spaceship.direction, asteroids, hitSound, explosionTex);
                    Game.Components.Add(m);
                    missiles.Add(m);
                    timer = 12;
                }
                timer -= 1;
            }

            System.Random random = new System.Random();
            float ateroidRotation = random.Next(360);
            float ateroidSize = random.Next(1, 3);
            if (spawnTimer <= 0 && stopAsteroids == false)
            {
                Vector2 asteroidDirection = new Vector2((float)Math.Cos(ateroidRotation), (float)Math.Sin(ateroidRotation));
                Vector2 asteroidPosition = RandomPosition();
                a = new Asteroid(Game, spriteBatch, asteroidDirection, asteroidPosition, ateroidSize, asteroidTex);
                Game.Components.Add(a);
                asteroids.Add(a);
                spawnTimer = 35;
            }
            for (int i = 0; i < asteroids.Count; i++)
            {
                Rectangle AsteroidRect = asteroids[i].getBounds();
                Rectangle SpaceshipRect = spaceship.getBounds();

                if (asteroids[i].Enabled == true && spaceship.Enabled == true)
                {
                    if (SpaceshipRect.Intersects(AsteroidRect))
                    {
                        spaceship.Enabled = false;
                        spaceship.Visible = false;
                        hitSound.Play();
                        Vector2 position = spaceship.position;
                        Vector2 origin = spaceship.origin;

                        exp = new HitExplosion(Game, spriteBatch, explosionTex, position, 2, origin);
                        Game.Components.Add(exp);
                        exp.restart();

                        for (int j = 0; j < asteroids.Count; j++)
                        {

                            asteroids[j].Enabled = false;
                            asteroids[j].Visible = false;
                        }

                        stopAsteroids = true;

                        gameEndScene.show();

                    }
                }
            }

            if (kS.IsKeyDown(Keys.Escape))
            {
                for (int i = 0; i < asteroids.Count; i++)
                {
                    
                    asteroids[i].Enabled = false;
                    asteroids[i].Visible = false;
                }
                isSpaceshipActive = false;
                gameEndScene.hide();
            }

            spawnTimer -= 1;

        }

        /// <summary>
        /// A method that determines where the asteroids spawn on the map
        /// </summary>
        /// <returns>A new vector at a random position</returns>
        public Vector2 RandomPosition()
        {
            var rand = new Random();
            int side = rand.Next(4);

            //Each number represents a side

            switch (side)
            {
                // Right
                case 0:
                    return new Vector2(1250, rand.Next(-50, 950));

                // Bottom
                case 1:
                    return new Vector2(rand.Next(-50, 1250), 950);

                // Left
                case 2:
                    return new Vector2(-50, rand.Next(-50, 950));

                //Top
                default:
                    return new Vector2(rand.Next(-50, 1250), -50);

            }
        }

    }
}
