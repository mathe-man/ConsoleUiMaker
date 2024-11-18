using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUIMaker
{
    public class ProgressBar : UIElement
    {
        public Action onProgressBarIsFull;

        public char filledChar;
        public char emptyChar;

        public char startChar;
        public char endChar;

        public ProgressBar(Action whenProgressBarIsFull, int progressBarXAxisPosition, int progressBarYAxisPosition, char filledSymbole = '≡', char emptySymbole = ' ', char atStartChar = '[', char atEndChar = ']') 
        {
            OnClick = null;
            onProgressBarIsFull = whenProgressBarIsFull;

            xAxisPosition = progressBarXAxisPosition;
            yAxisPosition = progressBarYAxisPosition;

            filledChar = filledSymbole;
            emptyChar = emptySymbole;

            startChar = atStartChar;
            endChar = atEndChar;
        }


        public override void Render()
        {
            //Console.SetCursorPosition
        }
    }
}
