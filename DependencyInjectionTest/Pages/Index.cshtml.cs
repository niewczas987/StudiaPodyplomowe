using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionTest.Pages
{
    public class IndexModel : PageModel
    {
        
        private IMojaZaleznosc _zaleznosc;
        private IWczytajDane _loadData;
        public IndexModel(IMojaZaleznosc zaleznosc, IWczytajDane loadData)
        {
            _zaleznosc = zaleznosc;
            _loadData = loadData;
        }

        public void OnGet()
        {
            _zaleznosc.NapiszWiadomosc("Ta wiadomość została wysłana z metody 'OnGet'");
            _loadData.LoadData();
        }
    }
}
