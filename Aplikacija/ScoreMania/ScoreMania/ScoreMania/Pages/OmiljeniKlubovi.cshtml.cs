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
    public class OmiljeniKluboviModel : PageModel
    {
        private readonly ILogger<OmiljeniKluboviModel> _logger;
        private readonly IDriver _driver;
        public Korisnik LogovaniKorisnik;
        public List<Klub> klubovi;
        public List<List<Utakmica>> utakmice;
        public List<Klub> domacini;
        public List<Klub> gosti;
        public string username;

        public OmiljeniKluboviModel(ILogger<OmiljeniKluboviModel> logger, IDriver driver)
        {
            _logger = logger;
            _driver = driver;
        }

        public void OnGet(string username)
        {
            this.username = username;
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

        public async Task<IActionResult> Klubovi()
        {
            var session = _driver.AsyncSession();
            klubovi = new List<Klub>();
            utakmice = new List<List<Utakmica>>();
            domacini = new List<Klub>();
            gosti = new List<Klub>();
            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    // Send cypher query to the database
                    var reader = await tx.RunAsync("MATCH(k:Klub)<-[r:OMILJENI_KLUB]-(ko:Korisnik) WHERE ko.username = '" + username + "' RETURN ID(k) as id, k.naziv as naziv, k.stadion as stadion, k.trener as trener");

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
                        klubovi.Add(new Klub(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3)));
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                        podaci.RemoveAt(0);
                    }
                    int id = 0;
                    foreach (Klub k in klubovi)
                    {
                        utakmice.Add(new List<Utakmica>());
                        string command = "MATCH (k:Klub { naziv: '" + k.naziv + "' })-[:DOMACIN]->(u:Utakmica) RETURN u.id,u.datum,u.dgolovi,u.ggolovi,u.sudija,u.vreme";
                        var reader2 = await tx.RunAsync(command);
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
                            utakmice[id].Add(new Utakmica(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3), podaci.ElementAt(4), podaci.ElementAt(5)));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }
                        command = "MATCH (k:Klub { naziv: '" + k.naziv + "' })-[:GOST]->(u:Utakmica) RETURN u.id,u.datum,u.dgolovi,u.ggolovi,u.sudija,u.vreme";
                        var reader3 = await tx.RunAsync(command);
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
                            utakmice[id].Add(new Utakmica(Convert.ToInt32(podaci.ElementAt(0)), podaci.ElementAt(1), podaci.ElementAt(2), podaci.ElementAt(3), podaci.ElementAt(4), podaci.ElementAt(5)));
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                            podaci.RemoveAt(0);
                        }

                        foreach (Utakmica u in utakmice[id])
                        {
                            string command2 = "MATCH(u:Utakmica { id:'" + u.id + "' })<-[:DOMACIN]-(k:Klub) RETURN ID(k),k.naziv as naziv,k.stadion as stadion,k.trener as trener";
                            var reader5 = await tx.RunAsync(command2);
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
                            string command3 = "MATCH(u:Utakmica { id:'" + u.id + "' })<-[:GOST]-(k:Klub) RETURN ID(k),k.naziv as naziv,k.stadion as stadion,k.trener as trener";
                            var reader4 = await tx.RunAsync(command3);
                            while (await reader4.FetchAsync())
                            {
                                podaci.Add(reader4.Current[0].ToString());
                                podaci.Add(reader4.Current[1].ToString());
                                podaci.Add(reader4.Current[2].ToString());
                                podaci.Add(reader4.Current[3].ToString());
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
                        id++;
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

        public async Task<IActionResult> OnGetObrisiAsync(string naziv, string username)
        {
            var session = _driver.AsyncSession();

            try
            {
                // Wrap whole operation into an managed transaction and
                // get the results back.
                await session.ReadTransactionAsync(async tx =>
                {
                    var podaci = new List<string>();

                    string command = "MATCH (k:Korisnik { username: '" + username + "' })-[r:OMILJENI_KLUB]->(kl:Klub { naziv: '" + naziv + "' }) DELETE r";
                    var reader = await tx.RunAsync(command);
                });
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }
            return base.RedirectToPage(new { username = username });
        }

        public async Task<IActionResult> OnGetDodajAsync(int id, string username)
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

            return base.RedirectToPage(new { username = username });
        }
    }
}
