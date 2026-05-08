namespace Application;

public class ProductsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IServiceProvider serviceProvider, IUnitOfWork unitOfWork) : base(serviceProvider)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await _unitOfWork.ProductRepository
            .GetProductsWithBrandsAndCategoryAsync(cancellationToken);

        List<ProductToReturnDto> dto = products
            .Adapt<List<ProductToReturnDto>>();

        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWork.ProductRepository
            .GetSingleProductWithBrandsAndCategoryAsync(id, cancellationToken);

        if (product is null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Product not found."));

        ProductToReturnDto dto = product.Adapt<ProductToReturnDto>();
        return Ok(dto);
    }

    [HttpGet("getLast5")]
    public async Task<IActionResult> GetLast5Async(CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await _unitOfWork.ProductRepository
            .GetLast5ProductsAsync(cancellationToken);

        List<ProductToReturnDto> dto = products
            .Adapt<List<ProductToReturnDto>>();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProductUpsertDto dto, CancellationToken cancellationToken)
    {
        Product product = dto.Adapt<Product>();

        await _unitOfWork.Repository<Product>().AddAsync(product, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        Product? createdProduct = await _unitOfWork.ProductRepository
            .GetSingleProductWithBrandsAndCategoryAsync(product.Id, cancellationToken);

        ProductToReturnDto result = createdProduct!.Adapt<ProductToReturnDto>();
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductUpsertDto dto, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(id, cancellationToken);

        if (product is null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Product not found."));

        dto.Adapt(product);

        await _unitOfWork.Repository<Product>().UpdateAsync(product);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(id, cancellationToken);

        if (product is null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Product not found."));

        await _unitOfWork.Repository<Product>().DeleteAsync(product);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return NoContent();
    }
}
