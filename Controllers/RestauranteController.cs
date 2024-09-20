using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Models;
using RestauranteApi.Controllers;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly ApiContext _context;

        public RestauranteController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/produto - Lista todos os produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apiresta>>> GetProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return Ok(produtos);  // Retorna um Ok com a lista de produtos
        }

        // POST: api/produto - Adiciona um novo produto
        [HttpPost]
        public async Task<ActionResult<Apiresta>> PostProduto(Apiresta produto)
        {
            // Adiciona o produto ao banco de dados
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync(); // Salva as mudanças

            // Retorna o produto criado com a URL onde ele pode ser acessado
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        // GET: api/produto/{id} - Busca um produto específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Apiresta>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/produto/{id} - Atualiza um produto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Apiresta produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/produto/{id} - Exclui um produto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}

