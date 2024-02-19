using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAD.ImmoWin.Business;
using AAD.ImmoWin.Data;
using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Services;
using Newtonsoft.Json.Linq;


namespace AAD.ImmoWin.ConsoleApp
{
	internal class Program
	{
		static public Klanten Klanten { get; set; } = new Klanten();

		static private void VulKlanten()
		{
			IKlant klant;
			IWoning woning;

			try
			{
				klant = new Business.Klant("Theo", "Flitser");
				Klanten.Add(klant);

				klant = new Business.Klant("Bert", "Bibber");
				woning = new Business.Huis(Huistype.Rijhuis, new Business.Adres("Stormstraat", 1, 1000, "Brussel"), DateTime.Now, 0);
				klant.Eigendommen.Add(woning);
				Klanten.Add(klant);

				klant = new Business.Klant("Junior", "Warwinkel");
				woning = new Business.Appartement(0, new Business.Adres("Stormstraat", 2, 1000, "Brussel"), DateTime.Now, 0);
				klant.Eigendommen.Add(woning);
				Klanten.Add(klant);

				klant = new Business.Klant("Piet", "Pienter");
				klant.Eigendommen.Add(new Business.Appartement(1, new Business.Adres("Stormstraat", 2, 1000, "Brussel"), DateTime.Now, 0));
				klant.Eigendommen.Add(new Business.Huis(Huistype.Rijhuis, new Business.Adres("Stormstraat", 3, 1000, "Brussel"), DateTime.Now, 0));
				Klanten.Add(klant);

				klant = new Business.Klant("Hilarius", "Warwinkel");
				klant.Eigendommen.Add(new Business.Huis(Huistype.Vrijstaand, new Business.Adres("Stormstraat", 5, 1000, "Brussel"), DateTime.Now, 0));
				klant.Eigendommen.Add(new Business.Appartement(3, new Business.Adres("Stormstraat", 2, 1000, "Brussel"), DateTime.Now, 0));
				klant.Eigendommen.Add(new Business.Appartement(2, new Business.Adres("Stormstraat", 2, 1000, "Brussel"), DateTime.Now, 0));
				klant.Eigendommen.Add(new Business.Appartement(5, new Business.Adres("Stormstraat", 2, 1000, "Brussel"), DateTime.Now, 0));
				klant.Eigendommen.Add(new Business.Huis(Huistype.Tweegevel, new Business.Adres("Stormstraat", 1, 1000, "Brussel"), DateTime.Now, 0));
				klant.Eigendommen.Add(new Business.Appartement(4, new Business.Adres("Stormstraat", 2, 1000, "Brussel"), DateTime.Now, 0));
				Klanten.Add(klant);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			Console.WriteLine("\nKlanten gevuld.");
		}

		static void ToonKlanten()
		{
			Console.WriteLine("\nKlanten:");

			List<IKlant> klantenList = Klanten.ToList<IKlant>();
			klantenList.Sort();

			foreach (IKlant k in klantenList)
			{
				Console.WriteLine($"{k}");
				List<IWoning> eigendommenList = k.Eigendommen.ToList<IWoning>();
				eigendommenList.Sort();
				foreach (IWoning w in eigendommenList)
					Console.WriteLine($"\t{w}");
			}
			Console.WriteLine();
		}


		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Default;

			//VulKlanten();
			//ToonKlanten();
			//TestAdresExceptions();
			//TestPropertyChanged();
			//TestEF();
			//TestBusiRepo();
			testAPI();

            Console.WriteLine("druk op toets...");
            Console.ReadKey(true);
		}

        private static void TestEF()
        {
            ImmoWinContext context = new ImmoWinContext();
			Data.Klant k = new Data.Klant() { Voornaam = "Tibo", Familienaam = "Menardo" };
			context.Klanten.Add(k);
			context.SaveChanges();
			Data.Woning huis = new Data.Huis() { Adres = new Data.Adres() { Straat = "Stormstraat", Nummer = 1, Postnummer = 1000, Gemeente = "Brussel" }, BouwDatum = DateTime.Now, Huistype =2, Waarde = (decimal?)382.2, Eigenaar=k};
			Data.Woning appart = new Data.Appartement() { Verdieping = 2, Adres = new Data.Adres() { Straat = "Stormstraat", Nummer = 1, Postnummer = 1000, Gemeente = "Brussel" }, BouwDatum = DateTime.Now, Waarde = (decimal?)382.2, Eigenaar = k };
            context.Woningen.Add(huis);
            context.Woningen.Add(appart);

            context.SaveChanges();
            //Data.WoningenRepository.AddWoning(huis);
            //Data.WoningenRepository.AddWoning(appart);

   //         Data.Woning updatew = Data.WoningenRepository.GetWoningen()[0];
			

   //         updatew.BouwDatum = DateTime.Now.AddDays(400);
			//updatew.Adres.Gemeente = "mechelen";

            //Data.WoningenRepository.UpdateWoning(updatew);
            //Data.WoningenRepository.FindWoning(2);



            foreach (Data.Klant klant in context.Klanten)
			{
				Console.WriteLine(klant.Voornaam);
			}
			List<Data.Woning> list = Data.WoningenRepository.GetWoningen();

            Console.WriteLine(list);
        }

        private static void TestBusiRepo()
		{
            Object klanten = Data.KlantenRepository.GetKlanten();
			Object woningfromklant1 = Data.WoningenRepository.GetWoningenFromKlant(1);
            Object woningfromklant12 = Data.WoningenRepository.GetWoningenFromKlant(2);
            Object woningfromklant3 = Data.WoningenRepository.GetWoningenFromKlant(3);
            Object woningfromklant4 = Data.WoningenRepository.GetWoningenFromKlant(4);

            Object datawoningen = Data.WoningenRepository.GetWoningen();
            Object dataAppartementen = Data.WoningenRepository.GetAppartementen();
            Object dataHuizen = Data.WoningenRepository.GetHuizen();


            Object busiklanten = Business.Services.KlantenRepository.GetKlanten();

            Woningen woningen = Business.Services.WoningenRepository.GetWoningen();
            Woningen Appartementen = Business.Services.WoningenRepository.GetAppartementen();
			Appartementen[0].Waarde += 99;
            (Appartementen[0] as Business.Appartement).Verdieping -= 1;
            Appartementen[0].Adres.Nummer += 99+1;
            object tesdt = Business.Services.WoningenRepository.UpdateWoning(Appartementen[0] as Business.Appartement);

            Woningen huizen = Business.Services.WoningenRepository.GetHuizen();

            Boolean test = true;
        }

		static void testAPI()
		{
   //         JObject json = CurrencyConverterService.APICallCurrencyList("EUR").Result;
   //         Dictionary<string, decimal> dictionarry = CurrencyConverterService.GetCurrencies();
			//decimal? converted = CurrencyConverterService.ConvertFromEuroTo("USD");
            //Console.WriteLine(json);
            //Console.WriteLine(dictionarry);
            //Console.WriteLine(converted);
        }

        static void TestAdresExceptions()
		{
			Console.WriteLine("\nTest Adres Exceptions");
			int deel = 1;
			Business.Adres adres = null;
			while (deel != 0)
			{
				try
				{
					switch (deel)
					{
						case 1:
							adres = new Business.Adres(null, 0, 0, null);
							break;
						case 2:
							adres = new Business.Adres("Stormstraat", 0, 0, null);
							break;
						case 3:
							adres = new Business.Adres("Stormstraat", 1, 0, null);
							break;
						case 4:
							adres = new Business.Adres("Stormstraat", 1, 1000, null);
							break;
						case 5:
							adres = new Business.Adres("Stormstraat", 1, 1000, "Brussel");
							break;
						default:
							break;
					}
					deel = 0;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"{deel}) {ex.GetType().Name}: {ex.Message}");
					++deel;
				}
			}
			Console.WriteLine($"adres: {adres}");
		}

		static void TestPropertyChanged()
		{
			Console.WriteLine($"{Klanten[4].Eigendommen.Count}");
			Klanten[4].PropertyChanged += Program_PropertyChanged;
			Klanten[4].Voornaam = "test";
			Klanten[4].Eigendommen[0].Waarde = 1234567;


			
		}

		private static void Program_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Console.WriteLine($"Klant {e.PropertyName} gewijzigd.");
		}
	}
}
