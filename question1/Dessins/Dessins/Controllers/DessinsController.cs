using Dessins.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dessins.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DessinsController : ControllerBase
    {
        [HttpGet]
        // Rien à modifier ici, juste un exemple de dessin très simple
        public ActionResult GetDrawing1()
        {
            var drawSquare = new DrawSquare(2, 2);
            
            return Ok(drawSquare);
        }

        // TODO: Il faut ajouter une nouvelle action pour dessiner la séquence mentionnée dans l'énoncé

        [HttpGet]
        public async Task<IActionResult> GetDrawing2()
        {
            var drawingEvents = new List<DrawingEvent>();  

            var drawCercle = new DrawCircle(1, 1);
            drawingEvents.Add(drawCercle); 

               drawingEvents.Add(new Wait(3)) ;         

            var drawSquare = new DrawSquare(0, 2);
            var drawSquare2 = new DrawSquare(2, 2);

            drawingEvents.Add(drawSquare);
            drawingEvents.Add(drawSquare2);

            drawingEvents.Add(new Wait(1));

            var drawEtoile = new DrawStar(1, 3, 20);


            drawingEvents.Add(drawEtoile);

            return Ok(drawingEvents); 
            

           

        }
    }
}
