using Microsoft.AspNetCore.Mvc;



namespace BackendAPI
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1,Name ="Keyboard" , Price = 100 },
            new Product { Id = 2,Name ="Mouse" , Price=50 }
        };

        private static int nextId = 3;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found");
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product newProduct)
        {

            if (newProduct == null)
                return BadRequest("Product is required");

            if (string.IsNullOrWhiteSpace(newProduct.Name))
                return BadRequest("Name is required");

            if (newProduct.Price <= 0)
                return BadRequest("Price must be greater than zero");

            
            if (newProduct.Id == 0)
            {
                newProduct.Id = nextId;
                nextId++;
            }

            products.Add(newProduct);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newProduct.Id },
                newProduct
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            products.Remove(product);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update (int id, [FromBody] Product UpdateProduct)
        {
            //Find the product by ID
            var product = products.FirstOrDefault (p=> p.Id == id);

            if (product == null)
                return NotFound();//404 not found

            //check if the request body exists 
            if (UpdateProduct == null)
                return BadRequest("Product is required");

            //valdiate Name
            if (String.IsNullOrWhiteSpace(UpdateProduct.Name))
                return BadRequest("Name is required");

            //valdiate price
            if (UpdateProduct.Price <= 0)
                return BadRequest("Price must be greater that zero");

            //update product
            product.Name = UpdateProduct.Name;
            product.Price = UpdateProduct.Price;


            //retrun the updated product    
            return Ok(product);
        }

        [HttpGet("test")]
        public string Test()
        {
            return "Api is working";
        }


    }


}
