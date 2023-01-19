using DocumentFormat.OpenXml.Office2019.Excel.RichData2;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using SpriteBatch = SharpDX.Direct2D1.SpriteBatch;

/* HighScore.cs
 * Asteroids
 * Revision History
 * Liam Stanziani, 2022.12.07: Created
 * Liam Stanziani, 2022.12.07: Added Code
 * Liam Stanziani, 2022.11.04: Debugging complete
 * Liam Stanziani, 2022.10.04: Comments added
*/

namespace Asteroids
{
    public class HighScore
    {
        public ActionScene aS;
        public HighScoreScene hss;
        public GameEndScene ges;
        public int _highScore;
        private string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"highscores.txt");
        private const int MAX_NUM_OF_SCORES = 10;
        public string[] allHighScores;
        private static int currentHighScore;

        /// <summary>
        /// A constructor for the HighScore class
        /// </summary>
        /// <param name="HighScore">A variable that is for an int</param>
        public HighScore(int HighScore)
        {
            _highScore = HighScore;
            allHighScores = File.ReadAllLines(fileName);
            HighScoreScene.allHighScores = allHighScores;
            ActionScene.storedHighScore = _highScore;
            Missile._highScoreCounter = _highScore;
            GameEndScene.highScore = _highScore;
        }

        /// <summary>
        /// A method that will run when the user has lost the game and has enterd a name for their player which will add a highscore
        /// to the .txt file that stores all of the highscores. The highscores below the new one will be dropped down below it, if
        /// it falls out of the range of the array it will be deleted
        /// </summary>
        /// <param name="highScoreFinal"></param>
        /// <param name="playerName"></param>
        public void StoreHighScore(int highScoreFinal, string playerName)
        {     
            for (int i = 1; i < MAX_NUM_OF_SCORES; i++)
            {
                try
                {
                    string value = Regex.Replace(allHighScores[i - 1], @"[A-Za-z:\s]", "");
                    if (Convert.ToInt64(value) < highScoreFinal)
                    {
                        for (int j = allHighScores.Length - 1; j > i - 1; j--)
                        {
                            allHighScores[j] = allHighScores[j - 1];
                        }
                        allHighScores[i - 1] = playerName + ": " + Convert.ToString(highScoreFinal);
                        File.WriteAllLines(fileName, allHighScores);

                        Debug.WriteLine(playerName);
                        Debug.WriteLine(highScoreFinal);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
                    {

                    }
                }

            }
        }
    }
}
