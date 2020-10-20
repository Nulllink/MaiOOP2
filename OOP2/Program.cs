using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace OOP2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            lab1();
            lab2();
            lab3();
            Console.ReadKey();
        }

        static void lab1()
        {
            try
            {
                Console.Write("Введите радиус шара: ");
                double r = double.Parse(Console.ReadLine()); //считывание из консоли и преобразование в десятичное число 
                figure f = new figure(r); //создание обьекта класса фигура
                Console.WriteLine("Радиус шара = " + f.r);
                Console.WriteLine();
                Console.Write("Введите радиус шара 1: ");
                r = double.Parse(Console.ReadLine());
                figure f1 = new figure(r); //создание нового обьекта класса
                Console.Write("Введите радиус шара 2: ");
                r = double.Parse(Console.ReadLine());
                figure f2 = new figure(r);
                Console.WriteLine();
                double ff = f1.s - f2.s; //Разница площадей первой фигуры и второй
                Console.WriteLine("Разница между площадью поверхности 1 и 2 шара = " + string.Format("{0:f}", ff));
                ff = f1.v - f2.v; // разница объемов
                Console.WriteLine("Разница между объемом 1 и 2 шара = " + string.Format("{0:f}", ff));
            }
            catch
            {
                Console.WriteLine("Введены неверные значения");
            }
        }

        static void lab2()
        {
            Console.WriteLine();
            Console.Write("Количество элементов в массиве: ");
            int n = int.Parse(Console.ReadLine());
            figure[] mass = new figure[n]; //создание массива объектов класса фигура
            for (int i = 0; i < mass.Length; i++) 
            {
                Console.Write("Введите радиус шара: ");
                double r = double.Parse(Console.ReadLine()); 
                mass[i] = new figure(r); //создание обьекта класса фигруре и занесение в массив
            }
            //Вывод елементов массива
            
            writearray(mass);
            //Выбор файла для сохранения
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.ShowDialog();//окрытие диалога выбора файла
            savefile(mass, sfd.FileName);//сохранение файла
            if (mass.Length > 2)
            {
                Console.WriteLine();
                Console.Write("Введите радиус шара: ");
                double rr = double.Parse(Console.ReadLine()); //радиус
                mass[2] = new figure(rr); //создание обьекта класса занесение в массив
                savefile(mass, sfd.FileName);//сохранение файла
                writearray(mass); //вывод массива
            }
            #region старый вывод
            //чтение из файла и вывод в консоль
            //StreamReader sr = new StreamReader(sfd.FileName);
            //string s;
            //while ((s = sr.ReadLine()) != null)
            //{
            //    string[] st = s.Split();
            //    Console.WriteLine("Таблица характеристик объектов");
            //    Console.WriteLine("Номер  Радиус  Площадь  Объем");
            //    Console.WriteLine("------------------------------");
            //    Console.WriteLine($"{st[0]}  {st[1]}  {st[2]}  {st[3]}");
            //}
            //sr.Close();
            #endregion 
        }

        static void lab3()
        {
            Console.WriteLine();
            Console.Write("Количество элементов в массиве: ");
            int n = int.Parse(Console.ReadLine());
            figure1[] mass = new figure1[n]; //создание массива класса фигуры
            //заполнение массива
            for (int i = 0; i < mass.Length; i++) 
            {
                Console.Write("Введите радиус и цвет шара: ");
                string[] text = Console.ReadLine().Split();
                double r = double.Parse(text[0]);
                mass[i] = new figure1(text[1], r); //создание обьекта класса фигруре и занесение в массив
            }
            //writearray(mass,""); //вывод массива
            string colr = "yellow";
            if(writearray(mass, colr) == 0)//если нет фигур с указаным цветом
            {
                Console.WriteLine($"Фигур с цветом {colr} нет");
            }
        }
        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        static void savefile(figure[] mass, string file)
        {
           

            StreamWriter sw = new StreamWriter(file); //создание обьекта класса с указанием файла, открытие потока
            for (int i = 0; i < mass.Length; i++)
            {
                sw.WriteLine($"{i:d5} {mass[i].r:f2} {mass[i].s:f2} {mass[i].v:f2}");
            }
            sw.Close(); //закрытие потока
        }
        /// <summary>
        /// Вывод в консоль массива
        /// </summary>
         static void writearray(figure[] arrayf)
        {
            //Console.Write()
            Console.WriteLine();
            Console.WriteLine("Таблица характеристик объектов");
            Console.WriteLine("Номер  Радиус  Площадь  Объем");
            Console.WriteLine("------------------------------");
            for (int i = 0; i < arrayf.Length; i++)
            {
                Console.WriteLine($"{i:d5}  {arrayf[i].r:f2}  {arrayf[i].s:f2}  {arrayf[i].v:f2}");
            }
        }
        /// <summary>
        /// Вывод в консоль массива с параметром цвета
        /// </summary>
        static int writearray(figure1[] arrayf, string colr)
        {
            Console.WriteLine();
            Console.WriteLine("Таблица характеристик объектов");
            Console.WriteLine("Номер  Радиус  Площадь  Объем   Цвет");
            Console.WriteLine("------------------------------------");
            int count = 0;
            for (int i = 0; i < arrayf.Length; i++)
            {
                if (colr == arrayf[i].col) //проверка цвета
                {
                    Console.WriteLine($"{i:d5}  {arrayf[i].r:f2}  {arrayf[i].s:f2}  {arrayf[i].v:f2}  {arrayf[i].col}");
                    count++;
                }
            }
            return count;//вывод количество нахождений
        }


    }
    class figure
    {
        public double r; //радиус
        public double s; //площадь
        public double v; //обьем
        public figure(double _r) 
        {
            r = _r;
            s = 4 * Math.PI * r * r;//вычисление площади
            v = 4 / 3 * Math.PI * r * r * r; //вычисление объема
        }
        
    }

    class figure1 : figure
    {
        public string col; //цвет
        public figure1(string _col, double _r) : base(_r)
        {
            col = _col;
            r = _r;
            s = 4 * Math.PI * r * r;//вычисление площади
            v = 4 / 3 * Math.PI * r * r * r; //вычисление объема
        }
    }
}
