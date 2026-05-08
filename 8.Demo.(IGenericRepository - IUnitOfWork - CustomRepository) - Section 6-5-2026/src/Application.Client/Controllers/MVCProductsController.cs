namespace Application.Client;

public class MVCProductsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IValidator<ProductUpsertDto> _productUpsertValidator;

    public MVCProductsController(IUnitOfWork unitOfWork, IValidator<ProductUpsertDto> productUpsertValidator)
    {
        _unitOfWork = unitOfWork;
        _productRepository = unitOfWork.ProductRepository;
        _productUpsertValidator = productUpsertValidator;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await _productRepository
            .GetProductsWithBrandsAndCategoryAsync(cancellationToken);

        return View(products);
    }

    public async Task<IActionResult> IndexWithPageMetaData(PageMetaData<Product> pageMeta, CancellationToken cancellationToken)
    {
        PageMetaData<Product> metaDate = await _unitOfWork
            .ProductRepository
            .GetProductsWithBrandsAndCategoryAsync(pageMeta, cancellationToken);

        return View(metaDate);
    }

    public IActionResult Create()
    {
        return View(new ProductUpsertDto(
            Name: string.Empty,
            NameSecondLanguage: string.Empty,
            Description: string.Empty,
            PictureUrl: string.Empty,
            Price: 0,
            BrandId: Guid.Empty,
            CategoryId: Guid.Empty));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductUpsertDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _productUpsertValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(dto);
        }

        Product product = dto.Adapt<Product>();

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return NotFound(new ApiResponse(404));

        ViewBag.ProductId = product.Id;

        ProductUpsertDto dto = product.Adapt<ProductUpsertDto>();
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, ProductUpsertDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _productUpsertValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            ViewBag.ProductId = id;
            return View(dto);
        }

        Product? product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return NotFound(new ApiResponse(404));

        dto.Adapt(product);

        //await _productRepository.UpdateAsync(product);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository
            .GetSingleProductWithBrandsAndCategoryAsync(id, cancellationToken);

        if (product is null)
            return NotFound(new ApiResponse(404));

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return NotFound();

        await _productRepository.DeleteAsync(product);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}