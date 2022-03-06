using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScoreMania.Models;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ScoreMania.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDriver _driver;
        public Korisnik korisnik;

        [BindProperty]
        public string KorisnickoIme { get; set; }
        [BindProperty]
        public string Sifra { get; set; }
        public bool Greska = false;
        public IndexModel(ILogger<IndexModel> logger, IDriver driver)
        {
            _logger = logger;
            _driver = driver;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var session = _driver.AsyncSession();

            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    // Send cypher query to the database
                    string command = "MATCH(k:Korisnik) WHERE k.username ='" + KorisnickoIme + "' RETURN ID(k),k.email as email,k.password as password,k.username as username";
                    var reader = await tx.RunAsync(command);
                    // Loop through the records asynchronously
                    while (await reader.FetchAsync())
                    {
                        // Each current read in buffer can be reached via Current
                        podaci.Add(reader.Current[0].ToString());
                        podaci.Add(reader.Current[1].ToString());
                        podaci.Add(reader.Current[2].ToString());
                        podaci.Add(reader.Current[3].ToString());
                    }
                    while (podaci.Count != 0)
                    {
                        korisnik = new Korisnik(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3));
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                    }
                });
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }
            if (korisnik == null)
            {
                Greska = true;
                return Page();
            }
            else
                return RedirectToPage("/PocetnaZaKorisnika", new { username = korisnik.username });
        }
    }
}
