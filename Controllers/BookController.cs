using LibraryManager.Communication.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase

{

    private static List<Book> Books = new List<Book>();
    private static int nextId = 1;

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] RequestCreateBookJson request)
    {

        var newBook = new Book
        {
            Id = nextId++,
            Autor = request.Autor,
            Genero = request.Genero,
            Preco = request.Preco,
            QuantidadeEmEstoque = request.QuantidadeEmEstoque, 
            Titulo = request.Titulo

        };

        Books.Add(newBook);


        return Created(string.Empty, newBook);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(Books);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
       var book = Books.Find(x => x.Id == id);

        if(book == null) return NotFound();

        return Ok(book);

    }


    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, RequestCreateBookJson request)
    {
        var book = Books.Find(x => x.Id == id);

        if (book == null) return NotFound();


        book.Titulo = request.Titulo;
        book.Autor = request.Autor;
        book.Preco = request.Preco;
        book.QuantidadeEmEstoque = request.QuantidadeEmEstoque;
        book.Genero = request.Genero;
     

        return NoContent();

    }



    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var book = Books.Find(x => x.Id == id);

        if (book == null) return NotFound();


        Books.Remove(book);


        return NoContent();

    }



}
