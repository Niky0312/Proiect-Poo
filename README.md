# Proiect-Poo
Proiect Programare orientata pe obiecte 2024-2025

using System;
using System.Collections.Generic;

namespace AplicatieDeplasari
{
    public class CerereDeplasare
    {
        public int Id { get; set; }
        public string Destinatie { get; set; }
        public string Motiv { get; set; }
        public DateTime Data { get; set; }
        public int Durata { get; set; }
        public decimal Buget { get; set; }
        public string Detalii { get; set; }
        public string Tip { get; set; }
        public bool EsteAprobata { get; set; } = false;

        public override string ToString()
        {
            return $"ID: {Id}, Destinație: {Destinatie}, Motiv: {Motiv}, Data: {Data.ToShortDateString()}, Durată: {Durata} zile, Buget: {Buget} lei, Tip: {Tip}, Aprobata: {EsteAprobata}";
        }
    }
    
        static void CreeazaCerere()
        {
            Console.WriteLine("\n--- Creare Cerere ---");
            Console.Write("Destinație: ");
            string destinatie = Console.ReadLine();

            Console.Write("Motiv: ");
            string motiv = Console.ReadLine();

            Console.Write("Data (YYYY-MM-DD): ");
            DateTime data;
            while (!DateTime.TryParse(Console.ReadLine(), out data))
            {
                Console.Write("Format invalid. Introdu data în formatul YYYY-MM-DD: ");
            }

            Console.Write("Durată (în zile): ");
            int durata;
            while (!int.TryParse(Console.ReadLine(), out durata) || durata <= 0)
            {
                Console.Write("Durata trebuie să fie un număr pozitiv. Încearcă din nou: ");
            }

            Console.Write("Buget (lei): ");
            decimal buget;
            while (!decimal.TryParse(Console.ReadLine(), out buget) || buget <= 0)
            {
                Console.Write("Bugetul trebuie să fie un număr pozitiv. Încearcă din nou: ");
            }

            Console.Write("Detalii suplimentare: ");
            string detalii = Console.ReadLine();

            Console.Write("Tip deplasare (Interna/Externa): ");
            string tip = Console.ReadLine();
            while (tip.ToLower() != "interna" && tip.ToLower() != "externa")
            {
                Console.Write("Tip invalid. Introdu 'Interna' sau 'Externa': ");
                tip = Console.ReadLine();
            }

            cereri.Add(new CerereDeplasare
            {
                Id = idCounter++,
                Destinatie = destinatie,
                Motiv = motiv,
                Data = data,
                Durata = durata,
                Buget = buget,
                Detalii = detalii,
                Tip = tip
            });

            Console.WriteLine("Cererea a fost creată cu succes!");
        }

        static void VizualizeazaSiModificaCereri()
        {
            Console.WriteLine("\n--- Vizualizare Cereri ---");

            // Filtrarea cererilor neaprobate
            var cereriNeaprobate = cereri.FindAll(c => !c.EsteAprobata);

            if (cereriNeaprobate.Count == 0)
            {
                Console.WriteLine("Nu există cereri neaprobate.");
                return;
            }

            foreach (var cerere in cereriNeaprobate)
            {
                Console.WriteLine(cerere);
            }

            Console.Write("Dorești să modifici o cerere? (da/nu): ");
            if (Console.ReadLine().ToLower() == "da")
            {
                Console.Write("Introdu ID-ul cererii: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("ID invalid. Introdu un număr valid: ");
                }

                var cerere = cereri.Find(c => c.Id == id);

                if (cerere == null || cerere.EsteAprobata)
                {
                    Console.WriteLine("Cererea nu există sau este deja aprobată.");
                    return;
                }

                Console.Write("Noua destinație (lăsați gol pentru a păstra): ");
                string destinatie = Console.ReadLine();
                if (!string.IsNullOrEmpty(destinatie)) cerere.Destinatie = destinatie;

                Console.Write("Noul motiv (lăsați gol pentru a păstra): ");
                string motiv = Console.ReadLine();
                if (!string.IsNullOrEmpty(motiv)) cerere.Motiv = motiv;

                Console.WriteLine("Cererea a fost actualizată!");
            }
        }
    }
}
