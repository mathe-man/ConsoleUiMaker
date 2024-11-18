using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUIMaker
{
    public class Button : UIElement
    {
        private static string UIElementType = "Button";
        public override string GetUIElementType { get { return Button.UIElementType; } }

        private bool isOnFocus;

        private static string defaultLeftBorder = "|";
        private static string defaultRightBorder = "|";

        public string leftBorder = defaultLeftBorder;
        public string rightBorder = defaultRightBorder;

        public bool useDefaultBorder = true;

        public string message;


        private int length;
        public int Length
        { 
            get { return length; }
            set 
            {
                if (value == 6 + message.Length)
                { length = value; }
                else
                { length = 6 + message.Length; }
            } 
        }

        private int xAxisEndPosition;
        public int XAxisEndPosition
        {
            get { return xAxisEndPosition; }
            set 
            { 
                if(value == xAxisPosition + length)
                { xAxisEndPosition = value; }
                else
                { xAxisEndPosition = xAxisPosition + length; } ; 
            }
        }

        public Button(string content, Action buttonAction, int buttonXAxisPosition, int buttonYAxisPosition) 
        {
            message = content;
            OnClick = buttonAction;

            xAxisPosition = buttonXAxisPosition; yAxisPosition = buttonYAxisPosition;

            Length = 0; XAxisEndPosition = 0;   //The value will be calculated automaticly (At line 21 and 34)
        }

        public static void SetDefaultsBorders(string newDefaultLeftBorder, string newDefaultRightBorder)
        {
            defaultLeftBorder = newDefaultLeftBorder; defaultRightBorder = newDefaultRightBorder;
        }
        public static void SetDefaultsBorders(string border)
        {
            defaultRightBorder = border; defaultRightBorder = border;
        }

        public void SetBorders(string newRightBorder, string newLeftBorder)
        {
            useDefaultBorder = false;
            rightBorder = newRightBorder; leftBorder = newLeftBorder;
        }
        public void SetBorders(string border)
        {
            useDefaultBorder = false;
            rightBorder = border; leftBorder = border;
        }
        
        
        //Visual rendering on console
        public override void Render()
        {
            Console.SetCursorPosition(xAxisPosition, yAxisPosition);
            if (useDefaultBorder )
            { Console.WriteLine($" {defaultLeftBorder}{message}{defaultRightBorder} "); }
            else
            { Console.WriteLine($" {leftBorder}{message}{rightBorder} "); }
        }
        public override void FocusOn()
        {
            isOnFocus = true;
            Console.SetCursorPosition(xAxisPosition, yAxisPosition);
            if (useDefaultBorder)
            { Console.WriteLine($"<{defaultLeftBorder}{message}{defaultRightBorder}>"); }
            else
            { Console.WriteLine($"<{leftBorder}{message}{rightBorder}>"); }
        }
        public override void Unfocus()
        {
            isOnFocus = false;
            Render();
        }
    }
}
