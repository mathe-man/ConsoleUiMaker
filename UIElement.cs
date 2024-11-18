using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUIMaker
{
    public class UIElement
    {
        private static string UIElementType = "UIElement";
        public virtual string GetUIElementType { get { return UIElement.UIElementType; } }

        public static UIElement FocusedElement = null;
        public static List<Action<ConsoleKeyInfo>> externsListeners = new List<Action<ConsoleKeyInfo>>();

        public static List<UIElement> UIElementList = new List<UIElement>();

        public int xAxisPosition;
        public int yAxisPosition;
        public int renderingLayer;

        public Action OnClick;


        public static void RunUI(int windowWidth = 0, int windowHeight = 0)
        {
            foreach (UIElement element in UIElementList)
            {
                element.Render();
            }
            while (true)
            {
                //Keyboard Listener
                //Détection de la pression qur le bouton tab
                ConsoleKeyInfo pressedConsoleKeyInfo = Console.ReadKey();
                

                if (pressedConsoleKeyInfo.Key == ConsoleKey.Tab)
                {
                    Console.CursorLeft -= 8;
                    Console.Write("");
                    if (FocusedElement == null)
                    {
                        SetFocusedElement(UIElementList[0]);
                    }
                    else
                    {
                        if (UIElementList.IndexOf(FocusedElement) != UIElementList.Count - 1)
                        {
                            SetFocusedElement(UIElementList[UIElementList.IndexOf(FocusedElement) + 1]);
                        }
                        else { SetFocusedElement(UIElementList[0]); }
                    }
                }
                else if (pressedConsoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    if (FocusedElement != null)
                    {
                        FocusedElement.OnClick.Invoke();
                    }
                }
                else
                {
                    if (FocusedElement == null || FocusedElement.GetUIElementType != "TextField")
                    {
                        Console.CursorLeft -= 1;
                        Console.Write("");
                    }
                }
                foreach (UIElement element in UIElementList)
                { element.Listener(UIElementList, pressedConsoleKeyInfo); }

                foreach (Action<ConsoleKeyInfo> action in externsListeners)
                { action.Invoke(pressedConsoleKeyInfo); }
            }
        }
        public virtual void Listener(List<UIElement> UIElementList, ConsoleKeyInfo keyInfo)
        {
            
        }

        public static void SetFocusedElement(UIElement element)
        {
            if (FocusedElement != null)
            { FocusedElement.Unfocus(); }
            FocusedElement = element;
            FocusedElement.FocusOn();
        }

        public virtual void Render()
        {
            Console.SetCursorPosition(xAxisPosition, yAxisPosition);
            Console.WriteLine(" UIElement ");
        }
        
        public virtual void FocusOn()
        {
            Console.SetCursorPosition(xAxisPosition, yAxisPosition);
            Console.Write("<UIElement>");
        }
        public virtual void Unfocus()
        {
            Console.SetCursorPosition(xAxisPosition, yAxisPosition);
            Console.Write(" UIElement ");
        }
    }
}
