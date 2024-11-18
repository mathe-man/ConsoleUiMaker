using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUIMaker
{
    public class TextField : UIElement
    {
        private static string UIElementType = "TextField";
        public override string GetUIElementType { get { return TextField.UIElementType; } }

        private bool isOnFocus;

        public int colums;
        public int rows;
        public int cursorActualRows = 0;
        public int cursorActualColums = 0;

        public bool isForPassword;

        public string[] content;
        public string placeHolder;


        public TextField(string textPlaceHolder, int fieldColumns, int fieldRows, int buttonXAxisPosition, int buttonYAxisPosition, bool isPasswordField = false)
        {
            placeHolder = textPlaceHolder;
            colums = fieldColumns;
            rows = fieldRows;
            
            isForPassword = isPasswordField;

            content = new string[fieldRows];
            for (int i = 0; i < fieldRows; i++)
            {
                //content[i] = " ";
                for (int j = 0; j < fieldColumns; j++)
                {
                    content[i] += " ";
                }
            }
            

            xAxisPosition = buttonXAxisPosition; yAxisPosition = buttonYAxisPosition;

            OnClick = AddContent;
        }

        public override void Listener(List<UIElement> UIElementList, ConsoleKeyInfo keyInfo)
        {
            if (isOnFocus)
            {
                //if arrows are pressed then the cursor will be moved
                if (keyInfo.Key == ConsoleKey.UpArrow)
                { MoveCursor("Up"); }

                else if (keyInfo.Key == ConsoleKey.DownArrow)
                { MoveCursor("Down"); }

                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                { MoveCursor("Left"); }

                else if (keyInfo.Key == ConsoleKey.RightArrow)
                { MoveCursor("Right"); }

                //Delete the character behind the cursor
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    cursorActualColums -= 1;
                    if (Console.CursorLeft == xAxisPosition)
                    {
                        Console.Write("|");
                        cursorActualColums += 1;
                    }
                    else
                    {
                        Console.Write(" ");
                        cursorActualColums += 1;

                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        cursorActualColums -= 1;

                        //Retire la lettre dans la variable content
                        content[cursorActualRows] = content[cursorActualRows].Remove(cursorActualColums, 1);
                        content[cursorActualRows] = content[cursorActualRows].Insert(cursorActualColums, " ");
                    }
                }

                //Errors can appen when Tab is pressed so this condition catch letter and pass the Tab
                else if (keyInfo.Key != ConsoleKey.Tab)
                {
                    cursorActualColums += 1;

                    if (Console.CursorLeft == xAxisPosition + colums + 2)
                    {
                        MoveCursor("Left");
                        Console.Write("|");
                        cursorActualColums += 1;
                        MoveCursor("Left");
                    }
                    else
                    {
                        if (isForPassword)
                        {
                            MoveCursor("Left");
                            Console.Write("*");
                            cursorActualColums += 1;
                        }
                        content[cursorActualRows] = content[cursorActualRows].Remove(cursorActualColums -1 < 0 ? 0 : cursorActualColums - 1, 1);
                         
                        content[cursorActualRows] = content[cursorActualRows].Insert(cursorActualColums - 1 < 0 ? 0 : cursorActualColums - 1, keyInfo.KeyChar.ToString());
                    }
                }
            }
        }

        public override void Render()
        {
            //Crétation de la bordure supérieur
            Console.SetCursorPosition(xAxisPosition + 1, yAxisPosition);
            for (int i = 0; i < colums; i++) { Console.Write("-"); }

            //éffacer le contenu du champ
            for (int i = 0; i < rows; i++) 
            {
                Console.SetCursorPosition(xAxisPosition + 1, yAxisPosition + 1 + i);
                for (int j = 0; j < colums; j++) { Console.Write(" "); }
            }

            //Création du place holder
            Console.SetCursorPosition(xAxisPosition + 1, yAxisPosition + (rows > 1 ? 1 : rows));
            Console.Write($"{placeHolder}");

            //Création des bordures de gauche et de droite
            for (int i = 0; i < rows + 2; i++)
            {
                Console.SetCursorPosition(xAxisPosition, yAxisPosition + i);    Console.Write("|");
                Console.SetCursorPosition(xAxisPosition + colums + 1, yAxisPosition + i); Console.Write("|");
            }

            //Création de la bordure inférieur
            Console.SetCursorPosition(xAxisPosition + 1, yAxisPosition + rows + 1);
            for (int i = 0; i < colums; i++) { Console.Write("-"); }

            //Création des interséctions entre les bordures d'angles différent
            Console.SetCursorPosition(xAxisPosition, yAxisPosition); Console.Write("+");
            Console.SetCursorPosition(xAxisPosition + colums + 1, yAxisPosition); Console.Write("+");
            Console.SetCursorPosition(xAxisPosition, yAxisPosition + rows + 1); Console.Write("+");
            Console.SetCursorPosition(xAxisPosition + colums + 1, yAxisPosition + rows + 1); Console.Write("+");
        }

        public override void FocusOn()
        {
            isOnFocus = true;
            Console.SetCursorPosition(xAxisPosition + 1, yAxisPosition + (rows > 1 ? 1 : rows));
            for (int i = 2; i < colums; i++) { Console.Write(" "); }
            
            //Afficher le contenu du champ pour chaque ligne
            if (!isForPassword)
            {
                for (int i = 1; i <= rows; i++)
            {
                Console.SetCursorPosition(xAxisPosition + 1, yAxisPosition + i);
                Console.Write(content[i-1]);
            }
            }

            Console.SetCursorPosition(xAxisPosition + 1, yAxisPosition + 1);
            cursorActualColums = 0; cursorActualColums = 0;
            Console.CursorVisible = true;
        }
        public override void Unfocus()
        {
            isOnFocus = false;
            Render();
            //Console.SetCursorPosition(xAxisPosition + 2, yAxisPosition);
            //Console.Write(placeHolder);
            Console.CursorVisible = false;
        }

        public void AddContent()
        {
            
        }
        public void MoveCursor(string direction)
        { 
            if (direction == "Up" && Console.CursorTop - 1 != yAxisPosition)
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                cursorActualRows -= 1;
            }
            else if (direction == "Down" && Console.CursorTop + 1 != yAxisPosition + rows + 1)
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
                cursorActualRows += 1;
            }

            else if (direction == "Right" && Console.CursorLeft + 1 != xAxisPosition + colums + 2)
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                cursorActualColums += 1;
            }
            else if (direction == "Left" && Console.CursorLeft - 1 != xAxisPosition)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                cursorActualColums -= 1;
            }
        }
    }
}
