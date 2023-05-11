using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Lotto
{
    class Lotto
    {

        static int dzien = 1;
        static Random rnd = new Random();
        static double pula = Math.Round(rnd.Next(1, 35) * 1000000 * rnd.NextDouble(), 2);
        public static int portfel = 25;
        static ConsoleKey wybor;
        public static List<int[]> kupon = new List<int[]>();

        static void Main(string[] args)
        {

            Kupon los = new Kupon();

            do
            {
                Console.Clear();
                Console.WriteLine("\nDZIEŃ: {0}", dzien);
                Console.WriteLine("Witaj w grze LOTTO, dziś do wygrania jest aż {0} zł ", pula);
                Console.WriteLine("Twój stan konta wynosi: {0} zł\n", portfel);

                Console.WriteLine("Twoje aktualne kupony:");
                los.Wyswietl_Kupon(kupon);

                Console.WriteLine("\n\nWybierz co chcesz zrobić: ");
                Console.WriteLine("1. Postaw kupon (3 zł)");
                Console.WriteLine("2. Sprawdź kupon");
                Console.WriteLine("3. Wyświetl kupon");
                Console.WriteLine("4. Wyjdź (lub wciśnij escape)\n");

                Console.Write("Twój wybór: ");
                wybor = Console.ReadKey().Key;

                
                
                switch (wybor)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        kupon.Add(los.PostawKupon());
                        

                        //  Console.ReadKey();
                        break;


                    case ConsoleKey.D2:
                        Console.WriteLine("Wybrano 2 - Sprawdź Kupon");
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine("Wybrano 3 - Wyświetl Kupon");


                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine("Wybrano 4");
                        break;

                }


            } while (!(wybor == ConsoleKey.Escape || wybor == ConsoleKey.D4));


        }
    }


    class Kupon : Lotto
    {

        public int[] PostawKupon()
        {
            int[] liczby = new int[6];
            int ilosc_zakladow = 1;
            //metoda do wpisania 6 liczb w tablice i wrzucenie tego do listy

            Console.WriteLine("\nWybrano 1 - Postaw kupon");
            Console.WriteLine("Kupon może się składać z maksymalnie 8 zakładów");
            Console.WriteLine("Ile zakładów chcesz postawić? (1-8)");

            do
            {
                try
                {
                    ilosc_zakladow = Convert.ToInt32(Console.ReadLine()); ///// obsluga prostego wyjątku
                }
                catch (Exception)
                {
                    Console.WriteLine("Podaj liczbę zamiast znaku");
                    Console.ReadKey();
                    Console.Clear();
                    PostawKupon();
                }

                if (ilosc_zakladow < 1 || ilosc_zakladow > 8)
                    Console.WriteLine("Wybrano niepoprawną liczbę!");
                else if (ilosc_zakladow * 3 > portfel)
                {
                    Console.WriteLine("Nie masz tyle siana byku");
                }
                else
                {

                    portfel = portfel - (ilosc_zakladow * 3);
                    PobierzLiczby(ilosc_zakladow, liczby);
                    

                }

            } while (!(ilosc_zakladow > 0 && ilosc_zakladow <= 8));

            return liczby;
        }

        private void PobierzLiczby(int ilosc, int[] liczby)
        {

            int liczba;
            
            for (int i = 0; i < ilosc; i++)
            {
               if (i > 0) kupon.Add(liczby);

                for (int j = 0; j < liczby.Length; j++)
                {

                    do
                    {

                        Console.Clear();
                        Console.WriteLine("Zaklad {0}/{1}\n", i + 1, ilosc);
                        Console.WriteLine("Twoje numery: ");
                        foreach (int l in liczby)
                        {
                            if (l > 0)
                            {
                                Console.Write(l + " ");
                            }
                        }

                        Console.Write("\nPodaj liczbę od 1 do 49\n{0}/6: ", j + 1);
                        if (!int.TryParse(Console.ReadLine(), out liczba))      //obsługa podobnej sytuacji jak w wyjątku, ale za pomocą metody TryParse
                        {
                            Console.WriteLine("To nie jest liczba!");
                            Console.ReadKey();
                            continue;
                        }
                        if (liczba < 1 || liczba > 49)
                        {
                            Console.WriteLine("Liczba musi być z zakresu 1-49");
                            Console.ReadKey();
                            continue;
                        }
                        if (Array.IndexOf(liczby, liczba) != -1)
                        {
                            Console.WriteLine("Ta liczba już wystąpiła na kuponie");
                            Console.ReadKey();
                            continue;
                        }
                        else

                        {
                            liczby[j] = liczba;    
                            break;
                        }

                        
                    } while (true);
                   
                }
               
            }

        }

        public void Wyswietl_Kupon(List<int[]> kupon)
        {

            if (kupon.Count == 0)
            {
                Console.WriteLine("Nie postawiłeś jeszcze żadnych losów");
            }
            else
            {
                int i = 0;
                foreach (int[] tablica in kupon)
                {
                    i++;
                    Console.Write(i + ": ");
                    foreach (int liczba in tablica)
                    {
                        Console.Write("{0} ", liczba);
                    }
                    Console.WriteLine();

                }
            }
        }


    }
}