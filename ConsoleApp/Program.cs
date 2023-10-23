using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

class StepSorter
{
    /*
     * Примечание:
     * Для эффективности удаление и вставки был использован LinkedList так как в нем процес удаления и вставки
     * не требует сдивга части масива
    */
    public static void Sort(LinkedList<IComparable> list)
    {
        int n = list.Count;

        int start = 0;
        int end = n - 1;

        do
        {
            int tempMax = int.MinValue;
            String tempMin = String.Empty;

            //Проходим до стартовой ноды
            LinkedListNode<IComparable> currentNode = list.First;
            for (int i = 0; i < start; i++)
                currentNode = currentNode.Next;

            for (int i = start; i <= end; i++)
            {
                //Определяем тип данного и сравниваем его с соответсвующим значением для поиска
                IComparable value = currentNode.Value;
                Type type = value.GetType();
                if (value is int && (int)value > tempMax)
                {
                    //Записываем само новое значение что бы в дальнейшем его перенести
                    tempMax = (int)value;
                }
                else if (value is String && (((String)value).CompareTo(tempMin) < 0 || tempMin.Length == 0))
                {
                    tempMin = (String)value;
                }

                //Переходим в следующую ноду
                if (currentNode.Next != null)
                    currentNode = currentNode.Next;
            }

            //Переносим наименшую строку в начало круга поиска
            list.Remove(tempMin);
            list.AddFirst(tempMin);

            //Переносим наибольшое число в конец круга поиска
            list.Remove(tempMax);
            list.AddLast(new LinkedListNode<IComparable>(tempMax));

            //Сужаем круг поиска
            start++;
            end--;

            Console.Write("Step " + start.ToString() + ": ");
            PrintLinkedList(list);
        } while (start < end);
    }

    public static void PrintLinkedList(LinkedList<IComparable> list)
    {
        foreach (IComparable item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        //List<IComparable> list = new List<IComparable> { "along", "four", 52, 25, "gym", "root", 15, "hat", 73, "bank", "success", 38, 46, 96 };
        Console.WriteLine("Enter a line of text:");
        string input = Console.ReadLine();
        string[] splited = input.Split(' ');

        // Превратим введенную строку в список IComparable (типы которые мы можем савнивать)
        LinkedList<IComparable> list = new LinkedList<IComparable>(splited.Select(item =>
        {
            int value;
            if (item.Length == 2 && int.TryParse(item, out value))
                return (IComparable)value;

            return item;
        }));

        Console.WriteLine("Original Integer Array:");
        StepSorter.PrintLinkedList(list);
        StepSorter.Sort(list);
        Console.WriteLine("Sorted Integer Array:");
        StepSorter.PrintLinkedList(list);
    }
}