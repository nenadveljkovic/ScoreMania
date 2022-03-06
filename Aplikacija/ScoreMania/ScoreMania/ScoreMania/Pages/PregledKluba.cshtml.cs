using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using ScoreMania.Models;

namespace ScoreMania.Pages
{
    public class PregledKlubaModel : PageModel
    {
        private readonly ILogger<PregledKlubaModel> _logger;
        private readonly IDriver _driver;
        public Korisnik LogovaniKorisnik;
        public string username;
        public string nazivkluba;
        public Klub klub;
        public List<Utakmica> odigrane;
        public List<Utakmica> neodigrane;
        public List<Klub> domacini;
        public List<Klub> gosti;
        public PregledKlubaModel(ILogger<PregledKlubaModel> logger, IDriver driver)
        {
            _logger = logger;
            _driver = driver;
        }
        public void OnGet(string username, string nazivkluba)
        {
            this.username = username;
            this.nazivkluba = nazivkluba;
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

        public async Task<IActionResult> Klub()
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
                    command = "MATCH(k: Klub { naziv: '" + nazivkluba + "'}) RETURN ID(k) as id,k.naziv as naziv,k.stadion as stadion,k.trener as trener";

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

                        klub = new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3));
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

        public async Task<IActionResult> Utakmice()
        {
            var session = _driver.AsyncSession();
            neodigrane = new List<Utakmica>();
            odigrane = new List<Utakmica>();
            domacini = new List<Klub>();
            gosti = new List<Klub>();

            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    string command = "MATCH(u:Utakmica { dgolovi: '*' })<-[:DOMACIN]-(k:Klub { naziv: '" + nazivkluba + "'}) RETURN u.id,u.datum,u.dgolovi,u.ggolovi,u.sudija,u.vreme";
                    var reader = await tx.RunAsync(command);
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
                        neodigrane.Add(new Utakmica(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3), podaci.ElementAt(4), podaci.ElementAt(5)));
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                    }

                    string command2 = "MATCH(u:Utakmica { dgolovi: '*' })<-[:GOST]-(k:Klub { naziv: '" + nazivkluba + "'}) RETURN u.id,u.datum,u.dgolovi,u.ggolovi,u.sudija,u.vreme";
                    var reader2 = await tx.RunAsync(command2);
                    while (await reader2.FetchAsync())
                    {
                        podaci.Add(reader2.Current[0].ToString());
                        podaci.Add(reader2.Current[1].ToString());
                        podaci.Add(reader2.Current[2].ToString());
                        podaci.Add(reader2.Current[3].ToString());
                        podaci.Add(reader2.Current[4].ToString());
                        podaci.Add(reader2.Current[5].ToString());
                    }

                    while (podaci.Count != 0)
                    {
                        neodigrane.Add(new Utakmica(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3), podaci.ElementAt(4), podaci.ElementAt(5)));
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                    }

                    string command3 = "MATCH(u:Utakmica)<-[:DOMACIN]-(k:Klub { naziv: '" + nazivkluba + "'}) WHERE u.dgolovi >= '0' RETURN u.id,u.datum,u.dgolovi,u.ggolovi,u.sudija,u.vreme";
                    var reader3 = await tx.RunAsync(command3);
                    while (await reader3.FetchAsync())
                    {
                        podaci.Add(reader3.Current[0].ToString());
                        podaci.Add(reader3.Current[1].ToString());
                        podaci.Add(reader3.Current[2].ToString());
                        podaci.Add(reader3.Current[3].ToString());
                        podaci.Add(reader3.Current[4].ToString());
                        podaci.Add(reader3.Current[5].ToString());
                    }

                    while (podaci.Count != 0)
                    {
                        odigrane.Add(new Utakmica(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3), podaci.ElementAt(4), podaci.ElementAt(5)));
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                    }

                    string command4 = "MATCH(u:Utakmica)<-[:GOST]-(k:Klub { naziv: '" + nazivkluba + "'}) WHERE u.dgolovi >= '0' RETURN u.id,u.datum,u.dgolovi,u.ggolovi,u.sudija,u.vreme";
                    var reader4 = await tx.RunAsync(command4);
                    while (await reader4.FetchAsync())
                    {
                        podaci.Add(reader4.Current[0].ToString());
                        podaci.Add(reader4.Current[1].ToString());
                        podaci.Add(reader4.Current[2].ToString());
                        podaci.Add(reader4.Current[3].ToString());
                        podaci.Add(reader4.Current[4].ToString());
                        podaci.Add(reader4.Current[5].ToString());
                    }

                    while (podaci.Count != 0)
                    {
                        odigrane.Add(new Utakmica(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3), podaci.ElementAt(4), podaci.ElementAt(5)));
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                    }


                    foreach (Utakmica u in odigrane)
                    {
                        string command5 = "MATCH(u:Utakmica { id:'" + u.id + "' })<-[:DOMACIN]-(k:Klub) RETURN ID(k),k.naziv as naziv,k.stadion as stadion,k.trener as trener";
                        var reader5 = await tx.RunAsync(command5);
                        while (await reader5.FetchAsync())
                        {
                            podaci.Add(reader5.Current[0].ToString());
                            podaci.Add(reader5.Current[1].ToString());
                            podaci.Add(reader5.Current[2].ToString());
                            podaci.Add(reader5.Current[3].ToString());
                        }
                        while (podaci.Count != 0)
                        {
                            domacini.Add(new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3)));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                        string command6 = "MATCH(u:Utakmica { id:'" + u.id + "' })<-[:GOST]-(k:Klub) RETURN ID(k),k.naziv as naziv,k.stadion as stadion,k.trener as trener";
                        var reader6 = await tx.RunAsync(command6);
                        while (await reader6.FetchAsync())
                        {
                            podaci.Add(reader6.Current[0].ToString());
                            podaci.Add(reader6.Current[1].ToString());
                            podaci.Add(reader6.Current[2].ToString());
                            podaci.Add(reader6.Current[3].ToString());
                        }
                        while (podaci.Count != 0)
                        {
                            gosti.Add(new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3)));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                    }
                    foreach (Utakmica u in neodigrane)
                    {
                        string command7 = "MATCH(u:Utakmica { id:'" + u.id + "' })<-[:DOMACIN]-(k:Klub) RETURN ID(k),k.naziv as naziv,k.stadion as stadion,k.trener as trener";
                        var reader7 = await tx.RunAsync(command7);
                        while (await reader7.FetchAsync())
                        {
                            podaci.Add(reader7.Current[0].ToString());
                            podaci.Add(reader7.Current[1].ToString());
                            podaci.Add(reader7.Current[2].ToString());
                            podaci.Add(reader7.Current[3].ToString());
                        }
                        while (podaci.Count != 0)
                        {
                            domacini.Add(new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3)));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                        string command8 = "MATCH(u:Utakmica { id:'" + u.id + "' })<-[:GOST]-(k:Klub) RETURN ID(k),k.naziv as naziv,k.stadion as stadion,k.trener as trener";
                        var reader8 = await tx.RunAsync(command8);
                        while (await reader8.FetchAsync())
                        {
                            podaci.Add(reader8.Current[0].ToString());
                            podaci.Add(reader8.Current[1].ToString());
                            podaci.Add(reader8.Current[2].ToString());
                            podaci.Add(reader8.Current[3].ToString());
                        }
                        while (podaci.Count != 0)
                        {
                            gosti.Add(new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3)));
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

        public async Task<IActionResult> OnGetDodajAsync(int id, string username, string nazivkluba)
        {
            var session = _driver.AsyncSession();

            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    string command2 = "MATCH (k:Korisnik { username: '" + username + "'})-[r:OMILJENA_UTAKMICA]->(u:Utakmica {id:'" + id + "'}) RETURN r.ocena";
                    var reader = await tx.RunAsync(command2);
                    while (await reader.FetchAsync())
                    {
                        // Each current read in buffer can be reached via Current
                        podaci.Add(reader.Current[0].ToString());
                    }
                    if (podaci.Count == 0)
                    {
                        string command = "MATCH (k:Korisnik),(u:Utakmica) WHERE k.username = '" + username + "' AND u.id = '" + id + "' CREATE (k)-[r:OMILJENA_UTAKMICA {ocena: '0'}]->(u)";
                        reader = await tx.RunAsync(command);
                    }
                });
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }

            return base.RedirectToPage(new { username = username , nazivkluba = nazivkluba});
        }
    }
}
