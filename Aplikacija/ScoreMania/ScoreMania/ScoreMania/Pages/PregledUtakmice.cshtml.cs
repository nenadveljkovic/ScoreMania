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
    public class PregledUtakmiceModel : PageModel
    {
        private readonly ILogger<PregledUtakmiceModel> _logger;
        private readonly IDriver _driver;
        public int utakmicaid;
        public Utakmica utakmica;
        public Korisnik LogovaniKorisnik;
        public Klub domacin;
        public Klub gost;
        public string username;
        public string domacinnaziv;
        public string gostnaziv;
        public List<Igrac> domaciIgraci;
        public List<Igrac> gostujuciIgraci;

        public PregledUtakmiceModel(ILogger<PregledUtakmiceModel> logger, IDriver driver)
        {
            _logger = logger;
            _driver = driver;
        }
        public void OnGet(int utakmica, string home, string away, string logovani)
        {
            utakmicaid = utakmica;
            domacinnaziv = home;
            gostnaziv = away;
            username = logovani;
        }

        public async Task<IActionResult> Utakmica()
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
                    string command = "MATCH(u: Utakmica { id: '" + utakmicaid + "'}) RETURN u.id,u.datum,u.dgolovi,u.ggolovi,u.sudija,u.vreme";
                    var reader = await tx.RunAsync(command);

                    // Loop through the records asynchronously
                    while (await reader.FetchAsync())
                    {
                        podaci.Add(reader.Current[0].ToString());
                        podaci.Add(reader.Current[1].ToString());
                        podaci.Add(reader.Current[2].ToString());
                        podaci.Add(reader.Current[3].ToString());
                        podaci.Add(reader.Current[4].ToString());
                        podaci.Add(reader.Current[5].ToString());
                    }

                    while (podaci.Count != 0)
                    {
                        utakmica = new Utakmica(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3), podaci.ElementAt(4), podaci.ElementAt(5));
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
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

            return new OkResult();
        }

        public async Task<IActionResult> Klub(bool dom)
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
                    string command = string.Empty;
                    if(dom)
                        command = "MATCH(k: Klub { naziv: '" + domacinnaziv + "'}) RETURN ID(k) as id,k.naziv as naziv,k.stadion as stadion,k.trener as trener";
                    else
                        command = "MATCH(k: Klub { naziv: '" + gostnaziv + "'}) RETURN ID(k) as id,k.naziv as naziv,k.stadion as stadion,k.trener as trener";
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
                        if (dom)
                        {
                            domacin = new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                        else
                        {
                            gost = new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                    }
                });
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }

            return new OkResult();
        }

        public async Task<IActionResult> Korisnik()
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
                    string command = "MATCH(a: Korisnik { username: '" + username + "'}) RETURN ID(a) as id, a.email as email, a.password as password, a.username as username";
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
                        LogovaniKorisnik = new Korisnik(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3));
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

            return new OkResult();
        }

        public async Task<IActionResult> Igraci(string nazivkluba, bool domacin)
        {
            var session = _driver.AsyncSession();
            if(domacin)
                domaciIgraci = new List<Igrac>();
            else
                gostujuciIgraci = new List<Igrac>();
            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    // Send cypher query to the database
                    string command = "MATCH(k: Klub { naziv: '" + nazivkluba + "' })<-[:NASTUPA_ZA]-(i:Igrac) RETURN ID(i) as id, i.ime as ime, i.prezime as prezime, i.godiste as godiste, i.pozicija as pozicija";
                    var reader = await tx.RunAsync(command);

                    // Loop through the records asynchronously
                    while (await reader.FetchAsync())
                    {
                        // Each current read in buffer can be reached via Current
                        podaci.Add(reader.Current[0].ToString());
                        podaci.Add(reader.Current[1].ToString());
                        podaci.Add(reader.Current[2].ToString());
                        podaci.Add(reader.Current[3].ToString());
                        podaci.Add(reader.Current[4].ToString());
                    }
                    while (podaci.Count != 0)
                    {
                        if (domacin)
                        {
                            domaciIgraci.Add(new Igrac(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), Convert.ToInt32(podaci.ElementAt(3)), podaci.ElementAt(4)));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                        else
                        {
                            gostujuciIgraci.Add(new Igrac(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), Convert.ToInt32(podaci.ElementAt(3)), podaci.ElementAt(4)));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                    }
                });
            }
            finally
            {
                await session.CloseAsync();
            }

            return new OkResult();
        }

        public async Task<IActionResult> OnGetDodajDomacinAsync(int utakmica, string domacin, string gost, string username)
        {
            var session = _driver.AsyncSession();

            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    string command = "MATCH (k:Korisnik),(kl:Klub) WHERE k.username = '" + username + "' AND kl.naziv = '" + domacin + "' CREATE (k)-[r:OMILJENI_KLUB]->(kl)";
                    var reader = await tx.RunAsync(command);
                });
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }
            return base.RedirectToPage(new { utakmica = utakmica, home = domacin, away = gost, logovani = username });
        }

        public async Task<IActionResult> OnGetDodajGostAsync(int utakmica, string domacin, string gost, string username)
        {
            var session = _driver.AsyncSession();

            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    string command = "MATCH (k:Korisnik),(kl:Klub) WHERE k.username = '" + username + "' AND kl.naziv = '" + gost + "' CREATE (k)-[r:OMILJENI_KLUB]->(kl)";
                    var reader = await tx.RunAsync(command);
                });
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }
            return base.RedirectToPage(new { utakmica= utakmica, home= domacin, away= gost, logovani= username });
        }
    }
}
