using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Services
{
    public class WoningenRepository
    {
        private static Woningen _woningen;

        static WoningenRepository()
        {
            _woningen = new Woningen();
        }

        public static Woningen GetWoningen()
        {
            _woningen = new Woningen();
            foreach (Data.Woning w in Data.WoningenRepository.GetWoningen())
            {

                if (w is Data.Huis dataHuis)
                {
                    Huis huis = new Huis(dataHuis);
                    _woningen.Add(huis);

                }
                else if (w is Data.Appartement dataAppartement)
                {
                    Appartement appartement = new Appartement(dataAppartement);
                    _woningen.Add(appartement);
                }
            }

            return _woningen;
        }

        public static Woningen GetWoningenFromKlant(IKlant klant)
        {
            _woningen = new Woningen();

            foreach (Data.Woning w in Data.WoningenRepository.GetWoningenFromKlant(klant.DataObject.Id))
            {
                if (w is Data.Huis dataHuis)
                {
                    Huis huis = new Huis(dataHuis, klant);
                    _woningen.Add(huis);
                }
                else if (w is Data.Appartement dataAppartement)
                {
                    Appartement appartement = new Appartement(dataAppartement, klant);
                    _woningen.Add(appartement);
                }
            }

            return _woningen;
        }

        public static Woningen GetHuizen()
        {
            _woningen = new Woningen();
            object test = Data.WoningenRepository.GetHuizen();
            foreach (Data.Woning w in Data.WoningenRepository.GetHuizen())
            {
                if (w is Data.Huis h)
                {
                    Huis huis = new Huis(h);
                    _woningen.Add(huis);
                }

            }
            return _woningen;
        }

        public static Woningen GetAppartementen()
        {
            _woningen = new Woningen();

            foreach (Data.Woning w in Data.WoningenRepository.GetAppartementen())
            {
                if(w is Data.Appartement a)
                {
                    Appartement appartement = new Appartement(a);
                    _woningen.Add(appartement);
                }

            }

            return _woningen;
        }

        public static IWoning AddWoning(IWoning woning)
        {
            woning.Schrijf();
            if (woning is IHuis huis)
            {
                woning = new Huis((Data.Huis)Data.WoningenRepository.AddWoning(huis.DataObject));
            }
            else if (woning is IAppartement appartement)
            {
                woning = new Appartement((Data.Appartement)Data.WoningenRepository.AddWoning(appartement.DataObject));
            }
            _woningen.Add(woning);
            return woning;
        }


        public static IWoning UpdateWoning(IWoning woning)
        {
            woning.Schrijf();
            if (woning is IHuis huis)
            {
                Data.WoningenRepository.UpdateWoning(huis.DataObject);
            }
            else if (woning is IAppartement appartement)
            {
                Data.WoningenRepository.UpdateWoning(appartement.DataObject);
            }
            return woning;
        }

        public static Woningen RemoveWoning(IWoning woning)
        {
            _woningen.Remove(woning);
            if (woning is IHuis huis)
            {
                Data.WoningenRepository.RemoveWoning(huis.DataObject);
            }
            else if (woning is IAppartement appartement)
            {
                Data.WoningenRepository.RemoveWoning(appartement.DataObject);
            }
            return _woningen;
        }
    }
}
