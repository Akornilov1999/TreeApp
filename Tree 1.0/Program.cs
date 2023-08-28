using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tree
{
    static class Menu
    {
        public static void Read_of_file(ref Tree tree)
        {
            Console.Clear();
            string f;
            bool b = false;
            while (b == false)
            {
                FileStream fin;
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Укажите расположение раннее сохраненного файла без его названия и расширения (для пропуска введите '0') и нажмите калвишу Enter: ");
                    f = Console.ReadLine();
                    if (f == "0")
                    {
                        b = true;
                        break;
                    }
                    f += "\\Inf.txt";
                    b = true;
                    fin = new FileStream(f, FileMode.Open);
                    fin.Close();
                    if (fin != null)
                    {
                        fin = new FileStream(f, FileMode.Open);
                        StreamReader fstr_in = new StreamReader(fin);
                        int i = 0;
                        string str;
                        Tree inf = null;
                        while ((str = fstr_in.ReadLine()) != null)
                        {
                            if (i == 0)
                            {
                                inf = new Tree();
                                if (str != "0" && str != "")
                                    inf.Name = str;
                                else
                                {
                                    tree.Clear();
                                    inf.Clear();
                                    b = false;
                                    break;
                                }
                            }
                            if (i == 1)
                            {
                                if (str != "0" && str != "")
                                    inf.Drug.country = str;
                                else
                                {
                                    tree.Clear();
                                    inf.Clear();
                                    b = false;
                                    break;
                                }
                            }
                            if (i == 2)
                                if (!UInt16.TryParse(str, out inf.Drug.year) || inf.Drug.year < 2000 || inf.Drug.year > DateTime.Now.Year)
                                {
                                    tree.Clear();
                                    inf.Clear();
                                    b = false;
                                    break;
                                }
                            if (i == 3)
                            {
                                if (inf.Drug.year < DateTime.Now.Year)
                                {
                                    if (!UInt16.TryParse(str, out inf.Drug.month) || inf.Drug.month < 1 || inf.Drug.month > 12)
                                    {
                                        tree.Clear();
                                        inf.Clear();
                                        b = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!UInt16.TryParse(str, out inf.Drug.month) || inf.Drug.month < 1 || inf.Drug.month > DateTime.Now.Month)
                                    {
                                        tree.Clear();
                                        inf.Clear();
                                        b = false;
                                        break;
                                    }
                                }
                            }
                            if (i == 4)
                                if (!UInt16.TryParse(str, out inf.Drug.date) || inf.Drug.date < 12 || inf.Drug.count > 60)
                                {
                                    tree.Clear();
                                    inf.Clear();
                                    b = false;
                                    break;
                                }
                            if (i == 5)
                                if (!UInt16.TryParse(str, out inf.Drug.count) || inf.Drug.count < 100 || inf.Drug.count > 20000)
                                {
                                    tree.Clear();
                                    inf.Clear();
                                    b = false;
                                    break;
                                }
                            if (i == 6)
                            {
                                if (str != "0")
                                {
                                    tree.Clear();
                                    inf.Clear();
                                    b = false;
                                    break;
                                }
                            }
                            if (i < 6)
                                i++;
                            else
                            {
                                if (tree.Drug != null)
                                {
                                    tree.Add(ref inf);
                                    inf.Clear();
                                }
                                else
                                {
                                    Menu.Show_error_open_file();
                                }
                                i = 0;
                            }
                        }
                        fstr_in.Close();
                    }
                    fin.Close();
                }
                catch (UnauthorizedAccessException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
                catch (IOException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
                catch (ArgumentException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
                catch (System.NotSupportedException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
            }
        }

        public static void Add(ref Tree tree)
        {
            string t = null;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Введите название препарата (или '0', если хотите выйти) и нажмите клавишу Enter: ");
                t = Console.ReadLine();
                if (t != "0" && t != "")
                {
                    if (tree == null)
                        tree = new Tree();
                    tree.Add(t);
                }
            } while (t != "0");
        }

        public static void Change(ref Tree tree)
        {
            string name;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Введите название препарата, информацию о котором вы хотите изменить (для выхода введите '0' и нажмите клавишу Enter:");
                tree.Cent_travel();
                Console.Write("Изменяем - ");
                name = Console.ReadLine();
                if (name != "0" && name != null && name != "")
                {
                    Tree change_drug = tree.Search(name);
                    if (change_drug != null)
                    {
                        Tree t = new Tree();
                        Console.Clear();
                        bool b = true, temp = false;
                        UInt16 i = 0;
                        while (i <= 5 && b == true)
                        {
                            string[] msg = { "Название препарата - " + change_drug.Name, "Страна-изготовитель препарата \"" + t.Name + "\" - " + change_drug.Drug.country, "Год изготовления препарата \"" + t.Name + "\" - " + change_drug.Drug.year, "Месяц изготовления препарата \"" + t.Name + "\" - " + change_drug.Drug.month, "Срок годности препарарта \"" + t.Name + "\" в месяцах - " + change_drug.Drug.date, "Количество препаратов \"" + t.Name + "\" - " + change_drug.Drug.count };
                            string ch;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine(msg[i] + ". Если хотите изменить, введите '+' (в противном случае '-') и нажмите клавишу Enter: ");
                                ch = Console.ReadLine();
                            } while (ch != "+" && ch != "-");
                            if (b != false)
                            {
                                if (ch == "-")
                                {
                                    i++;
                                    if (i == 1 && ch == "-")
                                        t.Name = change_drug.Name;
                                    if (i == 2 && ch == "-")
                                        t.Drug.country = change_drug.Drug.country;
                                    if (i == 3 && ch == "-")
                                        t.Drug.year = change_drug.Drug.year;
                                    if (i == 4 && ch == "-")
                                        t.Drug.month = change_drug.Drug.month;
                                    if (i == 5 && ch == "-")
                                        t.Drug.date = change_drug.Drug.date;
                                    if (i == 6 && ch == "-")
                                        t.Drug.count = change_drug.Drug.count;
                                    if (i != 6)
                                        continue;
                                }
                            }
                            if (i == 0)
                            {
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("Название препарата \"" + change_drug.Name + "\".");
                                    Console.WriteLine("Введите новое название препарата, отличающееся от старого (для выхода введите '0') и нажмите клавишу Enter: ");
                                    t.Name = Console.ReadLine();
                                    if (t.Name == "0")
                                    {
                                        b = false;
                                        break;
                                    }
                                } while (t.Name == change_drug.Name || t.Name == "" || t.Name == null);
                                if (b != false)
                                {
                                    temp = true;
                                    i++;
                                    continue;
                                }
                            }
                            if (i == 1)
                            {
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("Страна-изготовитель препарата \"" + t.Name + "\" - \"" + change_drug.Drug.country + "\".");
                                    Console.WriteLine("Введите новую страну-иззготовитель препарата, отличающуюся от старого (для выхода введите '0') и нажмите клавишу Enter: ");
                                    t.Drug.country = Console.ReadLine();
                                    if (t.Drug.country == "0")
                                    {
                                        b = false;
                                        break;
                                    }
                                } while (t.Drug.country == change_drug.Drug.country || t.Drug.country == "" || t.Drug.country == null);
                                if (b != false)
                                {
                                    temp = true;
                                    i++;
                                    continue;
                                }
                            }
                            if (i == 2)
                            {
                                string year;
                                do
                                {
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Год изготовления препарата \"" + t.Name + "\" - " + change_drug.Drug.year + ".");
                                        Console.WriteLine("Введите новый год изготовления препарата (натуральное четырехзначное число от 2000 до " + DateTime.Now.Year + "), отличающийся от старого (для выхода введите '0') и нажмите клавишу Enter: ");
                                        year = Console.ReadLine();
                                    } while ((year == "" || year == null) && year != "0");
                                    if (year == "0")
                                    {
                                        b = false;
                                        break;
                                    }
                                } while (!UInt16.TryParse(year, out t.Drug.year) || (t.Drug.year > DateTime.Now.Year) || (t.Drug.year < 2000) || (change_drug.Drug.year == t.Drug.year));
                                if (b != false)
                                {
                                    temp = true;
                                    i++;
                                    continue;
                                }
                            }
                            if (i == 3)
                            {
                                int month;
                                if (t.Drug.year == DateTime.Now.Year)
                                    month = DateTime.Now.Month;
                                else
                                    month = 12;
                                string month_;
                                do
                                {
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Месяц изготовления препарата \"" + t.Name + "\" - " + change_drug.Drug.month + ".");
                                        Console.WriteLine("Введите новый месяц изготовления препарата (натуральное число от 1 до " + month + "), отличающийся от старого (для выхода введите '0') и нажмите клавишу Enter: ");
                                        month_ = Console.ReadLine();
                                    } while ((month_ == "" || month_ == null) && month_ != "0");
                                    if (month_ == "0")
                                    {
                                        b = false;
                                        break;
                                    }
                                } while (!UInt16.TryParse(month_, out t.Drug.month) || (t.Drug.month > month) || (t.Drug.month < 1) || (change_drug.Drug.month == t.Drug.month));
                                if (b != false)
                                {
                                    temp = true;
                                    i++;
                                    continue;
                                }
                            }
                            if (i == 4)
                            {
                                string date;
                                do
                                {
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Срок годности препарата \"" + t.Name + "\" - " + change_drug.Drug.date + " месяцев.");
                                        Console.WriteLine("Введите новый срок годности препарата в месяцах (натуральное число от 12 до 60), отличающийся от старого (для выхода введите '0') и нажмите клавишу Enter: ");
                                        date = Console.ReadLine();
                                    } while ((date == "" || date == null) && date != "0");
                                    if (date == "0")
                                    {
                                        b = false;
                                        break;
                                    };
                                } while (!UInt16.TryParse(date, out t.Drug.date) || (t.Drug.date > 60) || (t.Drug.date < 12) || (t.Drug.date == change_drug.Drug.date));
                                if (b != false)
                                {
                                    temp = true;
                                    i++;
                                    continue;
                                }
                            }
                            if (i == 5)
                            {
                                string count;
                                do
                                {
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Количество препаратов \"" + t.Name + "\" - " + change_drug.Drug.date + ".");
                                        Console.WriteLine("Введите новое количество препаратов (натуральное число от 100 до 20000), отличающееся от старого (для выхода введите '0') и нажмите клавишу Enter: ");
                                        count = Console.ReadLine();
                                    } while ((count == "" || count == null) && count != "0");
                                    if (count == "0")
                                    {
                                        b = false;
                                        break;
                                    };
                                } while (!UInt16.TryParse(count, out t.Drug.count) || (t.Drug.count > 20000) || (t.Drug.count < 100) || (t.Drug.count == change_drug.Drug.count));
                                if (b != false)
                                {
                                    temp = true;
                                    i++;
                                }
                            }
                            if (b == true && temp != false)
                            {
                                Console.Clear();
                                Console.WriteLine("Информация о препарате \"" + change_drug.Name + "\"успешно изменена!");
                                tree = Tree.Delete(ref tree, change_drug.Name);
                                Menu.Show_load(3);
                                Tree.Unite(ref t, ref tree, false);
                            }
                        }
                    }
                }
            } while (name != "0");
        }

        public static void Show(ref Tree tree)
        {
            string name;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Введите название препарата, информацию о котором вы хотите просмотреть (для выхода введите \'0\') и нажмите клавишу Enter:");
                tree.Cent_travel();
                Console.Write("Ищем - ");
                name = Console.ReadLine();
                if (name != "0" && name != null && name != "")
                {
                    Tree t = tree.Search(name);
                    if (t != null)
                    {
                        Console.Clear();
                        t.Show_drug();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Для выхода нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
            } while (name != "0");

        }

        public static void Delete(ref Tree tree)
        {
            Console.Clear();
            string name;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Введите название препарата, который хотите удалить (для выхода введите '0') и нажмите клавишу Enter:");
                tree.Cent_travel();
                Console.Write("Удаляем - ");
                name = Console.ReadLine();
                if (name != "0" && name != null && name != "")
                {
                    Tree t = tree.Search(name);
                    if (t != null)
                    {
                        Console.Clear();
                        tree = Tree.Delete(ref tree, t.Name);
                        Console.WriteLine("Препарат \"" + name + "\" успешно удален из базы данных!");
                        Menu.Show_load(3);
                    }
                }
            } while (name != "0" && tree != null);
        }

        public static void Write_in_file(ref Tree tree)
        {
            Console.Clear();
            string f;
            bool b = false;
            while (b == false)
            {
                FileStream fout;
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Укажите путь для сохранения файла (без названия и расширения файла) и нажмите клавишу Enter: ");
                    f = Console.ReadLine();
                    f += "\\Inf.txt";
                    b = true;
                    fout = new FileStream(f, FileMode.Create);
                    fout.Close();
                    if (fout != null)
                    {
                        fout = new FileStream(f, FileMode.Create);
                        StreamWriter fstr_out = new StreamWriter(fout);
                        tree.Travel_fout(tree.Name, ref fstr_out);
                        fstr_out.Close();
                        Console.Clear();
                        Console.WriteLine("Информация о препаратах успешно сохранена в файл!");
                        Menu.Show_load(3);
                    }
                    fout.Close();
                }
                catch (UnauthorizedAccessException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
                catch (IOException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
                catch (ArgumentException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
                catch (System.NotSupportedException exc)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.Message);
                    Menu.Show_load(3);
                    b = false;
                }
            }
        }

        public static void Show_error_open_file()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка при записи данных из файла! Воможно файл был поврежден!");
            Menu.Show_load(6);
        }

        public static void Show_hello()
        {
            Console.SetWindowSize(150, 30);
            Console.Title = "Приветствие";
        }

        public static void Show_message(string str, int w, int x, int y)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            int i;
            int j = w / 2 - str.Length / 2;
            for (i = 0; i < j; i++)
            {
                Console.Write("_");
                System.Threading.Thread.Sleep(10);
            }
            for (i = j; i < j + str.Length; i++)
            {
                Console.Write(str[i - j]);
                System.Threading.Thread.Sleep(100);
            }
            for (i = j + str.Length; i < w; i++)
            {
                if (i != w - 1)
                    System.Threading.Thread.Sleep(10);
                Console.Write("_");
            }
            Console.SetCursorPosition(x, y);
            if (i == w)
            {
                for (i = 0; i < w; i++)
                {
                    if (i != w - 1)
                        System.Threading.Thread.Sleep(10);
                    Console.Write(" ");
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = true;
        }

        public static void Show_inf()
        {
            Console.Title = "Московский Авиационный Институт | Группа М3О-235Б-17 | Корнилов А. Н. | Создание и обслуживание динамической структуры данных \"Банирное дерево\"";
        }

        public static void Show_load(int t)
        {
            Console.CursorVisible = false;
            Console.Title = "";
            for (int i = t; i > 0; i--)
            {
                Console.Title = "Подождите " + i + " сек.";
                System.Threading.Thread.Sleep(1000);
            }
            Menu.Show_inf();
            Console.CursorVisible = true;
        }
    }

    class Tree
    {
        string name;
        Leave drug;
        Tree left;
        Tree right;

        public Tree()
        {
            Drug = new Leave();
        }

        public class Leave
        {
            public string country;
            public UInt16 year;
            public UInt16 month;
            public UInt16 date;
            public UInt16 count;

            public Leave()
            {
                country = null;
                year = 0;
                month = 0;
                date = 0;
                count = 0;
            }
        }

        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }

        public Leave Drug
        {
            set
            {
                drug = value;
            }

            get
            {
                return drug;
            }
        }

        public void Set_drug()
        {
            do
            {
                Console.Clear();
                Console.WriteLine(name + ":");
                Console.WriteLine("Введите название страны-изготовитель (например, Германия): ");
                Drug.country = Console.ReadLine();
            }
            while (Drug.country == "");
            do
            {
                Console.Clear();
                Console.WriteLine(name + ":");
                Console.WriteLine("Страна-изготовитель - " + Drug.country);
                Console.WriteLine("Введите год изготовления препарата (натуральное четырехзначное число, от 2000 до " + DateTime.Now.Year + "): ");
            } while (!UInt16.TryParse(Console.ReadLine(), out Drug.year) || (Drug.year > DateTime.Now.Year) || (Drug.year < 2000));
            if (Drug.year < DateTime.Now.Year)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(name + ":");
                    Console.WriteLine("Страна-изготовитель - " + Drug.country);
                    Console.WriteLine("Год изготовления препарата - " + Drug.year);
                    Console.WriteLine("Введите месяц изготовления препарата (натуральное число от 1 до 12): ");
                } while (!UInt16.TryParse(Console.ReadLine(), out Drug.month) || ((Drug.month > 12) || (Drug.month < 1)));
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(name + ":");
                    Console.WriteLine("Страна-изготовитель - " + Drug.country);
                    Console.WriteLine("Год изготовления препарата - " + Drug.year);
                    Console.WriteLine("Введите месяц изготовления препарата (натуральное число, не больше, чем " + DateTime.Now.Month + "): ");
                } while (!UInt16.TryParse(Console.ReadLine(), out Drug.month) || ((Drug.month > DateTime.Now.Month) || (Drug.month < 1)));

            }
            do
            {
                Console.Clear();
                Console.WriteLine(name + ":");
                Console.WriteLine("Страна-изготовитель - " + Drug.country);
                Console.WriteLine("Дата изготовления препарата (месц.год) - " + Drug.month + "." + Drug.year);
                Console.WriteLine("Введите срок годности препарата в месцах (натуральное число от 12 до 60): ");
            } while (!UInt16.TryParse(Console.ReadLine(), out Drug.date) || (Drug.date > 60) || (Drug.date < 12));
            do
            {
                Console.Clear();
                Console.WriteLine(name + ":");
                Console.WriteLine("Страна-изготовитель - " + Drug.country);
                Console.WriteLine("Дата изготовления препарата (месц.год) - " + Drug.month + "." + Drug.year);
                Console.WriteLine("Срок годности препарата в месяцах - " + Drug.date);
                Console.WriteLine("Введите количество препаратов (натуральное число от 100 до 20 000): ");
            } while (!UInt16.TryParse(Console.ReadLine(), out Drug.count) || (Drug.count < 100) || (Drug.count > 20000));
            Console.Clear();
                Console.WriteLine("Препарат \"" + name + "\" успешно добавлен в базу данных!");
                Menu.Show_load(3);
        }

        public void Show_drug()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Название препарата - " + "\"" + name + "\"");
            Console.WriteLine("Страна-изготовитель - " + "\"" + Drug.country + "\"");
            Console.WriteLine("Дата изготовления (месяц.год) - " + Drug.month + "." + Drug.year);
            Console.WriteLine("Срок годности - " + Drug.date);
            Console.WriteLine("Количество - " + Drug.count);
        }

        public void Add(ref Tree inf)
        {
            if (this.name == null)
            {
                this.name = inf.name;
                this.Drug = inf.Drug;
                if (Console.ForegroundColor == ConsoleColor.White)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Препарат \"" + inf.name + "\" успешно добавлен в базу данных!");
                    Menu.Show_load(1);
                }
            }
            else
            {
                if (this.name.CompareTo(inf.name) == 1)
                {
                    if (left == null)
                        this.left = new Tree();
                    left.Add(ref inf);
                }
                else if (this.name.CompareTo(inf.name) == -1)
                {
                    if (right == null)
                        this.right = new Tree();
                    right.Add(ref inf);
                }
                else
                {
                    if (Console.ForegroundColor == ConsoleColor.White)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Препарат с названием \"" + inf.Name + "\" уже существует!");
                        Menu.Show_load(1);
                    }
                    inf = null;
                }
            }
        }

        public void Add(string name)
        {
            if (this.name == null)
            {
                this.name = name;
                Set_drug();
            }
            else
            {
                if (this.name.CompareTo(name) == 1)
                {
                    if (left == null)
                        this.left = new Tree();
                    left.Add(name);
                }
                else if (this.name.CompareTo(name) == -1)
                {
                    if (right == null)
                        this.right = new Tree();
                    right.Add(name);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Препарат с названием \"" + name + "\" уже существует!");
                    Menu.Show_load(3);
                }
            }
        }

        public static Tree Delete(ref Tree tree, string name)
        {
            if (tree == null)
            {
                return tree;
            }
            if (name == tree.name)
            {
                Tree tmp = new Tree();
                if (tree.right == null)
                    tmp = tree.left;
                else
                {

                    Tree ptr = tree.right;
                    if (ptr.left == null)
                    {
                        ptr.left = tree.left;
                        tmp = ptr;
                    }
                    else
                    {

                        Tree pmin = ptr.left;
                        while (pmin.left != null)
                        {
                            ptr = pmin;
                            pmin = ptr.left;
                        }
                        ptr.left = pmin.right;
                        pmin.left = tree.left;
                        pmin.right = tree.right;
                        tmp = pmin;
                    }
                }

                tree = null;
                return tmp;

            }
            else if (name.CompareTo(tree.name) == -1)
                tree.left = Delete(ref tree.left, name);
            else
                tree.right = Delete(ref tree.right, name);
            return tree;
        }

        public void Clear()
        {
            this.name = null;
            if (Drug != null)
                Drug = null;
            left = null;
            right = null;
        }

        public void Cent_travel()
        {
            if (left != null && left.name != null)
                left.Cent_travel();
            if (this.name != null)
                Console.WriteLine("\"" + this.name + "\"");
            if (right != null && right.name != null)
                right.Cent_travel();
        }

        public Tree Search(string name)
        {
            if (this == null || this.name == null)
                return null;
            if (this.name == name)
                return this;
            else if (this.name.CompareTo(name) == 1)
            {
                if (left != null)
                {
                    if (left.name != null)
                    {
                        return this.left.Search(name);
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Препарат \"" + name + "\" в базе данных отсутствует!");
                        Menu.Show_load(3);
                        return null;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Препарат \"" + name + "\" в базе данных отсутствует!");
                    Menu.Show_load(3);
                    return null;
                }
            }
            else
            {
                if (right != null)
                {
                    if (right.name != null)
                    {
                        return this.right.Search(name);
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Препарат \"" + name + "\" в базе данных отсутствует!");
                        Menu.Show_load(4);
                        return null;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Препарат \"" + name + "\" в базе данных отсутствует!");
                    Menu.Show_load(4);
                    return null;
                }
            }
        }

        public static void Unite(ref Tree root1, ref Tree root2, bool b)
        {
            if (root1.left != null)
            {
                if (b == true)
                    Unite(ref root1.left, ref root2, true);
                else
                    Unite(ref root1.left, ref root2, false);
            }
            if (root1.right != null)
            {
                if (b == true)
                    Unite(ref root1.right, ref root2, true);
                else
                    Unite(ref root1.right, ref root2, false);
            }
            if (b == true)
                Console.ForegroundColor = ConsoleColor.White;
            root2.Add(ref root1);
            root1 = null;
        }

        public void Travel_fout(string fout, ref StreamWriter fstr_out)
        {
            if (this != null)
            {
                if (this.name != null)
                {
                    fstr_out.WriteLine(name);
                    fstr_out.WriteLine(Drug.country);
                    fstr_out.WriteLine(Drug.year);
                    fstr_out.WriteLine(Drug.month);
                    fstr_out.WriteLine(Drug.date);
                    fstr_out.WriteLine(Drug.count);
                    fstr_out.WriteLine("0");
                    if (left != null)
                        if (left.name != null)
                            left.Travel_fout(fout, ref fstr_out);
                    if (right != null)
                        if (right.name != null)
                            right.Travel_fout(fout, ref fstr_out);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Menu.Show_hello();
            int w = Console.WindowWidth;
            Tree tree = new Tree();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Menu.Show_message("Московский Авиационный Институт", w, 0, 6);
            Menu.Show_message("Группа М30-235Б-17", w, 0, 6);
            Menu.Show_message("Корнилов А. Н.", w, 0, 6);
            Menu.Show_message("Cоздание и обслуживание динамической структуры данных \"Бинарное дерево\"", w, 0, 6);
            Console.Clear();
            UInt16 t = 9;
            int time;
            time = DateTime.Now.Hour;
            if (time >= 22 || time < 4)
                Menu.Show_message("Доброй ночи!", w, 0, 6);
            if (time >= 4 && time < 10)
                Menu.Show_message("Доброе утро!", w, 0, 6);
            if (time >= 10 && time < 16)
                Menu.Show_message("Добрый день!", w, 0, 6);
            if (time >= 16 && time < 22)
                Menu.Show_message("Добрый вечер!", w, 0, 6);
            System.Console.Clear();
            Menu.Show_inf();
            while (t != 0)
            {
                do
                {
                    System.Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Для добавления перапарата в базу данных введите '1' и нажмите клавишу Enter");
                    Console.WriteLine("Для добавления информации о препаратах из файла введите '2' и нажмите клавишу Enter");
                    if (tree != null)
                    {
                        if (tree.Name != null)
                        {
                            Console.WriteLine("Для изменения сведений о препарате в базе данных введите '3' и нажмите клавишу Enter");
                            Console.WriteLine("Для просмотра сведений о препарате введите '4' и нажмите клавишу Enter");
                            Console.WriteLine("Для удаления препарата из базы данных введите '5' и нажмите клавишу Enter");
                            Console.WriteLine("Для сохранения инфофрамции о препаратх в файл введите '6' и нажмите клавишу Enter");
                            Console.WriteLine("Для удаления базы данных введите '7' и нажмите клавишу Enter");
                        }
                    }
                    Console.WriteLine("Для выхода из базы данных введите '0' и нажмите клавишу Enter");
                    Console.Write("Ваше действие - ");
                } while (!UInt16.TryParse(Console.ReadLine(), out t));
                if (t == 1)
                    Menu.Add(ref tree);
                if (t == 2)
                {
                    Tree tree_t = new Tree();
                    Menu.Read_of_file(ref tree_t);
                    if (tree_t.Name != null)
                    {
                        Tree.Unite(ref tree_t, ref tree, true);
                    }
                }
                if (tree != null)
                {
                    if (tree.Name != null)
                    {
                        if (t == 3)
                            Menu.Change(ref tree);
                        if (t == 4)
                            Menu.Show(ref tree);
                        if (t == 5)
                        {
                            Menu.Delete(ref tree);
                            if (tree == null)
                                tree = new Tree();
                        }
                        if (t == 6)
                            Menu.Write_in_file(ref tree);
                        if (t == 7)
                        {
                            tree.Clear();
                        }
                    }
                }
            }
            Console.Clear();
            Menu.Show_message("До свидания!", w, 0, 6);
        }
    }
};
