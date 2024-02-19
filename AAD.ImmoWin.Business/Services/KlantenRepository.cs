using AAD.ImmoWin.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Services
{
	public static class KlantenRepository
	{
        private static Klanten _klanten;

        static KlantenRepository()
        {
            _klanten = new Klanten();
        }

        public static Klanten GetKlanten()
        {
            _klanten = new Klanten();

            foreach (Data.Klant k in Data.KlantenRepository.GetKlanten())
            {
                Klant klant = new Klant(k);
                _klanten.Add(klant);
            }
            return _klanten;
        }
        public static Klanten GetKlantenMetEigendommen()
        {
            _klanten = new Klanten();

            foreach (Data.Klant k in Data.KlantenRepository.GetKlanten())
            {
                Klant klant = new Klant(k);
                klant.LeesEigendommen();
                _klanten.Add(klant);
            }
            return _klanten;
        }

        public static IKlant AddKlant(IKlant klant)
        {
            IKlant nieuw = new Klant(Data.KlantenRepository.AddKlant(klant.DataObject));
            _klanten.Add(nieuw);
            return nieuw;
        }

        public static IKlant UpdateKlant(IKlant klant)
        {
            Data.KlantenRepository.UpdateKlant(klant.DataObject);
            return klant;
        }

        public static void RemoveKlant(IKlant klant)
        {
            _klanten.Remove(klant);
            Data.KlantenRepository.RemoveKlant(klant.DataObject);
        }
    }
}
