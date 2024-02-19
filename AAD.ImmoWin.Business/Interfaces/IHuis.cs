using AAD.ImmoWin.Data;
using System;
using System.Collections.Generic;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IHuis : IWoning
    {
        Huistype Type { get; set; }
        Data.Huis DataObject { get; set; }
    }
}