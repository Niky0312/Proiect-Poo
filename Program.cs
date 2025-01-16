namespace Proiect_Poo;


{
    class Program
    {
        static List<CerereDeplasare> cereri = new List<CerereDeplasare>();
        static List<CerereDecontare> cereriDecontare = new List<CerereDecontare>();
        static int idCounter = 1;

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Aplicație Deplasări ---");
                Console.WriteLine("1. Creare cerere de deplasare");
                Console.WriteLine("2. Vizualizare/Modificare cereri");
                Console.WriteLine("3. Creare cerere de decontare");
                Console.WriteLine("4. Vizualizare cereri de decontare");
                Console.WriteLine("5. Vizualizare cereri ca manager");
                Console.WriteLine("6. Iesire");
                Console.Write("Alege o opțiune: ");
                string optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        CreeazaCerere();
                        break;
                    case "2":
                        VizualizeazaSiModificaCereri();
                        break;
                    case "3":
                        CreeazaCerereDecontare();
                        break;
                    case "4":
                        VizualizeazaCereriDecontare();
                        break;
                    case "5":
                        VizualizeazaCereriCaManager();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Opțiune invalidă. Încercați din nou.");
                        break;
                }
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

    Console.Write("\nDorești să modifici o cerere? (da/nu): ");
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

        
        Console.WriteLine("Modificare cerere:");

        Console.Write("Noua destinație (lăsați gol pentru a păstra): ");
        string destinatie = Console.ReadLine();
        if (!string.IsNullOrEmpty(destinatie)) cerere.Destinatie = destinatie;

        Console.Write("Noul motiv (lăsați gol pentru a păstra): ");
        string motiv = Console.ReadLine();
        if (!string.IsNullOrEmpty(motiv)) cerere.Motiv = motiv;

        Console.Write("Noua durată (lăsați gol pentru a păstra): ");
        string durataInput = Console.ReadLine();
        if (int.TryParse(durataInput, out int durata) && durata > 0)
        {
            cerere.Durata = durata;
        }

        Console.Write("Noua valoare a bugetului (lăsați gol pentru a păstra): ");
        string bugetInput = Console.ReadLine();
        if (decimal.TryParse(bugetInput, out decimal buget) && buget > 0)
        {
            cerere.Buget = buget;
        }

        Console.Write("Noi detalii suplimentare (lăsați gol pentru a păstra): ");
        string detalii = Console.ReadLine();
        if (!string.IsNullOrEmpty(detalii)) cerere.Detalii = detalii;

        Console.Write("Nou tip de deplasare (Interna/Externa, lăsați gol pentru a păstra): ");
        string tip = Console.ReadLine();
        if (!string.IsNullOrEmpty(tip) && (tip.ToLower() == "interna" || tip.ToLower() == "externa"))
        {
            cerere.Tip = tip;
        }

        Console.WriteLine("Cererea a fost actualizată!");
    }
        }

        
        static void CreeazaCerereDecontare()
        {
            Console.WriteLine("\n--- Creare Cerere de Decontare ---");
            Console.Write("Introdu ID-ul cererii de deplasare aprobată pentru decontare: ");
            int idCerereDeplasare;
            while (!int.TryParse(Console.ReadLine(), out idCerereDeplasare))
            {
                Console.Write("ID invalid. Introdu un număr valid: ");
            }

            var cerereDeplasare = cereri.Find(c => c.Id == idCerereDeplasare && c.EsteAprobata);
            if (cerereDeplasare == null)
            {
                Console.WriteLine("Cererea de deplasare nu există sau nu este aprobată.");
                return;
            }

            var cerereDecontare = new CerereDecontare { CerereDeplasareId = idCerereDeplasare };

            
            Console.Write("Adăugați documente justificative (transport, masă, cazare). Introduceți 'gata' pentru a încheia: ");
            while (true)
            {
                string document = Console.ReadLine().ToLower();
                if (document == "gata") break;

                if (document == "transport" || document == "masă" || document == "cazare")
                {
                    cerereDecontare.AdaugaDocument(document);
                }
                else
                {
                    Console.WriteLine("Document invalid. Adaugă 'transport', 'masă' sau 'cazare'.");
                }
            }

            cereriDecontare.Add(cerereDecontare);
            Console.WriteLine("Cererea de decontare a fost creată cu succes!");
        }

        
        static void VizualizeazaCereriDecontare()
        {
            Console.WriteLine("\n--- Vizualizare Cereri de Decontare ---");

            foreach (var cerere in cereriDecontare)
            {
                Console.WriteLine($"Cerere ID deplasare: {cerere.CerereDeplasareId}, Documente: {string.Join(", ", cerere.Documente)}, Aprobata: {cerere.EsteAprobata}");
            }
        }

        
        static void VizualizeazaCereriCaManager()
        {
            Console.WriteLine("\n--- Vizualizare Cereri ca Manager ---");

            foreach (var cerere in cereri)
            {
                Console.WriteLine(cerere);
            }

            Console.Write("Dorești să aprobi o cerere? (da/nu): ");
            if (Console.ReadLine().ToLower() == "da")
            {
                Console.Write("Introdu ID-ul cererii: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("ID invalid. Introdu un număr valid: ");
                }

                var cerere = cereri.Find(c => c.Id == id);
                if (cerere == null)
                {
                    Console.WriteLine("Cererea nu există.");
                    return;
                }

                cerere.EsteAprobata = true;
                Console.WriteLine("Cererea a fost aprobată.");
            }
        }
    }

    public class CerereDecontare
    {
        public int CerereDeplasareId { get; set; }
        public List<string> Documente { get; set; } = new List<string>(); 
        public bool EsteAprobata { get; set; } = false;

        public void AdaugaDocument(string tipDocument)
        {
            Documente.Add(tipDocument);
        }
    }
}
    
