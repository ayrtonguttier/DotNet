using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SimpleCrudApi.DataBase;
using SimpleCrudApi.Models;

namespace SimpleCrudApi.Controllers
{
    public class PessoasController : ApiController
    {
        private SimpleCrudApiContext db = new SimpleCrudApiContext();

        // GET: api/Pessoas
        public IQueryable<Pessoa> GetPessoas()
        {
            return db.Pessoas;
        }

        // GET: api/Pessoas/5
        [ResponseType(typeof(Pessoa))]
        public async Task<IHttpActionResult> GetPessoa(int id)
        {
            Pessoa pessoa = await db.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        // PUT: api/Pessoas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoa.IdPessoa)
            {
                return BadRequest();
            }

            db.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pessoas
        [ResponseType(typeof(Pessoa))]
        public async Task<IHttpActionResult> PostPessoa(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pessoa.DataDeRegistro == DateTime.MinValue)
                pessoa.DataDeRegistro = DateTime.Now;
            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pessoa.IdPessoa }, pessoa);
        }

        // DELETE: api/Pessoas/5
        [ResponseType(typeof(Pessoa))]
        public async Task<IHttpActionResult> DeletePessoa(int id)
        {
            Pessoa pessoa = await db.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            db.Pessoas.Remove(pessoa);
            await db.SaveChangesAsync();

            return Ok(pessoa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaExists(int id)
        {
            return db.Pessoas.Count(e => e.IdPessoa == id) > 0;
        }
    }
}