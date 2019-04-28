using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAnalysis.Models;

namespace WebAppAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly AnalysisContext _context;

        public AnalysisController(AnalysisContext context){
            _context = context;
            if (_context.AnalysisItems.Count() == 0){_context.SaveChanges();}
        }

        [HttpPost]
        public async Task<ActionResult<TakinData>> PostSentyItem(TakinData item){
            item.Sentiment = Program.UseModelWithSingleItem(Transliteration.Front(item.SentimentText));
            _context.AnalysisItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSentyItem), new { id = item.Id }, item);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TakinData>>> GetSentyItems(){
            return await _context.AnalysisItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TakinData>> GetSentyItem(long id){
            var SentyItem = await _context.AnalysisItems.FindAsync(id);
            if (SentyItem == null){return NotFound();}
            return SentyItem;
        }
     
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSentyItem(long id, TakinData item){
            if (id != item.Id) {return BadRequest();}
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSentyItem(long id){
            var SentyItem = await _context.AnalysisItems.FindAsync(id);
            if (SentyItem == null){ return NotFound();}
            _context.AnalysisItems.Remove(SentyItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}